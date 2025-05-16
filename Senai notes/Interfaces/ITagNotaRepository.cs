using Senai_notes.Dtos;
using Senai_notes.Models;

namespace Senai_notes.Interfaces
{
    public interface ITagNotaRepository
    {
        List<TagNota> ListarTodos();

        TagNota BuscarPorId(int id);

        void Cadastrar(TagNotaDto tag);

        void Atualizar(int id, TagNotaDto tagNota);

        void Deletar(int id);
    }
}
