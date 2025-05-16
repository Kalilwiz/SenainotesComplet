using Microsoft.AspNetCore.Identity;
using Senai_notes.Models;

namespace Senai_notes.Services
{
    public class PasswordService
    {

        private readonly PasswordHasher<Usuario> _hasher = new();

        public string HashPasswprd(Usuario usuario)
        {
            return _hasher.HashPassword(usuario, usuario.Senha);
        }

        public bool VerificarSenha(Usuario usuario, string SenhaInformada)
        {
            var resultado = _hasher.VerifyHashedPassword(usuario, usuario.Senha, SenhaInformada);

            return resultado == PasswordVerificationResult.Success;
        }
    }
}
