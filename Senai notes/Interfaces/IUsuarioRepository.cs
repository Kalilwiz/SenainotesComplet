using Senai_notes.Dtos;
using Senai_notes.Models;

namespace Senai_notes.Interfaces
{
    public interface IUsuarioRepository
    {
        // R - Read (Leitura)
        List<Usuario> ListarTodos();    // cria um metodo do tipo List com o tipo usuario de nome ListarTodos

        Usuario BuscarPorId(int id);    // Tras o id de cada usuario

        //  C - Create (cadastro)
        void Cadastrar(UsuarioDto usuario);    // Cria o metodo cadastrar do tipo Produto com o nome produto

        //  U - Update (atualizacao)

        void Atualizar(int id, Usuario usuario);

        //  D - Delete(Apagar)

        void Deletar(int id);

    }
}
