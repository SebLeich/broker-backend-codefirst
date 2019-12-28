namespace backend.Models
{
    public class MatchingResponse
    {
        public int pointscategories { get; set; } = 0;
        public int pointscertificates { get; set; } = 0;
        public int pointsdatalocations { get; set; } = 0;
        public int pointsdeploymentinfos { get; set; } = 0;
        public int pointsmodels { get; set; } = 0;
        public int pointsproviders { get; set; } = 0;
        public int pointsstoragetype { get; set; } = 0;
        public int pointsHasFileEncryption { get; set; } = 0;
        public int pointsHasReplication { get; set; } = 0;
        public int pointsHasFilePermissions { get; set; } = 0;
        public int pointsHasFileLocking { get; set; } = 0;
        public int pointsHasFileCompression { get; set; } = 0;
        public int pointsHasDBMS { get; set; } = 0;
        public int pointsHasFileVersioning { get; set; } = 0;
        public int pointsHasAutomatedSynchronisation { get; set; } = 0;
        public int prioritycategories { get; set; } = 0;
        public int prioritycertificates { get; set; } = 0;
        public int prioritydatalocations { get; set; } = 0;
        public int prioritydeploymentinfos { get; set; } = 0;
        public int prioritymodels { get; set; } = 0;
        public int priorityproviders { get; set; } = 0;
        public int prioritystoragetype { get; set; } = 0;
        public int priorityHasFileEncryption { get; set; } = 0;
        public int priorityHasReplication { get; set; } = 0;
        public int priorityHasFilePermissions { get; set; } = 0;
        public int priorityHasFileLocking { get; set; } = 0;
        public int priorityHasFileCompression { get; set; } = 0;
        public int priorityHasDBMS { get; set; } = 0;
        public int priorityHasFileVersioning { get; set; } = 0;
        public int priorityHasAutomatedSynchronisation { get; set; } = 0;

        public int percentage { get
            {
                if (total == 0) return 0;
                return (points / total) * 100;
            }
        }

        public int points { get
            {
                return (
                    this.pointscategories +
                    this.pointscertificates +
                    this.pointsdatalocations +
                    this.pointsdeploymentinfos +
                    this.pointsHasAutomatedSynchronisation +
                    this.pointsHasDBMS +
                    this.pointsHasFileEncryption +
                    this.pointsHasFileCompression +
                    this.pointsHasFileLocking +
                    this.pointsHasFilePermissions +
                    this.pointsHasFileVersioning +
                    this.pointsHasReplication +
                    this.pointsmodels +
                    this.pointsproviders +
                    this.pointsstoragetype
                );
            }
        }

        public int total
        {
            get
            {
                return (
                    this.prioritycategories +
                    this.prioritycertificates +
                    this.prioritydatalocations +
                    this.prioritydeploymentinfos +
                    this.priorityHasAutomatedSynchronisation +
                    this.priorityHasDBMS +
                    this.priorityHasFileEncryption +
                    this.priorityHasFileCompression +
                    this.priorityHasFileLocking +
                    this.priorityHasFilePermissions +
                    this.priorityHasFileVersioning +
                    this.priorityHasReplication +
                    this.prioritymodels +
                    this.priorityproviders +
                    this.prioritystoragetype
                );
            }
        }
    }

    public class MatchingResponseWrapper<T>
    {
        public MatchingResponse match { get; set; }
        public T content { get; set; }

        public MatchingResponseWrapper()
        {

        }
    }
}