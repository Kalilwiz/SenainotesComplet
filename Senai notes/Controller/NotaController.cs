using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai_notes.Dtos;
using Senai_notes.Interfaces;
using Senai_notes.Models;

namespace Senai_notes.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private INotaRepository _notaRepository;

        public NotaController(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        [HttpGet("{id}")]

       
        public IActionResult ListarUsuarios(int id)
        {
            Nota nota = _notaRepository.BuscarPorId(id);

            if (nota == null)
            {
                return NotFound();
            }

            return Ok(_notaRepository.ListarTodasAsNotasPorUsuario(id));
        }

        [HttpGet]

        // criando o metodo listar usando o metodo iactionresult para trazer seu resultado e o codigo para retornar ao navegador
        public IActionResult ListarTodos()
        {
            // chamando o metodo da sua interface e exibindo com o ok(codigo 200)
            return Ok(_notaRepository.ListarTodos());
        }


        // metodo posto usado para Cadastrar seus usuarios
        [HttpPost]

        // criando o metodod cadastrar usuario usando ia result com o argumento usuario usando a dto usuariodto para cadastrar apenas o que eu quero
        public IActionResult CadastrarUsuario(NotaDto nota)
        {
            // usando metodo cadastrar de dentro do seu repositorio para cadastrar com as informaçoes dadas
            _notaRepository.Cadastrar(nota);
            // retornando ok
            return Created();
        }
       
        // criando metodo put para atualizar algo criado usando um endpoint
        [HttpPut("{id}")]

        // criando um metodo alterar com os argumetos id e produto do tipo usuario
        public IActionResult Alterar(int id, NotaDto nota)
        {
            // usando try catch para tentar atualizar com base no seu id
            // retorna notfoud se nao achar
            try
            {
                //usando metodo atualizar do seu repositorio para procurar com base no seu id e alterar com as informacoes passadas dentro do seu produto
                _notaRepository.Atualizar(id, nota);

                return Ok(nota);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpDelete("{id}")]

        // criando metodo delete com o argumento id
        public IActionResult Delete(int id)
        {

            // usando try catch para tentar localizar o id informado
            // retorna notfound se nao encontrar o id
            try
            {
                // usando metodo deletar de dentro do seu repositorio com base no seu id
                _notaRepository.Deletar(id);
                // retornando que foi excluido
                return NoContent();

            }
            catch (Exception ex)
            {

                return NotFound(ex);
            }
        }
    }
}

