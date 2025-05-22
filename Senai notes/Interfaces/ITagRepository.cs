using Senai_notes.Dtos;
using Senai_notes.Models;

namespace Senai_notes.Interfaces
{
    public interface ITagRepository
    {
        List<TagDto> ListarTodos();

        List<TagDto> ListarTagsDoUsuario(int id);

        void Cadastrar(TagDto tag);

        void Atualizar(int id, Tag tag);

        void Deletar(int id);

        Tag BuscarTagPorIDeNome(int id, string nome);
    }
}
