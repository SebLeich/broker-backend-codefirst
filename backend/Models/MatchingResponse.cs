namespace backend.Models
{
    public class MatchingResponse
    {
        public int total { get; set; } = 0;
        public int points { get; set; } = 0;

        public int percentage { get
            {
                if (total == 0) return 0;
                return (points / total) * 100;
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