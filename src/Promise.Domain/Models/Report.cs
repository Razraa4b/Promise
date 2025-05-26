namespace Promise.Domain.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Conclusion { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastChangedTime { get; set; }

        public Report() : this("", "", "") { }

        public Report(string title, string summary, string conclusion)
        {
            Title = title;
            Summary = summary;
            Conclusion = conclusion;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
