using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Senai_notes.Context;
using Senai_notes.Dtos;
using Senai_notes.Interfaces;
using Senai_notes.Models;

namespace Senai_notes.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SenaiNotesContext _context;

        public UsuarioRepository(SenaiNotesContext context)
        {
            _context = context;
        }

        public void Atualizar(int id, Usuario usuario)
        {
            Usuario usuarioEncontrado = _context.Usuarios.Find(id);

            if (usuarioEncontrado == null)
            {
                throw new Exception();
            }

            usuarioEncontrado.Nome = usuario.Nome;
            usuarioEncontrado.Email = usuario.Email;
            usuarioEncontrado.Senha = usuario.Senha;
            usuarioEncontrado.DataCriacao = usuario.DataCriacao;

            _context.SaveChanges();

        }

        public Usuario BuscarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(p => p.UserId == id);
        }

        public void Cadastrar(UsuarioDto dto)
        {
            Usuario usuario = new Usuario 
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                DataCriacao = dto.DataCriacao,
            };

            _context.Usuarios.Add(usuario);

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            // cria uma variavel do tipo usuario e procura dentro do contexto usando a funçao FIND

            Usuario usuarioEncontrado = _context.Usuarios.Find(id);

            // verifica se o valor encontrado nao e null e se for cria um erro
            if (usuarioEncontrado == null)
            {
                throw new Exception();
            }

            // manda para o contexto e salva
            _context.Usuarios.Remove(usuarioEncontrado);
            _context.SaveChanges();

        }

        // cria um metodo listar todos do tipo LIST usando a classe produto e procura usando o metodo tolist que tras uma lista com tudo que ta dentro de produto
        public List<Usuario> ListarTodos()
        {
            return _context.Usuarios.ToList();
        }
    }
}
