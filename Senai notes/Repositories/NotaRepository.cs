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

        private readonly ITagRepository _tagRepository;


        public NotaRepository(SenaiNotesContext context, ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            _context = context;
        }

       
        public Nota? ArquivarNota(int id)
        {
            var nota = _context.Notas.Find(id);

            if (nota == null)
            {
                return null;
            }

            nota.Arquivado = !nota.Arquivado;

            _context.SaveChanges();

            return nota;
        }

        public void Atualizar(int id, AlterarNotaDTO nota)
        {
            Nota NotaEncontrada = _context.Notas.Find(id);

            if (NotaEncontrada == null)
            {
                throw new Exception();
            }

            NotaEncontrada.Titulo = nota.Titulo;
            NotaEncontrada.Texto = nota.Texto;
            NotaEncontrada.DataAlteracao = DateTime.Now;
            NotaEncontrada.Imagem = nota.Imagem;

            _context.SaveChanges();

        }

        public List<Notaviewmodel> BuscarNotaPorTitulo(string text)
        {
            
            return _context.Notas.Include(t => t.TagNota).ThenInclude(ta => ta.Tag).Where(p => p.Titulo == text || p.Texto == text || p.TagNota.Any(tn => tn.Tag.Nome == text )).Select(t => new Notaviewmodel
            {
                NotaId = t.NotaId,
                Titulo = t.Titulo,
                Texto = t.Texto,
                DataCriacao = t.DataCriacao,
                DataAlteracao = t.DataAlteracao,
                Arquivado = t.Arquivado,
                Imagem = t.Imagem,
                tags = t.TagNota.Select(ta => new TagDto
                {
                    TagId = ta.Tag.TagId,
                    Nome = ta.Tag.Nome
                }).ToList(),
            }).ToList();
        }
        

        public NotaDto? Cadastrar(NotaDto dto)
        {

            //  crio uma lista paga armazenar id de tags que vierem do metodo
            List<int> IdTags = new List<int>();

            // itero a lista de tags que o front mandar
            foreach (var item in dto.Tags)
            {
                //  crio uma variavel para armazenar o que vem de dentro do metodo - guarda o id do usuario e a tag
                var tag = _tagRepository.BuscarTagPorIDeNome(dto.UserId, item);

                //  verifico se a tag nao existe
                if (tag == null) 
                {
                    //  instanciando a tag para conseguir mandar as informaçoes da tag para o metodo
                    tag = new Tag
                    {
                        Nome = item,            // nome da tag recebe a tag que o usuario mandar
                        UserId = dto.UserId,    // puxo o usuario de dentro da dto
                    };
                    
                    // adicionando no context a tag criada e salvando
                    _context.Add(tag);
                    _context.SaveChanges();

                }

                IdTags.Add(tag.TagId);
            }

            //  instanciando um objeto do tipo nota, para receber da DTO e converter para o tipo nota
            var novaNota = new Nota
            {
                // recebendo informacoes da dto para a nota
                Titulo = dto.Titulo,
                Texto = dto.Texto,
                DataAlteracao = DateTime.Now,
                DataCriacao = DateTime.Now,
                Arquivado = false,
                Imagem = dto.Imagem,
                UserId = dto.UserId
            };

            // adicionando a nota no context e salvando
            _context.Notas.Add(novaNota);

            _context.SaveChanges();

            foreach (var id in IdTags)
            {
                var tagNota = new TagNota 
                {
                    NotaId = novaNota.NotaId,
                    TagId = id
                };

                _context.Add(tagNota);
                _context.SaveChanges();
            }

            return dto;
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

        public List<Notaviewmodel> ListarTodasAsNotasPorUsuario(int id)
        {
            return _context.Notas.Include(t => t.TagNota).ThenInclude(ta => ta.Tag).Where(p => p.UserId == id).Select(t => new Notaviewmodel
            { 
                NotaId = t.NotaId,
                Titulo = t.Titulo,
                Texto = t.Texto,
                DataCriacao = t.DataCriacao,
                DataAlteracao = t.DataAlteracao,
                Arquivado = t.Arquivado,
                Imagem = t.Imagem,
                tags = t.TagNota.Select(ta => new TagDto 
                { 
                    TagId = ta.Tag.TagId,
                    Nome = ta.Tag.Nome
                }).ToList(),
            }).ToList();
        }

        public List<ListarTodasNotasDTO> ListarTodos()
        {
            return _context.Notas.Include(t => t.TagNota).ThenInclude(ta => ta.Tag).Select(t => new ListarTodasNotasDTO
            {
                NotaId = t.NotaId,
                Titulo = t.Titulo,
                Texto = t.Texto,
                DataCriacao = t.DataCriacao,
                DataAlteracao = t.DataAlteracao,
                Arquivado = t.Arquivado,
                Imagem = t.Imagem,
                UserId = t.UserId,
                tags = t.TagNota.Select(ta => new TagDto
                {
                    TagId = ta.Tag.TagId,
                    Nome = ta.Tag.Nome
                }).ToList(),
            }).ToList();
        }

    }   
}
