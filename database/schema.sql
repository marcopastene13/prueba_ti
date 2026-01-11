CREATE DATABASE PruebaTiLibros;
GO

USE PruebaTiLibros;
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Favoritos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT NOT NULL,
    IdExterno NVARCHAR(100) NOT NULL,
    Titulo NVARCHAR(300) NOT NULL,
    Autores NVARCHAR(500) NULL,
    AnioPrimeraPublicacion INT NULL,
    UrlPortada NVARCHAR(500) NULL,

    CONSTRAINT FK_Favoritos_Usuarios
        FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id),

    CONSTRAINT UQ_Favoritos_Usuario_Externo
        UNIQUE (UsuarioId, IdExterno)
);

INSERT INTO Usuarios (Nombre) VALUES ('Usuario Demo');
