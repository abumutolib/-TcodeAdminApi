namespace Domain.Entities
{
    public class ArticleComment
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int ArticleId { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual Article Article { get; set; }
    }
}
