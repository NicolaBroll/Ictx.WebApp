using System;
using FluentValidation;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Application.Validators
{
    public class DipendenteValidator : AbstractValidator<Dipendente>
    {
        public DipendenteValidator()
        {
            // Codice fiscale.
            RuleFor(dipendente => dipendente.CodiceFiscale)
                .Length(16).WithMessage("Il campo 'Codice fiscale' dev'essere di {MaxLength} caratteri.");

            // Nome.
            RuleFor(dipendente => dipendente.Nome)
                .MinimumLength(1).WithMessage("Il campo '{PropertyName}' è obbligatorio.")
                .MaximumLength(64).WithMessage("Il campo '{PropertyName}' non può superare i {MaxLength} caratteri.");

            // Cognome.
            RuleFor(dipendente => dipendente.Cognome)
                .MinimumLength(1).WithMessage("Il campo '{PropertyName}' è obbligatorio.")
                .MaximumLength(64).WithMessage("Il campo '{PropertyName}' non può superare i {MaxLength} caratteri.");

            // Sesso.
            RuleFor(dipendente => dipendente.Sesso)
                .Must((string value) => Enum.IsDefined(typeof(Sesso), value)).WithMessage("Il campo '{PropertyName}' non è valido.");

            // Data nascita.
            RuleFor(dipendente => dipendente.DataNascita)
                .NotNull().WithMessage("Il campo '{PropertyName}' è obbligatorio.");
        }
    }
}
