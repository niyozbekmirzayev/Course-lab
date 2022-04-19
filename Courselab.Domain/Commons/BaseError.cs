namespace Courselab.Domain.Commons
{
    public class BaseError
    {
        public BaseError(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }
        public string Message { get; set; }
    }
}
