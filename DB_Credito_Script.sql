CREATE DATABASE DB_Credito;
GO

USE DB_Credito;
GO

CREATE TABLE [dbo].[EdadTasa](
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Edad] INT NOT NULL,
    [Tasa] DECIMAL(5,2) NOT NULL
);
GO

CREATE TABLE [dbo].[PlazoMeses](
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Meses] INT NOT NULL,
    [Descripcion] NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE [dbo].[LogConsultas](
    [IdConsulta] INT IDENTITY(1,1) PRIMARY KEY,
    [FechaConsulta] DATETIME NOT NULL DEFAULT GETDATE(),
    [Edad] INT NOT NULL,
    [Monto] DECIMAL(18,2) NOT NULL,
    [Meses] INT NOT NULL,
    [ValorCuota] DECIMAL(18,2) NOT NULL,
    [IP_Consulta] NVARCHAR(45) NOT NULL
);
GO

INSERT INTO EdadTasa (Edad, Tasa) VALUES
(18, 1.20), (19, 1.18), (20, 1.16), (21, 1.14), (22, 1.12),
(23, 1.10), (24, 1.08), (25, 1.05);
GO

INSERT INTO PlazoMeses (Meses, Descripcion) VALUES
(3, '3 Meses'), (6, '6 Meses'), (9, '9 Meses'), (12, '12 Meses');
GO

CREATE PROCEDURE usp_ObtenerTasaPorEdad
    @Edad INT
AS
BEGIN
    SELECT Tasa FROM EdadTasa WHERE Edad = @Edad;
END;
GO

CREATE PROCEDURE usp_ValidarPlazo
    @Meses INT
AS
BEGIN
    SELECT COUNT(1) FROM PlazoMeses WHERE Meses = @Meses;
END;
GO

CREATE PROCEDURE usp_InsertarLogConsulta
    @Edad INT,
    @Monto DECIMAL(18,2),
    @Meses INT,
    @ValorCuota DECIMAL(18,2),
    @IP_Consulta NVARCHAR(45)
AS
BEGIN
    INSERT INTO LogConsultas (FechaConsulta, Edad, Monto, Meses, ValorCuota, IP_Consulta)
    VALUES (GETDATE(), @Edad, @Monto, @Meses, @ValorCuota, @IP_Consulta);
END;
GO

CREATE PROCEDURE usp_ObtenerPlazos
AS
BEGIN
    SELECT Meses, Descripcion FROM PlazoMeses ORDER BY Meses;
END;
GO
