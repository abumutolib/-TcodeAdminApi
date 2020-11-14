using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Domain.Common;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<User> AppUsers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<ArticleImage> ArticleImages { get; set; }
        public DbSet<LanguageTool> LanguageTools { get; set; }
        public DbSet<GroupTutorial> GroupTutorials { get; set; }
        public DbSet<TutorialImage> TutorialImages { get; set; }
        public DbSet<GroupLanguageTool> GroupLanguageTools { get; set; }
        public DbSet<AspNetUserRefreshToken> RefreshTokens { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
