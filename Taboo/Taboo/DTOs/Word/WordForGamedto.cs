namespace Taboo.DTOs.Word
{
    public class WordForGamedto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> BannedWords { get; set; }
    }
}
