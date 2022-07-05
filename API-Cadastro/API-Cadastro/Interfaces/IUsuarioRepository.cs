using API_Cadastro.Domain;
using System.Collections.Generic;

namespace API_Cadastro.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> Listar();
        Usuario Atualizar(int id, Usuario usuarioAtt);
        Usuario BuscarUsuario(int id);
        Usuario Criar(Usuario novoUsuario);
        void Deletar(int id);
    }
}
