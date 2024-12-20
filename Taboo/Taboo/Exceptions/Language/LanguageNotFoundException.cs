using Microsoft.AspNetCore.Mvc.Formatters;

namespace Taboo.Exceptions.Language
{
    public class LanguageNotFoundException : Exception, IBaseException
    {
         int IBaseException.StatusCode => StatusCodes.Status404NotFound;

        public string ErrorMessage { get; }
        public LanguageNotFoundException()
        {
            ErrorMessage = "Language Not Found";
        }

        public LanguageNotFoundException(string message) : base(message)
        {
            ErrorMessage = message;
        }

       
    }
}
