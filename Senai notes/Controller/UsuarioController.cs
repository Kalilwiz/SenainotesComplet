using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai_notes.Dtos;
using Senai_notes.Interfaces;
using Senai_notes.Models;

namespace Senai_notes.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]

        // criando o metodo listar usando o metodo iactionresult para trazer seu resultado e o codigo para retornar ao navegador
        public IActionResult ListarUsuarios()
        {
            // chamando o metodo da sua interface e exibindo com o ok(codigo 200)
            return Ok(_usuarioRepository.ListarTodos());
        }

        // metodo posto usado para Cadastrar seus usuarios
        [HttpPost]

        // criando o metodod cadastrar usuario usando ia result com o argumento usuario usando a dto usuariodto para cadastrar apenas o que eu quero
        public IActionResult CadastrarUsuario(UsuarioDto usuario)
        {
            // usando metodo cadastrar de dentro do seu repositorio para cadastrar com as informaçoes dadas
            _usuarioRepository.Cadastrar(usuario);
            // retornando ok
            return Created();
        }

        [HttpGet("{id}")]

        // criando o metodod listarporid com iactionresult com o argumento id
        public IActionResult ListarPorID(int id)
        {
            // criando a variavel produto do tipo produto para buscar o id informado e trazer as informacoes localizadas
            Usuario usuario = _usuarioRepository.BuscarPorId(id);

            // verificando se for nulo
            if (usuario == null)
            {
                return NotFound();
            }


            // trazendo o resultado e o codigo 
            return Ok(usuario);
        }

        // criando metodo put para atualizar algo criado usando um endpoint
        [HttpPut("{id}")]

        // criando um metodo alterar com os argumetos id e produto do tipo usuario
        public IActionResult Alterar(int id, Usuario usuario)
        {
            // usando try catch para tentar atualizar com base no seu id
            // retorna notfoud se nao achar
            try
            {
                //usando metodo atualizar do seu repositorio para procurar com base no seu id e alterar com as informacoes passadas dentro do seu produto
                _usuarioRepository.Atualizar(id, usuario);

                return Ok(usuario);
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
                _usuarioRepository.Deletar(id);
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
