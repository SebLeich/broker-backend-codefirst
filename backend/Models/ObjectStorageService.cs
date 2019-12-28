namespace backend.Models
{
    public class ObjectStorageService: Service
    {
        public bool HasFileEncryption { get; set; }
        public bool HasFileVersioning { get; set; }
        public bool HasFilePermissions { get; set; }
        public bool HasReplication { get; set; }
        public bool HasFileLocking { get; set; }

        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            if (Search.hasFileEncryption != null && Search.hasFileEncryption.priority > 0)
            {
                Output.priorityHasFileEncryption = Search.hasFileEncryption.priority;
                if (HasFileEncryption) Output.pointsHasFileEncryption = Search.hasFileEncryption.priority;
            }
            if (Search.hasReplication != null && Search.hasReplication.priority > 0)
            {
                Output.priorityHasReplication = Search.hasReplication.priority;
                if (HasReplication) Output.pointsHasReplication = Search.hasReplication.priority;
            }
            if (Search.hasFilePermissions != null && Search.hasFilePermissions.priority > 0)
            {
                Output.priorityHasFilePermissions = Search.hasFilePermissions.priority;
                if (HasFilePermissions) Output.pointsHasFilePermissions = Search.hasFilePermissions.priority;
            }
            if (Search.hasFileLocking != null && Search.hasFileLocking.priority > 0)
            {
                Output.priorityHasFileLocking = Search.hasFileLocking.priority;
                if (HasFileLocking) Output.pointsHasFileLocking = Search.hasFileLocking.priority;
            }
            if (Search.hasFileVersioning != null && Search.hasFileVersioning.priority > 0)
            {
                Output.priorityHasFileVersioning = Search.hasFileVersioning.priority;
                if (HasFileVersioning) Output.pointsHasFileVersioning = Search.hasFileVersioning.priority;
            }
            return Output;
        }
    }
}