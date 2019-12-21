namespace backend.Models
{
    /// <summary>
    /// the class contains a relational database service
    /// </summary>
    public class RelationalDatabaseService : Service
    {
        public bool HasDBMS { get; set; }
        public bool HasReplication { get; set; }

        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            if (Search.hasDBMS != null && Search.hasDBMS.priority > 0)
            {
                Output.total += Search.hasDBMS.priority;
                if (HasDBMS) Output.points += Search.hasDBMS.priority;
            }
            if (Search.hasReplication != null && Search.hasReplication.priority > 0)
            {
                Output.total += Search.hasReplication.priority;
                if (HasReplication) Output.points += Search.hasReplication.priority;
            }
            return Output;
        }
    }
}