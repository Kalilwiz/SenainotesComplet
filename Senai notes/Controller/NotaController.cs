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
    public class NotaController : ControllerBase
    {
        private INotaRepository _notaRepository;

        public NotaController(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }
        
        [HttpPatch("{id}")]

        public IActionResult ArquivarNota(int id)
        {
            return Ok(_notaRepository.ArquivarNota(id));
        }


        [HttpGet("{id}")]

       
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

        
        public IActionResult ListarTodos()
        {
            
            return Ok(_notaRepository.ListarTodos());
        }


        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar notas",
            Description = "Metodo para cadastrar notas"
            )]

        
        public IActionResult CadastrarNota(NotaDto nota)
        {
            
            _notaRepository.Cadastrar(nota);
            
            return Created();
        }
       
        
        [HttpPut("{id}")]

        
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

