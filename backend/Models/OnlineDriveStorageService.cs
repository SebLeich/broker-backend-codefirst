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
            if (Search.hasFileEncryption != null && Search.hasFileEncryption.priority > 0)
            {
                Output.priorityHasFileEncryption = Search.hasFileEncryption.priority;
                if (HasFileEncryption) Output.pointsHasFileEncryption = Search.hasFileEncryption.priority;
            }
            if (Search.hasFileVersioning != null && Search.hasFileVersioning.priority > 0)
            {
                Output.priorityHasFileVersioning = Search.hasFileVersioning.priority;
                if (HasFileVersioning) Output.pointsHasFileVersioning = Search.hasFileVersioning.priority;
            }
            if (Search.hasFilePermissions != null && Search.hasFilePermissions.priority > 0)
            {
                Output.priorityHasFilePermissions = Search.hasFilePermissions.priority;
                if (HasFilePermissions) Output.pointsHasFilePermissions = Search.hasFilePermissions.priority;
            }
            if (Search.hasAutomatedSynchronisation != null && Search.hasAutomatedSynchronisation.priority > 0)
            {
                Output.priorityHasAutomatedSynchronisation = Search.hasAutomatedSynchronisation.priority;
                if (HasAutomatedSynchronisation) Output.pointsHasAutomatedSynchronisation = Search.hasAutomatedSynchronisation.priority;
            }
            return Output;
        }
    }
}