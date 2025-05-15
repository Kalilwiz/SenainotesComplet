using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai_notes.Dtos;
using Senai_notes.Interfaces;
using Senai_notes.Models;

namespace Senai_notes.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {

            var tag = _tagRepository.ListarTodos();
            return Ok(tag);
        }

        [HttpPost]

        public IActionResult CadastrarTag(TagDto tag)
        {
            _tagRepository.Cadastrar(tag);

            return Created();
        }

        [HttpPut("{id}")]

        public IActionResult Editar(int id, Tag tag)
        {
            try
            {
                _tagRepository.Atualizar(id, tag);

                return Ok(tag);
            }
            catch (Exception ex)
            {
                return NotFound("Tag não encontrada");
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            try
            {
                _tagRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound("Tag não encontrada");
            }
        }

        [HttpGet("{id}")]

        public IActionResult ListarPorId(int id)
        {
            var tag = _tagRepository.BuscarPorId(id);

            if (tag == null) return NotFound();

            return Ok(tag);
        }
    }
}
