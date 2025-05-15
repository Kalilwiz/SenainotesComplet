--Criacao das Roles // Back-end

	CREATE LOGIN BackEndDev WITH PASSWORD = '1234Back';
	CREATE USER BackEndDev FOR LOGIN BackEndDev;

	CREATE ROLE Role_Backend;
	GRANT SELECT, INSERT ON SCHEMA ::dbo TO Role_Backend;
	ALTER ROLE Role_Backend ADD MEMBER BackEndDev;