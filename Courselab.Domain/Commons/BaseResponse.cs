using Newtonsoft.Json;

namespace Courselab.Domain.Commons
{
    public class BaseResponse<T>
    {
        [JsonIgnore]
        public int? Code { get; set; }

        public T Data { get; set; }
        public BaseError Error { get; set; }
    }
}
