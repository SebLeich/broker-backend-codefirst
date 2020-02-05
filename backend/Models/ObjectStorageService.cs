namespace backend.Models
{
    public class ObjectStorageService: Service
    {
        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            return Output;
        }
    }
}