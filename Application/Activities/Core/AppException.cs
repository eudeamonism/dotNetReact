namespace Application.Activities.Core
{
    //This custom Controller inherits from the namespace, meaning it has its powers. 
    //We are creating a class called AppException.
    //Inside the class, we have three properties: integer, string, string
    //We create a constructor so it can instantiate properties when this class is invoked.
    public class AppException
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public string Details { get; set; }

        public AppException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}