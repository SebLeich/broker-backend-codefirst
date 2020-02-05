namespace backend.Models
{
    public class OnlineDriveStorageService : Service
    {
        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            return Output;
        }
    }
}