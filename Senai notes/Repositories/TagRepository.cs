using Microsoft.EntityFrameworkCore;
using Senai_notes.Context;
using Senai_notes.Dtos;
using Senai_notes.Interfaces;
using Senai_notes.Models;

namespace Senai_notes.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly SenaiNotesContext _context;

        public TagRepository(SenaiNotesContext context)
        {
            _context = context;
        }
        public void Atualizar(int id, Tag tag)
        {
            Tag tagEncontrada = _context.Tags.Find(id);

            if (tagEncontrada == null)
            {
                throw new Exception();
            }

            tagEncontrada.TagId = tag.TagId;
            tagEncontrada.Nome = tag.Nome;

            _context.SaveChanges();
        }


        public void Cadastrar(TagDto dto)
        {

            Tag tag = new Tag
            {
                Nome = dto.Nome
            };

            _context.Tags.Add(tag);

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            Tag tagEncontrada = _context.Tags.Find(id);

            if (tagEncontrada == null)
            {
                throw new Exception();
            }

            _context.Tags.Remove(tagEncontrada);

            _context.SaveChanges();
        }

        public List<TagViewModel> ListarTodos()
        {
            return _context.Tags.Include(t => t.TagNota).Select(t => new TagViewModel 
            { 
                TagId = t.TagId,
                Nome = t.Nome,
                UserId = t.UserId

            }).ToList();
        }

        public List<TagDto> ListarTagsDoUsuario(int id)
        {
            return _context.Tags.Include(t => t.TagNota).Where(u => u.UserId == id).Select(t => new TagDto
            {
                TagId = t.TagId,
                Nome = t.Nome

            }).ToList();
        }

        public Tag BuscarTagPorIDeNome(int id, string nome)
        {
            var tags = _context.Tags.FirstOrDefault(t => t.UserId == id && t.Nome == nome);

            return tags;
        }
    }
}