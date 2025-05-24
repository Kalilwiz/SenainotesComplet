using FluentValidation;
using Senai_notes.Dtos;

namespace Senai_notes.Validators
{
    public class NotaValidator : AbstractValidator<Notaviewmodel>
    {
        public NotaValidator()
        {
            RuleFor(n => n.Titulo)
                .NotEmpty().WithMessage("O titulo da nota é obrigatório.")
                .MaximumLength(100).WithMessage("O titulo da nota é obrigatório.");

            RuleFor(n => n.Texto)
                .NotEmpty().WithMessage("A descrição da anotação é obrigatória.");

            RuleFor(n => n.DataCriacao)
                .NotEmpty().WithMessage("A data da criação é obrigatória.");
        }
    }
}
