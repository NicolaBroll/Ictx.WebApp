using System;
using FluentValidation;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Core.Validators
{
    public class DipendenteValidator : AbstractValidator<Dipendente>
    {
        public DipendenteValidator()
        {
            // Nome.
            RuleFor(dipendente => dipendente.Nome)
                .MinimumLength(1).WithMessage("Il campo '{PropertyName}' è obbligatorio.")
                .MaximumLength(64).WithMessage("Il campo '{PropertyName}' non può superare i {MaxLength} caratteri.");

            // Cognome.
            RuleFor(dipendente => dipendente.Cognome)
                .MinimumLength(1).WithMessage("Il campo '{PropertyName}' è obbligatorio.")
                .MaximumLength(64).WithMessage("Il campo '{PropertyName}' non può superare i {MaxLength} caratteri.");

            // Data nascita.
            RuleFor(dipendente => dipendente.DataNascita)
                .NotNull().WithMessage("Il campo '{PropertyName}' è obbligatorio.");
        }
    }
}
