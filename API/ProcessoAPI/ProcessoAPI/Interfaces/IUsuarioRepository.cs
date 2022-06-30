using ProcessoAPI.Domains;
using System.Collections.Generic;

namespace ProcessoAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Login(string email, string senha);
        IEnumerable<Usuario> Listar();
        Usuario Atualizar(int id, Usuario usuarioAtt);
        Usuario AtualizarStatus(int id, bool idStatus);
        Usuario BuscarPorId(int id);
        Usuario Criar (Usuario novoUsuario);
        void Deletar (int id);

    }
}
