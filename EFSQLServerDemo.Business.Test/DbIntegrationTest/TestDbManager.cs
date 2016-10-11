using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading;

namespace EFSQLServerDemo.Business.Test.DbIntegrationTest
{
    public class TestDbManager
    {
        private readonly string _localSqlServerWorkingFolder;
        private readonly string _localDatabaseFilesCacheFolder;
        private readonly string _remoteDatabaseSourceFolder;
        private readonly string _connectionString;
        private readonly string _localBaseFolder;
        private readonly string _localDeltasFolder;
        private readonly string _instanceName;
        private readonly string _database;
        private readonly bool _disabled;

        public TestDbManager(string dbName)
        {
            var oConfigSectionSettings = (NameValueCollection)ConfigurationManager.GetSection("TestDbManager/" + dbName);

            _disabled = oConfigSectionSettings == null;
            if (_disabled) return;

            _remoteDatabaseSourceFolder = oConfigSectionSettings["remoteDatabaseSourceFolder"];

            _instanceName = oConfigSectionSettings["instanceName"];

            _database = dbName;

            _connectionString = GetAliasedConnectionString("Data Source=(local);Initial Catalog=master;Trusted_Connection=True", _instanceName);

            _localSqlServerWorkingFolder = GetAliasedPath(oConfigSectionSettings["localSqlServerWorkingFolder"], _instanceName);
            _localDatabaseFilesCacheFolder = GetAliasedPath(oConfigSectionSettings["localDatabaseFilesCacheFolder"], _instanceName);
            _localBaseFolder = GetAliasedPath(oConfigSectionSettings["localBaseFolder"], _instanceName);
            _localDeltasFolder = oConfigSectionSettings["localDeltasFolder"];
        }

        private static string GetAliasedConnectionString(string connectionString, string instanceName)
        {
            if (string.IsNullOrWhiteSpace(instanceName)) return connectionString;

            var csb = new SqlConnectionStringBuilder(connectionString) { DataSource = string.Format(@".\{0}", instanceName) };

            return csb.ToString();

        }


        private static string GetAliasedPath(string path, string instanceName)
        {
            if (string.IsNullOrWhiteSpace(path)) return string.Empty;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                instanceName = "LUT"; //Local Unit Tests
            }

            return Path.Combine(path, instanceName);
        }

        public static void Reset(string database)
        {
            Console.Out.WriteLine("==============================\nResetting Database: {0}\n------------------------------", database);
            new TestDbManager(database).Reset();
            Console.Out.WriteLine("==============================\n");
        }

        public void Reset()
        {
            if (_disabled) return;
            if (ShouldSkipRemoteCopy())
            {
                NotifyCopySkip(_remoteDatabaseSourceFolder);
            }
            else
            {
                CopyNewFiles(_remoteDatabaseSourceFolder, _localDatabaseFilesCacheFolder);
            }
            Detach(_database);
            CopyNewFiles(_localDatabaseFilesCacheFolder, _localSqlServerWorkingFolder);
            ReAttach(_database, _localSqlServerWorkingFolder);
            RunLocalDeltas(_database, _localBaseFolder, _localBaseFolder + "\\13_Metadata", runDelayed: false);
            RunLocalDeltas(_database, _localDeltasFolder);
            RunLocalDeltas(_database, _localBaseFolder, _localBaseFolder + "\\13_Metadata", runDelayed: true);
        }

        private void NotifyCopySkip(string remotePath)
        {
            Console.Out.WriteLine("==============================\nFound no-reset.txt in working directory.\nSKIPPING REMOTE COPY OF: {0}\n------------------------------", remotePath);
            Console.Out.WriteLine("==============================\n");
        }

        private bool ShouldSkipRemoteCopy()
        {
            //go up for instance name, go up for database name, go up for Data
            var rootPath = Path.GetFullPath(_localSqlServerWorkingFolder + "\\..\\..\\..");

            var noResetPath = Path.Combine(rootPath, _instanceName + "-no-reset.txt");

            Console.Out.WriteLine("Looking for {0}", noResetPath);

            return File.Exists(noResetPath);
        }

        private static IEnumerable<FileInfo> GetRemoteLocation(string remotePath, int attemptNumber, int attemptsAllowed)
        {
            try
            {
                return new DirectoryInfo(remotePath).GetFiles();
            }
            catch (IOException)
            {
                if (attemptNumber > attemptsAllowed)
                {
                    Console.Out.WriteLine("{0} is still unavailable. I give up, go find a better cloud service provider.", remotePath);
                    throw;
                }
                var sleepWait = 5000 * (int)Math.Pow(2, attemptNumber - 1);
                Console.Out.WriteLine("{0} is unavailable, waiting {1} seconds before trying again ({2} of {3} attempts)", remotePath, sleepWait, attemptNumber, attemptsAllowed);
                Thread.Sleep(sleepWait);
                return GetRemoteLocation(remotePath, attemptNumber + 1, attemptsAllowed);
            }
        }

        private static void CopyNewFiles(string remotePath, string localPath)
        {
            Console.Out.WriteLine("Copying files from \"{0}\" to \"{1}\"", remotePath, localPath);
            if (remotePath == localPath) return;

            var remoteLocation = GetRemoteLocation(remotePath, 1, 5);

            foreach (var remoteFile in remoteLocation)
            {
                var localFile = new FileInfo(Path.Combine(localPath, remoteFile.Name));

                if (!localFile.Directory.Exists) localFile.Directory.Create();

                if (localFile.Exists && localFile.LastWriteTime == remoteFile.LastWriteTime)
                {

                    Console.Out.WriteLine("\t{0} is up to date.", remoteFile.Name);
                    continue;
                }
                else
                {
                    Console.Out.WriteLine("\tUpdating {0}.", remoteFile.Name);
                    remoteFile.CopyTo(localFile.FullName, true);
                }
            }
        }

        private void Detach(string dbName)
        {
            Console.Out.WriteLine("Detach \"{0}\"", dbName);
            ExecSql(string.Format(@"
				IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}')
				BEGIN
						ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE
						exec sp_detach_db '{0}', true
				END
			", dbName));
        }

        private void ReAttach(string dbName, string pathToDbFiles)
        {
            Console.Out.WriteLine("ReAttach \"{0}\"", dbName);
            var script = new StringBuilder();
            script.AppendFormat("exec sp_attach_db @dbname = '{0}'", dbName);

            var databaseFolder = new DirectoryInfo(pathToDbFiles).GetFiles();

            int counter = 1;
            foreach (var remoteFile in databaseFolder)
            {
                script.AppendFormat(@", @filename{0} ='{1}'", counter++, remoteFile.FullName);
            }

            script.AppendFormat(" ALTER DATABASE {0} SET MULTI_USER", dbName);

            ExecSql(script.ToString());
        }

        public void RunLocalDeltas(string dbName, string pathToLocalDeltas, string delayPath = "", bool runDelayed = false)
        {
            var files = GetDirectoryFiles(pathToLocalDeltas);
            foreach (var file in files)
            {
                if (runDelayed)
                {
                    if (file.ToLower().Contains(delayPath.ToLower()))
                    {
                        Console.Out.WriteLine("Executing delayed" + file);
                        RunSql(File.ReadAllText(file));
                    }
                    continue;
                }
                if (delayPath != "" && file.ToLower().Contains(delayPath.ToLower()))
                {
                    continue;
                }
                Console.Out.WriteLine("Executing " + file);
                RunSql(File.ReadAllText(file));
            }
        }

        public string[] GetDirectoryFiles(string path)
        {
            if (!Directory.Exists(path))
            {
                return new string[] { };
            }
            var files = new List<string>();
            files.AddRange(Directory.GetFiles(path, "*.sql"));
            var subDirectories = Directory.GetDirectories(path);
            foreach (var subDirectory in subDirectories)
            {
                files.AddRange(GetDirectoryFiles(subDirectory));
            }
            return files.ToArray();
        }

        private void ExecSql(string commandText)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = commandText;
                cmd.CommandTimeout = 300;
                cmd.ExecuteNonQuery();
            }
        }

        private void RunSql(string sql)
        {
            if (sql == string.Empty)
            {
                return;
            }
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction(IsolationLevel.Serializable);

                var sqlArray = SplitSqlString(sql);
                try
                {
                    var use = new SqlCommand("USE " + _database, conn, tran) { CommandTimeout = 12000 };
                    use.ExecuteNonQuery();
                    foreach (var sqlString in sqlArray)
                    {
                        if (sqlString == string.Empty)
                        {
                            return;
                        }
                        var cmd = new SqlCommand(sqlString, conn, tran) { CommandTimeout = 12000 };
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Error: " + ex.Message);
                    tran.Rollback();
                }
            }
        }

        private static IEnumerable<string> SplitSqlString(string sql)
        {
            var arrSql = new List<string>();
            using (var reader = new StringReader(sql))
            {
                string line;
                var cmd = new StringBuilder();
                var ignoring = false;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Trim().ToLower().StartsWith("/*"))
                    {
                        ignoring = true;
                    }
                    if (!ignoring)
                    {
                        if (line.Trim().ToLower().Equals("go"))
                        {
                            arrSql.Add(cmd.ToString());
                            cmd.Length = 0;
                        }
                        else
                        {
                            cmd.Append(line);
                            cmd.Append(Environment.NewLine);
                        }
                    }
                    if (line.Trim().ToLower().EndsWith("*/"))
                    {
                        ignoring = false;
                    }
                }
                if (cmd.ToString().Trim().Length > 0)
                {
                    arrSql.Add(cmd.ToString());
                }
                return arrSql.ToArray();
            }
        }
    }
}
