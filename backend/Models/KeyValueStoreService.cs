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
            if (Search.hasDBMS != null && Search.hasDBMS.priority > 0)
            {
                Output.priorityHasDBMS = Search.hasDBMS.priority;
                if (HasDBMS) Output.pointsHasDBMS = Search.hasDBMS.priority;
            }
            if (Search.hasReplication != null && Search.hasReplication.priority > 0)
            {
                Output.priorityHasReplication = Search.hasReplication.priority;
                if (HasReplication) Output.pointsHasReplication = Search.hasReplication.priority;
            }
            return Output;
        }
    }
}