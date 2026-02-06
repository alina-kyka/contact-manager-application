using ContactManagerApplication.Domain;
using Microsoft.EntityFrameworkCore;


namespace ContactManagerApplication.Infrastructure.Context;
public class ContactManagerApplicationDbContext: DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public ContactManagerApplicationDbContext(DbContextOptions<ContactManagerApplicationDbContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
