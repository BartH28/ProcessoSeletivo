using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessoAPI.Contexts;
using ProcessoAPI.Domains;
using ProcessoAPI.Interfaces;
using ProcessoAPI.Repositories;
using ProcessoAPI.Utils;

namespace ProcessoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        public UsuariosController(IUsuarioRepository repo)
        {
            _usuarioRepository = repo;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuarioRepository.Listar());
        }
        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Cadastrar(Usuario novoUsuario)
        {

            _usuarioRepository.Criar(novoUsuario);

            return StatusCode(201);
        }
        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            _usuarioRepository.Deletar(id);

            return StatusCode(200);
        }
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        [Authorize(Roles = "1,2,3")]
        [HttpPut("{id}")]
        public IActionResult Alterar(int id,Usuario usuarioAtt)
        {
            return Ok(_usuarioRepository.Atualizar(id,usuarioAtt));
        }
        [Authorize(Roles = "1,2,3")]
        [HttpPatch("{id}")]
        public IActionResult AlterarStatus(int id, bool idStatus)
        {
            return Ok(_usuarioRepository.AtualizarStatus(id, idStatus));
        }

    }
}
