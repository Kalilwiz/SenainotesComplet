using Microsoft.EntityFrameworkCore;
using Senai_notes.Context;
using Senai_notes.Dtos;
using Senai_notes.Interfaces;
using Senai_notes.Models;

namespace Senai_notes.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly SenaiNotesContext _context;

        public NotaRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public void Atualizar(int id, NotaDto nota)
        {
            Nota NotaEncontrada = _context.Notas.Find(id);

            if (NotaEncontrada == null)
            {
                throw new Exception();
            }

            NotaEncontrada.Titulo = nota.Titulo;
            NotaEncontrada.Texto = nota.Texto;
            NotaEncontrada.DataCriacao = nota.DataCriacao;
            NotaEncontrada.DataAlteracao = nota.DataAlteracao;
            NotaEncontrada.Arquivado = nota.Arquivado;
            NotaEncontrada.Imagem = nota.Imagem;

            _context.SaveChanges();

        }

        public Nota BuscarPorId(int id)
        {
            return _context.Notas.FirstOrDefault(p => p.NotaId == id);
        }

        public void Cadastrar(NotaDto dto)
        {
            Nota nota = new Nota
            {
                Titulo = dto.Titulo,
                Texto = dto.Texto,
                DataAlteracao = dto.DataAlteracao,
                DataCriacao = dto.DataCriacao,
                Arquivado = dto.Arquivado,
                Imagem = dto.Imagem,
            };

            _context.Notas.Add(nota);

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            Nota NotaEncontrada = _context.Notas.Find(id);

            // verifica se o valor encontrado nao e null e se for cria um erro
            if (NotaEncontrada == null)
            {
                throw new Exception();
            }

            // manda para o contexto e salva
            _context.Notas.Remove(NotaEncontrada);
            _context.SaveChanges(); 
        }

        public List<Nota> ListarTodos(int id)
        {
            return _context.Notas.Include(t => t.TagNota).Where(p => p.UserId == id).ToList();
        }
    }
}
