using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai_notes.Dtos;
using Senai_notes.Interfaces;
using Senai_notes.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Senai_notes.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotaController : ControllerBase
    {
        private INotaRepository _notaRepository;

        public NotaController(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }
        
        [HttpPatch("{id}")]
        [SwaggerOperation(
            Summary = "Arquivar nota.",
            Description = "Método para arquivar nota."
            )]

        public IActionResult ArquivarNota(int id)
        {
            return Ok(_notaRepository.ArquivarNota(id));
        }


        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Listar nota por usuário.",
            Description = "Método para listar notas por usuário."
            )]


        public IActionResult ListarNotas(int id)
        {
            List<Notaviewmodel> nota = _notaRepository.ListarTodasAsNotasPorUsuario(id);

            if (nota == null)
            {
                return NotFound();
            }

            return Ok(_notaRepository.ListarTodasAsNotasPorUsuario(id));
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar notas.",
            Description = "Método para listar todas as notas."
            )]


        public IActionResult ListarTodos()
        {
            
            return Ok(_notaRepository.ListarTodos());
        }


        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar nota.",
            Description = "Método para cadastrar nota."
            )]

        
        public IActionResult CadastrarNota(NotaDto nota)
        {
            
            _notaRepository.Cadastrar(nota);
            
            return Created();
        }
       
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Alterar nota.",
            Description = "Método para alterar nota."
            )]


        public IActionResult Alterar(int id, NotaDto nota)
        {
            
            try
            {
                
                _notaRepository.Atualizar(id, nota);

                return Ok(nota);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletar nota.",
            Description = "Método para deletar nota."
            )]


        public IActionResult Delete(int id)
        {

            
            try
            {
                
                _notaRepository.Deletar(id);
                
                return NoContent();

            }
            catch (Exception ex)
            {

                return NotFound(ex);
            }
        }
    }
}

