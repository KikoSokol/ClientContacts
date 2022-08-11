using Microsoft.EntityFrameworkCore;
using ClientContacts.Models;

namespace ClientContacts.DAL
{
    class ClientContactsContext : DbContext
    {
        public ClientContactsContext(DbContextOptions options) : base(options) { }
        public DbSet<Contact> contact { get; set; } = null!;
    }
}