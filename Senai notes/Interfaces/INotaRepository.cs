using Senai_notes.Dtos;
using Senai_notes.Models;

namespace Senai_notes.Interfaces
{
    public interface INotaRepository
    {
        // R - Read (Leitura)
        List<Notaviewmodel> ListarTodasAsNotasPorUsuario(int id);    // cria um metodo do tipo List com o tipo nota de nome ListarTodos

        List<Notaviewmodel> BuscarNotaPorTitulo();

        List<Notaviewmodel> ListarTodos();

        //  C - Create (cadastro)
        NotaDto Cadastrar(NotaDto nota);    // Cria o metodo cadastrar do tipo nota com o nome nota

        //  U - Update (atualizacao)

        void Atualizar(int id, NotaDto nota);

        //  D - Delete(Apagar)

        void Deletar(int id);

        Nota? ArquivarNota(int id);

    }
}
