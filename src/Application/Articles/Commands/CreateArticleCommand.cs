using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Articles.Commands
{
    public class CreateArticleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    }
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateArticleCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var img = new ArticleImage
            {
                Name = request.ImageName,
                Path = request.ImagePath
            };
            var article = new Article
            {
                Title = request.Title,
                HtmlContent = request.Content,
                IsActive = false,
            };
            article.Images.Add(img);
            _context.Articles.Add(article);
            await _context.SaveChangesAsync(cancellationToken);

            return article.Id;
        }
    }
}
