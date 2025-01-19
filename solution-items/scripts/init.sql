IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'RotaViagem')
BEGIN
    CREATE DATABASE RotaViagem;
    PRINT 'Banco de dados RotaViagem criado com sucesso.';
END
ELSE
BEGIN
    PRINT 'Banco de dados RotaViagem já existe.';
END
GO

USE RotaViagem;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = N'Rota' AND xtype = 'U')
BEGIN
    CREATE TABLE Rota (
        [Id]             BIGINT          IDENTITY (1, 1) NOT NULL,
        [Origem]         CHAR (3)        NOT NULL,
        [Destino]        CHAR (3)        NOT NULL,
        [Valor]          DECIMAL (15, 2) NOT NULL,
        CONSTRAINT [PK_Rota] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Tabela Rota criada com sucesso.';
END
ELSE
BEGIN
    PRINT 'Tabela Rota já existe.';
END
GO