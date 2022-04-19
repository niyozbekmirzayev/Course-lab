namespace Courselab.Domain.Commons
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public BaseError Error { get; set; }
    }
}
