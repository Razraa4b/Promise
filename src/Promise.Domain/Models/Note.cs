namespace Promise.Domain.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Note> Links { get; set; } = new List<Note>();
        public DateTime CreatedTime { get; set; }
        public DateTime LastChangedTime { get; set; }

        public Note(string title, string content)
        {
            Title = title;
            Content = content;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
