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
                Output.total += Search.hasFileEncryption.priority;
                if (HasFileEncryption) Output.points += Search.hasFileEncryption.priority;
            }
            if (Search.hasReplication != null && Search.hasReplication.priority > 0)
            {
                Output.total += Search.hasReplication.priority;
                if (HasReplication) Output.points += Search.hasReplication.priority;
            }
            if (Search.hasFilePermissions != null && Search.hasFilePermissions.priority > 0)
            {
                Output.total += Search.hasFilePermissions.priority;
                if (HasFilePermissions) Output.points += Search.hasFilePermissions.priority;
            }
            if (Search.hasFileLocking != null && Search.hasFileLocking.priority > 0)
            {
                Output.total += Search.hasFileLocking.priority;
                if (HasFileLocking) Output.points += Search.hasFileLocking.priority;
            }
            if (Search.hasFileVersioning != null && Search.hasFileVersioning.priority > 0)
            {
                Output.total += Search.hasFileVersioning.priority;
                if (HasFileVersioning) Output.points += Search.hasFileVersioning.priority;
            }
            return Output;
        }
    }
}