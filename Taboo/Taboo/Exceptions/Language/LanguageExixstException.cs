namespace Taboo.Exceptions.Language
{
    public class LanguageExixstException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status409Conflict;

        public string ErrorMessage { get; }
        public LanguageExixstException()
        {
            ErrorMessage = "Language already added in database";
        }

        public LanguageExixstException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

      
    }
}
