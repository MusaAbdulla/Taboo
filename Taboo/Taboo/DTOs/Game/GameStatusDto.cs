using Taboo.DTOs.Word;

namespace Taboo.DTOs.Game
{
    public class GameStatusDto
    {
        public int Wrong {  get; set; }
        public int Success { get; set; }
        public int Pass {  get; set; }
        public int MaxPassCount { get; set; }
        public string LangCode { get; set; }
        public Stack<WordForGamedto> Words { get; set; }
        public List<int> UserWordIds { get; set; }
    }
}
