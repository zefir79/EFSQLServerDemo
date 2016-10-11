DO
$body$
BEGIN
   IF NOT EXISTS (
      SELECT *
      FROM   pg_catalog.pg_user
      WHERE  usename = 'efdemo_app_user') THEN

	CREATE ROLE efdemo_app_user LOGIN
	  PASSWORD 'Year2016!'
	  NOSUPERUSER INHERIT CREATEROLE REPLICATION;	  
   END IF;
END
$body$;