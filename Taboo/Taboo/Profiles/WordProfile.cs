using AutoMapper;
using Taboo.DTOs.Word;
using Taboo.Entities;

namespace Taboo.Profiles
{
    public class WordProfile:Profile
    {
        public WordProfile()
        {
            CreateMap<WordCreateDTo, Word>();
                
            CreateMap<string, BannedWord>();
            CreateMap<Word, WordGetDto>()
                .ForMember(x => x.Language, y => y.MapFrom(z => z.LanguageCode))
                .ForMember(x => x.BannedWords, y => y.MapFrom(z => z.BannedWords.Select(z => z.Text)));
            CreateMap<WordUpdateDto, Word>()
                 .ForMember(x => x.BannedWords, y => y.MapFrom(z => z.BannedWords));

        }
    }
}
