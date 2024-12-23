using FluentValidation;
using Taboo.DTOs.Word;
using Taboo.Enums;

namespace Taboo.Validators.Words
{
    public class WordUpdateDTOValidator:AbstractValidator<WordUpdateDto>
    {
        public WordUpdateDTOValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .NotNull()
                .WithMessage("Null ola bilmez !")
                .MaximumLength(32)
                .WithMessage("simvol sayi 32 den cox ola bilmez");
            RuleFor(x => x.BannedWords)
                .NotNull();

            RuleFor(x => x.BannedWords)
                .Must(x => x.Count() == (int)GameLevel.Hard)
                .WithMessage((int)GameLevel.Hard + "eded qadagan olunmus soz olmalidir ");

            RuleForEach(x => x.BannedWords)
                .NotNull()
                .WithMessage("data Null ola bilmez")
                .MaximumLength(132)
                .WithMessage("simvol 32 den cox olmamalidir");
        }
    }
}
