using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;

namespace NotesAPI.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<Note> Notes { get; set; }
    }
}