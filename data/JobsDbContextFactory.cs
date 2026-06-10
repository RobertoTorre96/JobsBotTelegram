using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace JobsBotApp.Data;

public class JobsDbContextFactory : IDesignTimeDbContextFactory<JobsDbContext> {
    public JobsDbContext CreateDbContext(string[] args) {
        var optionsBuilder = new DbContextOptionsBuilder<JobsDbContext>();

        optionsBuilder.UseSqlite("Data Source=jobs.db");

        return new JobsDbContext(optionsBuilder.Options);
    }
}