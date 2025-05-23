using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai_notes.Dtos;
using Senai_notes.Interfaces;
using Senai_notes.Models;
using Senai_notes.Repositories;

namespace Senai_notes.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagNotaController : ControllerBase
    {
        private ITagNotaRepository _tagNotaRepository;

        public TagNotaController(ITagNotaRepository tagNotaRepository)
        {
            _tagNotaRepository = tagNotaRepository;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var tagNota = _tagNotaRepository.ListarTodos();
            return Ok(tagNota);
        }

        [HttpPost]
        public IActionResult CadastrarTagNota(TagNotaDto tagNota)
        {
            _tagNotaRepository.Cadastrar(tagNota);

            return Created();
        }

        [HttpPut("{id}")]

        public IActionResult Editar(int id, TagNotaDto tagNota)
        {
            try
            {
                _tagNotaRepository.Atualizar(id, tagNota);

                return Ok(tagNota);
            }
            catch (Exception ex)
            {
                return NotFound("TagNota não encontrada");
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                _tagNotaRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound("TagNota não encontrada");
            }
        }

        [HttpGet("{id}")]

        public IActionResult ListarPorId(int id)
        {
            var tagNota = _tagNotaRepository.BuscarPorId(id);

            if (tagNota == null) return NotFound();

            return Ok(tagNota);
        }



    }
}
