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

    public class TagController : ControllerBase
    {

        private ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Listar tags.",
            Description = "Método para listar todas as tags."
            )]
        public IActionResult ListarTodos()
        {

            var tag = _tagRepository.ListarTodos();
            return Ok(tag);
        }   

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar tag.",
            Description = "Método para cadastrar tag."
            )]

        public IActionResult CadastrarTag(TagDto tag)
        {
            _tagRepository.Cadastrar(tag);

            return Created();
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Editar tag.",
            Description = "Método para editar tag."
            )]

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
        [SwaggerOperation(
            Summary = "Deletar tag.",
            Description = "Método para deletar tag."
            )]

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
        [SwaggerOperation(
            Summary = "Listar tag por ID.",
            Description = "Método para listar tag por ID."
            )]

        public IActionResult ListarPorId(int id)
        {
            var tag = _tagRepository.ListarTagsDoUsuario(id);

            if (tag == null) return NotFound();

            return Ok(tag);
        }
    }
}
