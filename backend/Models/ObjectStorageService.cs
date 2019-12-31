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
            if (Search.hasFileEncryption != null && Search.hasFileEncryption.Priority > 0)
            {
                if (HasFileEncryption) Output.pointsHasFileEncryption = Search.hasFileEncryption.Priority;
            }
            if (Search.hasReplication != null && Search.hasReplication.Priority > 0)
            {
                if (HasReplication) Output.pointsHasReplication = Search.hasReplication.Priority;
            }
            if (Search.hasFilePermissions != null && Search.hasFilePermissions.Priority > 0)
            {
                if (HasFilePermissions) Output.pointsHasFilePermissions = Search.hasFilePermissions.Priority;
            }
            if (Search.hasFileLocking != null && Search.hasFileLocking.Priority > 0)
            {
                if (HasFileLocking) Output.pointsHasFileLocking = Search.hasFileLocking.Priority;
            }
            if (Search.hasFileVersioning != null && Search.hasFileVersioning.Priority > 0)
            {
                if (HasFileVersioning) Output.pointsHasFileVersioning = Search.hasFileVersioning.Priority;
            }
            return Output;
        }
    }
}