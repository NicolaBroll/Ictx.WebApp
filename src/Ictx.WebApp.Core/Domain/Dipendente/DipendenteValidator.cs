using FluentValidation;

namespace Ictx.WebApp.Core.Domain.Dipendente;

public class DipendenteValidator : AbstractValidator<Dipendente>
{
    public DipendenteValidator()
    {
        // Nome.
        RuleFor(dipendente => dipendente.Nome)
            .NotEmpty()
            .MinimumLength(1).WithMessage("Il campo '{PropertyName}' è obbligatorio.")
            .MaximumLength(64).WithMessage("Il campo '{PropertyName}' non può superare i {MaxLength} caratteri.");

        // Cognome.
        RuleFor(dipendente => dipendente.Cognome)
            .NotEmpty()
            .MinimumLength(1).WithMessage("Il campo '{PropertyName}' è obbligatorio.")
            .MaximumLength(64).WithMessage("Il campo '{PropertyName}' non può superare i {MaxLength} caratteri.");

        // Data nascita.
        RuleFor(dipendente => dipendente.DataNascita)
            .NotNull()
            .WithMessage("Il campo '{PropertyName}' è obbligatorio.");
    }
}
