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
                Output.total += Search.hasFileEncryption.priority;
                if (HasFileEncryption) Output.points += Search.hasFileEncryption.priority;
            }
            if (Search.hasFileVersioning != null && Search.hasFileVersioning.priority > 0)
            {
                Output.total += Search.hasFileVersioning.priority;
                if (HasFileVersioning) Output.points += Search.hasFileVersioning.priority;
            }
            if (Search.hasFilePermissions != null && Search.hasFilePermissions.priority > 0)
            {
                Output.total += Search.hasFilePermissions.priority;
                if (HasFilePermissions) Output.points += Search.hasFilePermissions.priority;
            }
            if (Search.hasAutomatedSynchronisation != null && Search.hasAutomatedSynchronisation.priority > 0)
            {
                Output.total += Search.hasAutomatedSynchronisation.priority;
                if (HasAutomatedSynchronisation) Output.points += Search.hasAutomatedSynchronisation.priority;
            }
            return Output;
        }
    }
}