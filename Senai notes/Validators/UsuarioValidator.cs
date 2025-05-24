using FluentValidation;
using Senai_notes.Dtos;

namespace Senai_notes.Validators
{
    //  herdando a classe abstractValidator do pacote e usando a interface do metodo cadastrar usuario
    public class UsuarioValidator : AbstractValidator<UsuarioDto>
    {
        //  criando um construtor para criar e ler as regras
        public UsuarioValidator()
        {

            //  criando regra para o nome
            //  NotEmpty - nao deixa ser vazio
            //  maximumlength - indica o maximo de caracteres
            //  withmessage - tras uma mensagem para quando nao atender a regra
            RuleFor(u => u.Nome)
            .NotEmpty().WithMessage("O nome de Usuario e obrigatorio").MaximumLength(100).WithMessage("O nome de usuario deve ter no maximo 100 caracteres");


        }
    }
}
