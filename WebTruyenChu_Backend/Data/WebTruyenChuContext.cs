using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTruyenChu_Backend.Entities;
using WebTruyenChu_Backend.Entities.EntityTypeConfiguration;

namespace WebTruyenChu_Backend.Data;

public class WebTruyenChuContext : IdentityDbContext<User,IdentityRole<int>,int>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public WebTruyenChuContext(DbContextOptions<WebTruyenChuContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // Get all the entities that inherit from AuditableEntity
        // and have a state of Added or Modified
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AuditableEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

        // For each entity we will set the Audit properties
        foreach (var entityEntry in entries)
        {
            // If the entity state is Added let's set
            // the CreatedAt and CreatedBy properties
            if (entityEntry.State == EntityState.Added)
            {
                ((AuditableEntity)entityEntry.Entity).CreatedAt = DateTime.Now;
                ((AuditableEntity)entityEntry.Entity).CreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "guest";
            }
            else
            {
                // If the state is Modified then we don't want
                // to modify the CreatedAt and CreatedBy properties
                // so we set their state as IsModified to false
                Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
            }

            // In any case we always want to set the properties
            // ModifiedAt and ModifiedBy
            ((AuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.Now;
            ((AuditableEntity)entityEntry.Entity).ModifiedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "guest";
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        /*modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new ChapterConfiguration());
        modelBuilder.ApplyConfiguration(new BookGenreConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new CommentReactionConfiguration());
        modelBuilder.ApplyConfiguration(new ChapterCommentConfiguration());
        modelBuilder.ApplyConfiguration(new BookCommentConfiguration());
        modelBuilder.ApplyConfiguration(new SavedBookConfiguration());
        modelBuilder.ApplyConfiguration(new ReadingHistoryConfiguration()); */
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebTruyenChuContext).Assembly);
    }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<SavedBook> SavedBooks { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CommentReaction> CommentReactions { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ReadingHistory> ReadingHistories { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<BookComment> CommentEntities { get; set; }
    public DbSet<ChapterComment> ChapterComments { get; set; }



}
