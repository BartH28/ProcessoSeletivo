using API_Cadastro.Domain;
using Microsoft.EntityFrameworkCore;

namespace API_Cadastro.Infra.Contexts
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
