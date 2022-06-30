using Microsoft.EntityFrameworkCore;
using ProcessoAPI.Contexts;
using ProcessoAPI.Domains;
using ProcessoAPI.Interfaces;
using ProcessoAPI.Utils;
using System.Collections.Generic;
using System.Linq;

namespace ProcessoAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ProcessoContext ctx;

        public UsuarioRepository(ProcessoContext appContext)
        {
            ctx = appContext;
        }
        public Usuario Atualizar(int id ,Usuario usuarioAtt)
        {
            Usuario usuarioBuscado = BuscarPorId(id);
            if (usuarioBuscado != null)
            {
                usuarioBuscado.IdTipoUsuario = usuarioAtt.IdTipoUsuario;
                usuarioBuscado.Nome = usuarioAtt.Nome;
                usuarioBuscado.Email = usuarioAtt.Email;
                usuarioBuscado.Senha = usuarioAtt.Senha;
                ctx.Usuarios.Update(usuarioBuscado);
                ctx.SaveChanges();
                return ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
            }
            return null;
        }

        public Usuario AtualizarStatus(int id, bool idStatus)
        {
            Usuario usuarioBuscado = BuscarPorId(id);
            if (usuarioBuscado != null)
            {
                usuarioBuscado.Status = idStatus;
                
                ctx.Usuarios.Update(usuarioBuscado);
                ctx.SaveChanges();
            }
            return ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }

        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuarios.Include(u => u.IdTipoUsuarioNavigation). FirstOrDefault(u => u.IdUsuario == id);
        }

        public Usuario Criar(Usuario novoUsuario)
        {
            novoUsuario.Senha = Criptografia.GerarHash(novoUsuario.Senha);
            ctx.Usuarios.Add(novoUsuario);
            ctx.SaveChanges();
            return novoUsuario;
        }

        public void Deletar(int id)
        {
            
            ctx.Usuarios.Remove(BuscarPorId(id));
            ctx.SaveChanges();
           
        }

        public IEnumerable<Usuario> Listar()
        {
            return ctx.Usuarios.Include(u => u.IdTipoUsuarioNavigation).ToList();
        }

        public Usuario Login(string email, string senha)
        {
            var usuario = ctx.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario != null)
            {
                if (usuario.Senha == senha)
                {
                    usuario.Senha = Criptografia.GerarHash(usuario.Senha);

                    ctx.SaveChanges();
                }

                bool confere = Criptografia.Comparar(senha, usuario.Senha);
                if (confere)
                    return usuario;
            }

            return ctx.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
    }
}
