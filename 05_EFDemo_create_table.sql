/*******************************************************************************
   Create Tables
********************************************************************************/
/* Create state table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_State_Table" ()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'State'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."State" already exists.';
ELSE
   CREATE TABLE "ECommerce"."State" (
    "StateId" bigserial,
	"StateCode" varchar(2) not null,
    "StateDescription" varchar(50) not null,
    CONSTRAINT "PK_State_StateId" PRIMARY KEY  ("StateId"),
    CONSTRAINT "UNQ_State_StateCode" UNIQUE ("StateCode"));
END IF;

END;
$_$ LANGUAGE plpgsql;

SELECT "ECommerce"."Create_State_Table" ();


/* Create shipping service table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_ShippingService_Table" ()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'ShippingService'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."ShippingService" already exists.';
ELSE
   CREATE TABLE "ECommerce"."ShippingService" (
    "ShippingServiceId" bigserial,
	"ShippingServiceCode" varchar(10) not null,
    "ShippingServiceDescription" varchar(50) not null,
    CONSTRAINT "PK_ShippingService_ShippingServiceId" PRIMARY KEY  ("ShippingServiceId"),
    CONSTRAINT "UNQ_ShippingService_ShippingServiceCode" UNIQUE ("ShippingServiceCode"));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_ShippingService_Table" ();

/* Create address type table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_AddressType_Table" ()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'AddressType'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."AddressType" already exists.';
ELSE
   CREATE TABLE "ECommerce"."AddressType" (
    "AddressTypeId" bigserial,
	"AddressTypeCode" varchar(10) not null,
    "AddressTypeDescription" varchar(50) not null,
    CONSTRAINT "PK_AddressType_AddressTypeId" PRIMARY KEY  ("AddressTypeId"),
    CONSTRAINT "UNQ_AddressType_AddressTypeCode" UNIQUE ("AddressTypeCode"));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_AddressType_Table"();

/* Create payment mode table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_PaymentMode_Table" ()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'PaymentMode'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."PaymentMode" already exists.';
ELSE
   CREATE TABLE "ECommerce"."PaymentMode" (
    "PaymentModeId" bigserial,
	"PaymentModeCode" varchar(10) not null,
    "PaymentModeDescription" varchar(50) not null,
    CONSTRAINT "PK_PaymentMode_PaymentModeId" PRIMARY KEY  ("PaymentModeId"),
    CONSTRAINT "UNQ_PaymentMode_PaymentModeCode" UNIQUE ("PaymentModeCode"));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_PaymentMode_Table"();

/* Create customer table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_Customer_Table" ()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'Customer'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."Customer" already exists.';
ELSE
   CREATE TABLE "ECommerce"."Customer" (
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
    CONSTRAINT "PK_Customer_CustomerId" PRIMARY KEY  ("CustomerId") );
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_Customer_Table"();

/* Create order table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_Order_Table"()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'Order'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."Order" already exists.';
ELSE
   CREATE TABLE "ECommerce"."Order" (
    "OrderId" bigserial,
	"CustomerId" integer not null,
	"OrderDate" timestamp not null default CURRENT_TIMESTAMP,
	"ProcessDate" timestamp null,
	"GiftPackaging" boolean not null,
	"ShippingServiceId" integer not null,
	"FulfilledBy" varchar(30) null,
	"TotalOrderCost" decimal(7,2) not null,
    CONSTRAINT "PK_Order_OrderId" PRIMARY KEY  ("OrderId"));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_Order_Table"();

/* Create orderitems table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_OrderItem_Table"()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'OrderItem'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."OrderItem" already exists.';
ELSE
   CREATE TABLE "ECommerce"."OrderItem" (
    "OrderItemId" bigserial,
	"OrderId" integer not null,
	"ItemDescription" varchar(100) null,
	"Color" varchar(50) null,
	"Size" varchar(50) null,
	"Price" decimal(7,2) not null,
	"Quantity" varchar(50) null,
    CONSTRAINT "PK_OrderItem_OrderItemId" PRIMARY KEY  ("OrderItemId"));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_OrderItem_Table"();

/* Create orderaddress table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_OrderAddress_Table"()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'OrderAddress'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."OrderAddress" already exists.';
ELSE
   CREATE TABLE "ECommerce"."OrderAddress" (
    "OrderAddressId" bigserial,
	"OrderId" integer not null,
	"AddressTypeId" integer not null,
    "Address1" varchar(50) not null,
    "Address2" varchar(50) null,
    "City" varchar(50) null,
    "StateId" integer not null,
    "ZipCode" varchar(10) not null,
    CONSTRAINT "PK_OrderAddress_OrderAddressId" PRIMARY KEY  ("OrderAddressId"));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_OrderAddress_Table"();

/* Create order payments table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_OrderPayment_Table"()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'OrderPayment'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."OrderPayment" already exists.';
ELSE
   CREATE TABLE "ECommerce"."OrderPayment" (
    "OrderPaymentId" bigserial,
	"OrderId" integer not null,
	"PaymentModeId" integer not null,
    "CardNumber" integer not null,
    "CardName" varchar(50) null,
    "CCV" varchar(50) null,
    "ExpirationDate" varchar(6) null,
    "PaymentAmount" decimal(7,2) not null,
	"ProcessingDate" timestamp null,
    CONSTRAINT "PK_OrderPayment_OrderPaymentId" PRIMARY KEY  ("OrderPaymentId"));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_OrderPayment_Table"();

/* Create user table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_User_Table"()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'User'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."User" already exists.';
ELSE
   CREATE TABLE "ECommerce"."User" (
    "UserId" bigserial,
    "FirstName" varchar(20) not null,
    "LastName" varchar(20) not null,
    "UserName" varchar(20) not null,
    "Password" varchar(50) not null,
	"PrimarySSN" integer not null,
	"SecondarySSN" integer null,
    CONSTRAINT "PK_User_UserId" PRIMARY KEY  ("UserId"),
    CONSTRAINT "UNQ_User_UserName" UNIQUE ("UserName"),
	CONSTRAINT valid_number_p_ssn
      CHECK ("PrimarySSN" <= 999999999),
	CONSTRAINT valid_number_s_ssn
      CHECK ("SecondarySSN" <= 999999999));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_User_Table"();

/* Create accounts table */

CREATE OR REPLACE FUNCTION "ECommerce"."Create_Account_Table"()
  RETURNS void AS
$_$
BEGIN
IF EXISTS (
    SELECT *
    FROM   pg_catalog.pg_tables
    WHERE  schemaname = 'ECommerce'
    AND    tablename  = 'Account'
    ) THEN
   RAISE NOTICE 'Table "ECommerce"."Account" already exists.';
ELSE
   CREATE TABLE "ECommerce"."Account" (
    "AccountId" bigserial,
	"UserId" integer not null,
    "Year" integer not null,
    "FilingStatus" varchar(50) not null,
    "ReturnsStatus" varchar(50) null,
	"ReturnsStatusDate" timestamp null,
    "RefundStatus" varchar(50) null,
	"RefundStatusDate" timestamp null,
	"TotalExceptions" integer null,
	"AGI" decimal(14,2) not null,
	"Deductions" decimal(14,2) null,
	"TaxesDue" decimal(14,2) null,
	"PaymentsMade" decimal(14,2) null,
	"BalanceDue" decimal(14,2) null,
	"RefundDue" decimal(14,2) null,
    CONSTRAINT "PK_Account_AccountId" PRIMARY KEY  ("AccountId"));
END IF;

END;
$_$ LANGUAGE plpgsql;


SELECT "ECommerce"."Create_Account_Table"();

/*******************************************************************************
   Create Foreign Keys
********************************************************************************/
ALTER TABLE "ECommerce"."Customer" DROP CONSTRAINT IF EXISTS "FK_Customer_State_StateId";
ALTER TABLE "ECommerce"."Customer" ADD CONSTRAINT "FK_Customer_State_StateId"
    FOREIGN KEY ("StateId") REFERENCES "ECommerce"."State" ("StateId") ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE "ECommerce"."Order" DROP CONSTRAINT IF EXISTS "FK_Order_Customer_CustomerId";
ALTER TABLE "ECommerce"."Order" ADD CONSTRAINT "FK_Order_Customer_CustomerId"
    FOREIGN KEY ("CustomerId") REFERENCES "ECommerce"."Customer" ("CustomerId") ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE "ECommerce"."Order" DROP CONSTRAINT IF EXISTS "FK_Order_ShippingService_ShippingServiceId";
ALTER TABLE "ECommerce"."Order" ADD CONSTRAINT "FK_Order_ShippingService_ShippingServiceId"
    FOREIGN KEY ("ShippingServiceId") REFERENCES "ECommerce"."ShippingService" ("ShippingServiceId") ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE "ECommerce"."OrderItem" DROP CONSTRAINT IF EXISTS "FK_OrderItem_Order_OrderId";
ALTER TABLE "ECommerce"."OrderItem" ADD CONSTRAINT "FK_OrderItem_Order_OrderId"
    FOREIGN KEY ("OrderId") REFERENCES "ECommerce"."Order" ("OrderId") ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE "ECommerce"."OrderAddress" DROP CONSTRAINT IF EXISTS "FK_OrderAddress_Order_OrderId";
ALTER TABLE "ECommerce"."OrderAddress" ADD CONSTRAINT "FK_OrderAddress_Order_OrderId"
    FOREIGN KEY ("OrderId") REFERENCES "ECommerce"."Order" ("OrderId") ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE "ECommerce"."OrderAddress" DROP CONSTRAINT IF EXISTS "FK_OrderAddress_AddressType_AddressTypeId";
ALTER TABLE "ECommerce"."OrderAddress" ADD CONSTRAINT "FK_OrderAddress_AddressType_AddressTypeId"
    FOREIGN KEY ("AddressTypeId") REFERENCES "ECommerce"."AddressType" ("AddressTypeId") ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE "ECommerce"."OrderAddress" DROP CONSTRAINT IF EXISTS "FK_OrderAddress_State_StateId";
ALTER TABLE "ECommerce"."OrderAddress" ADD CONSTRAINT "FK_OrderAddress_State_StateId"
    FOREIGN KEY ("StateId") REFERENCES "ECommerce"."State" ("StateId") ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE "ECommerce"."OrderPayment" DROP CONSTRAINT IF EXISTS "FK_OrderPayment_Order_OrderId";
ALTER TABLE "ECommerce"."OrderPayment" ADD CONSTRAINT "FK_OrderPayment_Order_OrderId"
    FOREIGN KEY ("OrderId") REFERENCES "ECommerce"."Order" ("OrderId") ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE "ECommerce"."PaymentMode" DROP CONSTRAINT IF EXISTS "FK_OrderPayment_PaymentMode_PaymentModeId";
ALTER TABLE "ECommerce"."PaymentMode" ADD CONSTRAINT "FK_OrderPayment_PaymentMode_PaymentModeId"
    FOREIGN KEY ("PaymentModeId") REFERENCES "ECommerce"."PaymentMode" ("PaymentModeId") ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE "ECommerce"."Account" DROP CONSTRAINT IF EXISTS "FK_Account_User_UserId";
ALTER TABLE "ECommerce"."Account" ADD CONSTRAINT "FK_Account_User_UserId"
    FOREIGN KEY ("UserId") REFERENCES "ECommerce"."User" ("UserId") ON DELETE NO ACTION ON UPDATE NO ACTION;


/*******************************************************************************
   Populate Tables
********************************************************************************/
INSERT INTO "ECommerce"."State" ("StateId", "StateCode", "StateDescription") VALUES (1, N'MD', 'Maryland');
INSERT INTO "ECommerce"."State" ("StateId", "StateCode", "StateDescription") VALUES (2, N'VA', 'Virginia');
INSERT INTO "ECommerce"."State" ("StateId", "StateCode", "StateDescription") VALUES (3, N'DC', 'Disctrict of Columbia');

INSERT INTO "ECommerce"."User" ("UserId", "FirstName", "LastName", "UserName", "Password", "PrimarySSN", "SecondarySSN")
	VALUES (1, N'John', 'Doe', 'jdoe1234', '1RS@ccount1', '111111111', null);
INSERT INTO "ECommerce"."User" ("UserId", "FirstName", "LastName", "UserName", "Password", "PrimarySSN", "SecondarySSN")
	VALUES (2, N'Jane', 'Smith', 'jsmith1234', '1RS@ccount2', '111111112', null);
INSERT INTO "ECommerce"."User" ("UserId", "FirstName", "LastName", "UserName", "Password", "PrimarySSN", "SecondarySSN")
	VALUES (3, N'Will', 'Smith', 'wsmith1234', '1RS@ccount3', '111111113', '222222223');
INSERT INTO "ECommerce"."User" ("UserId", "FirstName", "LastName", "UserName", "Password", "PrimarySSN", "SecondarySSN")
	VALUES (4, N'John', 'Wood', 'jwood1234', '1RS@ccount4', '111111114', null);
INSERT INTO "ECommerce"."User" ("UserId", "FirstName", "LastName", "UserName", "Password", "PrimarySSN", "SecondarySSN")
	VALUES (5, N'Amanda', 'Garcia', 'agarcia1234', '1RS@ccount5', '111111115', null);
INSERT INTO "ECommerce"."User" ("UserId", "FirstName", "LastName", "UserName", "Password", "PrimarySSN", "SecondarySSN")
	VALUES (6, N'Anna', 'Smith', 'asmith1234', '1RS@ccount6', '111111116', '222222226');
INSERT INTO "ECommerce"."User" ("UserId", "FirstName", "LastName", "UserName", "Password", "PrimarySSN", "SecondarySSN")
	VALUES (7, N'Ross', 'Smith', 'rsmith1234', '1RS@ccount7', '111111117', '222222227');

  /* Individual filing as SINGLE and return status for 2015 NOT AVAILABLE*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", "ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (1, 1, '2015', 'Single', 'Not Available', current_timestamp, null, null, null, 0, null, null, null, null, null);

  /* Individual filing as SINGLE and return status for 2014 has been received by IRS*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", "ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (2, 1, '2014', 'Single', 'Return Received for 2014', current_timestamp, 'Refund Sent for 2014', '04/10/2015', 3, 50000.00, 500.00, 100.00, 100.00, 0.00, 100.00);

  /* Individual filing as SINGLE and has been RECEIVED by IRS*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", "ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (3, 2, '2015', 'Single', 'Return Received for 2015', current_timestamp, 'Return Received', null, 3, 50000.00, 500.00, 100.00, 0.00, 100.00, 0.00);

  /* Individual filing as HEAD OF HOUSEHOLD and has been RECEIVED by IRS*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", 			"ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (4, 3, '2015', 'Head of Household', 'Return Received for 2015', current_timestamp, 'Return Received', null, 0, 90000.00, 10000.00, 0.00, 0.00, 0.00, 200.00);

  /* Individual filing as HEAD OF HOUSEHOLD and return status for 2015 is EXTENSION has been approved by IRS*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", "ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (5, 4, '2015', 'Head of Household', 'Extension Approved for 2015', current_timestamp, null, null, null, 0, null, null, null, null, null);

  /* Individual filing as HEAD OF HOUSEHOLD and return status for 2014 has been approved by IRS*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", "ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (6, 4, '2014', 'Head of Household', 'Return Received for 2014', current_timestamp, 'Refund Sent for 2014', '04/01/2015', 0, 90000.00, 10000.00, 0.00, 0.00, 0.00, 200.00);

  /* Individual filing as HEAD OF HOUSEHOLD and return status for 2015 is FILING PROPOSAL SENT has been approved by IRS*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", "ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (7, 5, '2015', 'Head of Household', 'Filing Proposal Sent for 2015', current_timestamp, null, null, null, 0, null, null, null, null, null);

  /* Individual filing as HEAD OF HOUSEHOLD and return status for 2014 has been approved by IRS*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", "ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (8, 5, '2014', 'Head of Household', 'Return Received for 2014', current_timestamp, 'Refund Sent for 2014', '04/01/2015', 0, 90000.00, 10000.00, 0.00, 0.00, 0.00, 200.00);

  /* Individual filing as HEAD OF HOUSEHOLD and return status for 2015 has been approved by IRS*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", "ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (9, 6, '2015', 'Head of Household', 'Return Received for 2015', current_timestamp, 'Refund Approved', '03/01/2016', 0, 100000.00, 10000.00, 0.00, 0.00, 0.00, 500.00);

  /* Individual filing as MARRIED FILING JOINTLY and return status for 2015 has been SENT by IRS*/
  INSERT INTO "ECommerce"."Account" ("AccountId", "UserId", "Year", "FilingStatus", "ReturnsStatus", "ReturnsStatusDate",
  	"RefundStatus", "RefundStatusDate", "TotalExceptions", "AGI", "Deductions", "TaxesDue", "PaymentsMade", "BalanceDue", "RefundDue")
  	VALUES (10, 7, '2015', 'Married Filing Jointly', 'Return Received for 2015', current_timestamp, 'Refund Sent for 2015', '03/01/2016', 0, 75000.00, 5000.00, 0.00, 0.00, 0.00, 100.00);
