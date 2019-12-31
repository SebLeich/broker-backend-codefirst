namespace backend.Models
{
    public class OnlineDriveStorageService : Service
    {
        public bool HasFileEncryption { get; set; }
        public bool HasFileVersioning { get; set; }
        public bool HasFilePermissions { get; set; }
        public bool HasAutomatedSynchronisation { get; set; }

        new public MatchingResponse MatchWithSearchVector(SearchVector Search)
        {
            MatchingResponse Output = base.MatchWithSearchVector(Search);
            if (Search.hasFileEncryption != null && Search.hasFileEncryption.Priority > 0)
            {
                if (HasFileEncryption) Output.pointsHasFileEncryption = Search.hasFileEncryption.Priority;
            }
            if (Search.hasFileVersioning != null && Search.hasFileVersioning.Priority > 0)
            {
                if (HasFileVersioning) Output.pointsHasFileVersioning = Search.hasFileVersioning.Priority;
            }
            if (Search.hasFilePermissions != null && Search.hasFilePermissions.Priority > 0)
            {
                if (HasFilePermissions) Output.pointsHasFilePermissions = Search.hasFilePermissions.Priority;
            }
            if (Search.hasAutomatedSynchronisation != null && Search.hasAutomatedSynchronisation.Priority > 0)
            {
                if (HasAutomatedSynchronisation) Output.pointsHasAutomatedSynchronisation = Search.hasAutomatedSynchronisation.Priority;
            }
            return Output;
        }
    }
}