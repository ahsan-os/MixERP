DO
$$
BEGIN
    IF NOT EXISTS (SELECT * FROM pg_catalog.pg_roles WHERE rolname = 'frapid_db_user') THEN
        CREATE ROLE frapid_db_user WITH LOGIN PASSWORD 'change-on-deployment@123';
    END IF;

    COMMENT ON ROLE frapid_db_user IS 'The default user for frapid databases.';

    EXECUTE 'ALTER DATABASE ' || current_database() || ' OWNER TO frapid_db_user;';
END
$$
LANGUAGE plpgsql;

DO
$$
BEGIN
    IF NOT EXISTS (SELECT * FROM pg_catalog.pg_roles WHERE rolname = 'report_user') THEN
        CREATE ROLE report_user WITH LOGIN PASSWORD 'change-on-deployment@123';
    END IF;

    COMMENT ON ROLE report_user IS 'This user account is used by the Reporting Engine to run ad-hoc queries. It is strictly advised for this user to only have a read-only access to the database.';
END
$$
LANGUAGE plpgsql;
