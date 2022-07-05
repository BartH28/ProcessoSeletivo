using API_Cadastro.Domain;
using API_Cadastro.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_Cadastro.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private IUsuarioRepository _usuarioRepository { get; set; }
        public UsuariosController(IUsuarioRepository repo, ILogger<UsuariosController> logger)
        {
            _usuarioRepository = repo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Listar()
        {   
            _logger.LogInformation("Get: Usuarios Listados");
            return Ok(_usuarioRepository.Listar());
        }
        
        [HttpPost]
        public IActionResult Cadastrar(Usuario novoUsuario)
        {
            _logger.LogInformation($"Post: Usuario {novoUsuario.FirstName} Criado");
            _usuarioRepository.Criar(novoUsuario);

            return Created("Usuario",novoUsuario);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            _logger.LogInformation($"Delete: Usuario {id} Deletado");
            _usuarioRepository.Deletar(id);

            return StatusCode(200);
        }
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            _logger.LogInformation($"Get/{id}: Usuario {id} listado");
            return Ok(_usuarioRepository.BuscarUsuario(id));
        }
        
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Usuario usuarioAtt)
        {
            _logger.LogInformation($"Put: Usuario {usuarioAtt.FirstName} atualizado");
            return Ok(_usuarioRepository.Atualizar(id, usuarioAtt));
        }
       

    }
}
