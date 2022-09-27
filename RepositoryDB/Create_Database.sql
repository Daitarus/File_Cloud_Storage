-- Database: FileCloudStorage

-- DROP DATABASE IF EXISTS "FileCloudStorage";

CREATE DATABASE "FileCloudStorage"
    WITH
    OWNER = "user"
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United States.1252'
    LC_CTYPE = 'English_United States.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;





-- Table: public.Clients

-- DROP TABLE IF EXISTS public."Clients";

CREATE TABLE IF NOT EXISTS public."Clients"
(
    "Id" SERIAL,
    "Hash" bytea NOT NULL,
    "Name" character varying(255) COLLATE pg_catalog."default" NOT NULL,
    "Id_Files" integer[],
    CONSTRAINT "Clients_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Clients"
    OWNER to "user";





-- Table: public.Histories

-- DROP TABLE IF EXISTS public."Histories";

CREATE TABLE IF NOT EXISTS public."Histories"
(
    "Id" SERIAL,
    "Id_Client" integer NOT NULL,
    "Address" character varying(22) COLLATE pg_catalog."default" NOT NULL,
    "Time" timestamp without time zone NOT NULL,
    "Action" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "History_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Histories"
    OWNER to "user";




-- Table: public.ClientFiles

-- DROP TABLE IF EXISTS public."ClientFiles";

CREATE TABLE IF NOT EXISTS public."ClientFiles"
(
    "Id" SERIAL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Path" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Files_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."ClientFiles"
    OWNER to "user";