using JobsBotApp.features.GetOnBoard;
using Microsoft.EntityFrameworkCore;

namespace JobsBotApp.Data;

public class JobsDbContext : DbContext {
    public JobsDbContext(DbContextOptions<JobsDbContext> options) : base(options) {
    }

    public DbSet<JobOffer> JobOffers => Set<JobOffer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<JobOffer>(entity => {
            entity.HasKey(x => x.Id);

            entity.HasIndex(x => new { x.Source, x.ExternalId })
                  .IsUnique();
        });
    }
}