using Azure;
using Microsoft.EntityFrameworkCore;
using Senai_notes.Context;
using Senai_notes.Dtos;
using Senai_notes.Interfaces;
using Senai_notes.Models;

namespace Senai_notes.Repositories
{
    public class TagNotaRepository : ITagNotaRepository
    {
        private readonly SenaiNotesContext _context;

        public TagNotaRepository(SenaiNotesContext context)
        {
            _context = context;
        }
        public void Atualizar(int id, TagNotaDto tagNota)
        {
            TagNota tagNotaEncontrada = _context.TagNotas.Find(id);

            if (tagNotaEncontrada == null)
            {
                throw new Exception();
            }

            tagNotaEncontrada.TagId = tagNota.TagId;
            tagNotaEncontrada.NotaId = tagNota.NotaId;

            _context.SaveChanges();
        }

        public TagNota BuscarPorId(int id)
        {
            return _context.TagNotas.FirstOrDefault(t => t.TagNotaId == id);
        }

        public void Cadastrar(TagNotaDto dto)
        {

            TagNota tagNota = new TagNota
            {
                TagId = dto.TagId,
                NotaId = dto.NotaId
            };

            _context.TagNotas.Add(tagNota);

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            TagNota tagNotaEncontrada = _context.TagNotas.Find(id);

            if (tagNotaEncontrada == null)
            {
                throw new Exception();
            }

            _context.TagNotas.Remove(tagNotaEncontrada);

            _context.SaveChanges();
        }

        public List<TagNota> ListarTodos()
        {
            return _context.TagNotas.Include(t => t.Nota).ToList();
        }
    }
}
