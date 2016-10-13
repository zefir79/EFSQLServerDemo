DO
$body$
BEGIN
   IF NOT EXISTS (
      SELECT *
      FROM   pg_catalog.pg_user
      WHERE  usename = 'efdemo_super_user') THEN

	CREATE ROLE efdemo_super_user LOGIN
	  PASSWORD 'Year2016!'
	  SUPERUSER INHERIT CREATEDB CREATEROLE REPLICATION;	  
   END IF;
END
$body$;