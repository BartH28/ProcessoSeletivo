USE ProcessoDB;

INSERT into TipoUsuarios(nomeTipo)
VALUES ('geral'),('admin'),('root');
go

INSERT into Usuarios(idTipoUsuario,Nome,Email,Senha,Status_)
VALUES (1,'Roberto','roberto@email.com','roberto123',1),(2,'Admin','admin@email.com','admin',1);
go