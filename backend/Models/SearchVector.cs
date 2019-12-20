using System.Collections.Generic;

namespace backend.Models
{
    public class SearchVector
    {
        public int minFulfillmentPercentage { get; set; }
        public SearchVectorEntryList categories { get; set; }
        public SearchVectorEntryList certificates { get; set; }
        public SearchVectorEntryList datalocations { get; set; }
        public SearchVectorEntryList deploymentinfos { get; set; }
        public SearchVectorEntryList models { get; set; }
        public SearchVectorEntryList providers { get; set; }
        public SearchVectorEntryList storageType { get; set; }
    }

    public class SearchVectorEntryList
    {
        public List<int> value { get; set; }
        public int priority { get; set; }

        public SearchVectorEntryList()
        {
            value = new List<int>();
        }
    }
    public class SearchVectorBooleanEntry
    {
        public bool value { get; set; }
        public int priority { get; set; }
    }
}