using Microsoft.EntityFrameworkCore;
using PostService.Models;

namespace PostService.DataAccess;

public class PostingDbContext : DbContext
{
    public DbSet<Posting> Postings { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=PostingEfDb.db;");
    }
}