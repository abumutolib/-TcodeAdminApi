namespace Domain.Entities
{
    public class TutorialImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsPrimary { get; set; }
        public int TutorialId { get; set; }
        public virtual Tutorial Tutorial { get; set; }
    }
}
