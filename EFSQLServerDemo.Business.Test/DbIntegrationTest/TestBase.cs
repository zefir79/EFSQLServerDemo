using System;
using System.ComponentModel;

namespace EFSQLServerDemo.Business.Test.DbIntegrationTest
{
    [Category("Db_Free")]
    public class TestBase
    {
        public void Describes(string description)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine(description);
            Console.WriteLine("----------------------------------");
        }

        public void Scenario(string description)
        {
            Console.WriteLine("Scenario: " + description);
        }
    }
}
