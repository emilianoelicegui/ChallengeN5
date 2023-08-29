CREATE DATABASE N5Challenge
GO

USE N5Challenge
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PermissionTypes')
BEGIN
    CREATE TABLE dbo.[PermissionTypes] (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Descripcion VARCHAR(255),
    );

	-- Descripciones de campos
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PermissionTypes', @level2type=N'COLUMN',@level2name=N'Id';
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission description', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PermissionTypes', @level2type=N'COLUMN',@level2name=N'Descripcion';
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Permissions')
BEGIN
    CREATE TABLE dbo.[Permissions] (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        NombreEmpleado VARCHAR (250) NOT NULL,
        ApellidoEmpleado VARCHAR (250) NOT NULL,
		TipoPermiso INT NOT NULL,
		FechaPermiso DATE NOT NULL,
        -- Otros campos necesarios
        CONSTRAINT FK_Permissions_PermissionType FOREIGN KEY (TipoPermiso) REFERENCES PermissionTypes(Id)
    );

	-- Descripciones de campos
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'Id';
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee Forename', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'NombreEmpleado';
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee Surname', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'ApellidoEmpleado';
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Type', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'TipoPermiso';
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission granted on Date', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'FechaPermiso';
END

IF NOT EXISTS (SELECT * FROM dbo.[PermissionTypes] WHERE [Descripcion] = 'Administrador')
BEGIN
	INSERT INTO dbo.[PermissionTypes] VALUES ('Administrador')
END
GO

IF NOT EXISTS (SELECT * FROM dbo.[PermissionTypes] WHERE [Descripcion] = 'Empleado')
BEGIN
	INSERT INTO dbo.[PermissionTypes] VALUES ('Empleado')
END