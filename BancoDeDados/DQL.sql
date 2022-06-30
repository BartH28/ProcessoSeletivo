USE ProcessoDB

SELECT * FROM Usuarios
SELECT * FROM TipoUsuarios

SELECT Nome,Email,Senha,Status_,nomeTipo FROM Usuarios
INNER JOIN TipoUsuarios
ON Usuarios.idTipoUsuario = TipoUsuarios.idTipoUsuario