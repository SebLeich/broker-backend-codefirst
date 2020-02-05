namespace backend.Models
{
    /// <summary>
    /// the class contains a relational database service
    /// </summary>
    public class RelationalDatabaseStorageService : Service
    {
        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            return Output;
        }
    }
}