using API_Cadastro.Domain;
using API_Cadastro.Infra.Contexts;
using API_Cadastro.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace API_Cadastro.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApiContext ctx;

        public UsuarioRepository(ApiContext appContext)
        {
            ctx = appContext;
        }
        public Usuario Atualizar(int id, Usuario usuarioAtt)
        {
            Usuario usuarioBuscado = BuscarUsuario(id);
            if (usuarioBuscado != null)
            {
                usuarioBuscado.FirstName = usuarioAtt.FirstName;
                usuarioBuscado.Surname = usuarioAtt.Surname;
                usuarioBuscado.Age = usuarioAtt.Age;

                ctx.Usuarios.Update(usuarioBuscado);
                ctx.SaveChanges();

                return usuarioBuscado;
            }
            return null;
            
        }

        public Usuario BuscarUsuario(int id)
        {
            return ctx.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public Usuario Criar(Usuario novoUsuario)
        {
            ctx.Usuarios.Add(novoUsuario);
            ctx.SaveChanges();

            return novoUsuario;
        }

        public void Deletar(int id)
        {
            ctx.Usuarios.Remove(BuscarUsuario(id));
            ctx.SaveChanges(true);
        }

        public IEnumerable<Usuario> Listar()
        {
            return ctx.Usuarios.ToList();
        }
    }
}
