using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    /// <summary>
    /// the class contains a key value store service
    /// </summary>
    public class KeyValueStoreService : Service
    {
        public bool HasDBMS { get; set; }
        public bool HasReplication { get; set; }

        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            if (Search.hasDBMS != null && Search.hasDBMS.Priority > 0)
            {
                if (HasDBMS) Output.pointsHasDBMS = Search.hasDBMS.Priority;
            }
            if (Search.hasReplication != null && Search.hasReplication.Priority > 0)
            {
                if (HasReplication) Output.pointsHasReplication = Search.hasReplication.Priority;
            }
            return Output;
        }
    }
}