using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FileStorageMVC.Models
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> options)
            : base(options)
        { }

        public DbSet<File> Files { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}