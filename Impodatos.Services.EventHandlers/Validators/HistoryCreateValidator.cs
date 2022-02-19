using FluentValidation;
using Impodatos.Services.EventHandlers.Commands;

namespace Impodatos.Services.EventHandlers.Validators
{
    public class HistoryCreateValidator : AbstractValidator<HistoryCreateCommand>
    {
      public HistoryCreateValidator()
        {
            RuleFor(x => x.UserLogin).NotNull().WithMessage("El campo usuario esta vacio");
            RuleFor(x => x.Programsid).NotNull().WithMessage("El campo Id programa esta vacio");
            RuleFor(x => x.ExcelFile).NotNull().WithMessage("No ha subido un archivo CSV");
            RuleFor(x => x.ExcelFile.ContentType).Equal("application/vnd.ms-excel").WithMessage("El archivo adjunto debe ser en formato .CSV"); 
        }
    }
}
