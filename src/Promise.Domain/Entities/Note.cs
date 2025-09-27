namespace Promise.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastChangedTime { get; set; }

        public Note() : this("", "") { }

        public Note(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
