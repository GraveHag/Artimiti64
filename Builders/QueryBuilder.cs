namespace Artimiti64
{
    public class QueryBuilder
    {
        string BaseUri;
        Dictionary<string, string> queries = new Dictionary<string, string>();

        public QueryBuilder(string baseUri)
        {
            BaseUri = baseUri;
        }

        public Uri Build()
        {
            string[] querySegments = queries.Select(x => x.Key+"="+x.Value).ToArray();
            if (querySegments.Length == 0) 
            {
                return new Uri(BaseUri);
            }
            return new Uri($"{BaseUri}?{string.Join("&", querySegments)}");
        }
        
        public void AddSegment(string segment)
        {
            string[] segmentArray = segment.Split("/");
            string validSegment = string.Join("/", segmentArray);
            BaseUri += $"/{validSegment}";
        }

        public void AddQueryParam(string argument, string value)
        {
            queries.TryAdd(argument, value);
        }
    }
}
