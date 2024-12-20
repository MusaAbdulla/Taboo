using FluentValidation;
using Taboo.DTOs.Languages;

namespace Taboo.Validators.Languages
{
    public class LanguageUpdateDTOValidtaor:AbstractValidator<LanguageUpdateDTO>
    {
        public LanguageUpdateDTOValidtaor()
        {
            
            RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
                  .WithMessage("Name bosh ola bilmez")
               .MaximumLength(128)
                  .WithMessage("Name uzulugu 128-dən çox ola bilməz");
            RuleFor(x => x.Icon)
              .NotNull()
              .NotEmpty()
                 .WithMessage(" Icon bosh ola bilmez")
              .Matches("http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?")
                 .WithMessage("Icon deyeri Link Olmaalidir")
              .MaximumLength(128)
                 .WithMessage("Icon uzulugu 128-dən çox ola bilməz");
        }
    }
}
