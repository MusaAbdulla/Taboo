using FluentValidation;
using Taboo.DTOs.Languages;

namespace Taboo.Validators.Languages
{
    public class LanguageCreateDTOValidator 
        :AbstractValidator<LanguageCreateDTO>
    {
        public LanguageCreateDTOValidator()
        {
            RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty()
                   .WithMessage("Code bosh ola bilmez")
                .MaximumLength(2)
                   .WithMessage("Code uzulugu 2-dən çox ola bilməz");
            RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
                  .WithMessage("Name bosh ola bilmez")
               .MaximumLength(128)
                  .WithMessage("Name uzulugu 128-dən çox ola bilməz");
            RuleFor(x => x.IconUrl)
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
