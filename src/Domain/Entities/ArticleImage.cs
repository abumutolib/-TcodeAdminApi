namespace Domain.Entities
{
    public class ArticleImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsPrimary { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}
