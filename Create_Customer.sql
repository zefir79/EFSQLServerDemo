CREATE OR REPLACE FUNCTION create_customer_table ()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables 
    WHERE  schemaname = 'public'
    AND    tablename  = 'Customer'
    ) THEN
   RAISE NOTICE 'Table "public"."Customer" already exists.';
ELSE
   CREATE TABLE public."Customer" (
    "CustomerId" bigserial,
	"LoginId" varchar(30) not null,
    "FirstName" varchar(20) not null,
    "LastName" varchar(20) not null,
    "FriendlyName" varchar(20) null,
    "Address1" varchar(50) not null,
    "Address2" varchar(50) null,
    "City" varchar(50) null,
    "StateId" integer not null,
    "ZipCode" varchar(10) not null,
    "Phone" integer not null,
    "AccountId" varchar(10) not null,
    "StartDate" timestamp not null default CURRENT_TIMESTAMP,
	"EndDate" timestamp null,
    CONSTRAINT "PK_Customer" PRIMARY KEY  ("CustomerId"));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT create_customer_table();