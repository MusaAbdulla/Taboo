namespace Taboo.DTOs.Word
{
    public class WordCreateDTo
    {
       
        public string Text { get; set; }
        public string LanguageCode { get; set; }
        public HashSet<string> BannedWords { get; set; }
    }
}
