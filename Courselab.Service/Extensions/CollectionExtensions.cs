using Courselab.Domain.Configurations;
using Courselab.Service.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Courselab.Service.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> ToPagesList<T>(this IEnumerable<T> source, PaginationParams @params)
        {
            var metaData = new PaginationMetadata(source.Count(), @params);
            var json = JsonConvert.SerializeObject(metaData);

            if (HttpContextHelper.ResponseHeaders.Keys.Contains("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.Context.Response.Headers.Add("X-Pagination", json);


            return @params.PageSize > 0 && @params.PageIndex >= 0
                ? source.Skip(@params.PageSize * (@params.PageIndex - 1)).Take(@params.PageSize) : source;
        }
    }
}
