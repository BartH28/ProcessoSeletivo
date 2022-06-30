CREATE DATABASE ProcessoDB;

USE ProcessoDB

CREATE TABLE TipoUsuarios(
	idTipoUsuario SMALLINT PRIMARY KEY IDENTITY,
	nomeTipo VARCHAR(5) NOT NULL 
);
GO

CREATE TABLE Usuarios(
	idUsuario  SMALLINT PRIMARY KEY IDENTITY,
	idTipoUsuario SMALLINT FOREIGN KEY REFERENCES TipoUsuarios(idTipoUsuario),
	Nome VARCHAR(100) NOT NULL,
	Email VARCHAR(256) NOT NULl,
	Senha VARCHAR(100) NOT NULl,
	Status_ BIT NOT NULL
);
GO