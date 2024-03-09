using FilesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilesAPI.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<FileEntity> Files { get; set; }

    }
}