namespace backend.Models
{
    /// <summary>
    /// the class contains a key value store service
    /// </summary>
    public class KeyValueStorageService : Service
    {
        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            return Output;
        }
    }
}