using FluentValidation;

namespace Ictx.WebApp.Core.Domain.DipendenteDomain;

public class DipendenteValidator : AbstractValidator<Dipendente>
{
    public DipendenteValidator()
    {
        // Nome.
        RuleFor(dipendente => dipendente.Nome)
            .NotEmpty()
            .WithMessage("Il campo '{PropertyName}' è obbligatorio.")

            .MaximumLength(64)
            .WithMessage("Il campo '{PropertyName}' non può superare i {MaxLength} caratteri.");

        // Cognome.
        RuleFor(dipendente => dipendente.Cognome)
            .NotEmpty()
            .WithMessage("Il campo '{PropertyName}' è obbligatorio.")

            .MaximumLength(64)
            .WithMessage("Il campo '{PropertyName}' non può superare i {MaxLength} caratteri.");

        // Data nascita.
        RuleFor(dipendente => dipendente.DataNascita)
            .NotEmpty()
            .WithMessage("Il campo '{PropertyName}' è obbligatorio.");
    }
}
