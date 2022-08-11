using ClientContacts.DAL;
using ClientContacts.Models;
using Microsoft.EntityFrameworkCore;
using ClientContacts.Exceptions;

namespace ClientContacts.Services
{
    public class ContactService
    {
        private ClientContactsContext _context;


        public ContactService(DbContextOptions options)
        {
            _context = new ClientContactsContext(options);
        }

        public IEnumerable<Contact> GetAllContact()
        {
            return _context.contact;
        }

        public Contact GetContactById(int id)
        {
            Contact contact = _context.contact.Find(id);

            return contact;
        }

        public bool UpdateContact(Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(contact.Id))
                {
                    throw new ObjectNotFoundException();
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public Contact AddNewContact(Contact contact)
        {
            _context.contact.Add(contact);
            _context.SaveChangesAsync();

            return contact;
        }

        public Contact DeleteContact(int id)
        {
          
            var contact = _context.contact.Find(id);
            if (contact == null)
            {
                return null;
            }

            _context.contact.Remove(contact);
            _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(int id)
        {
            return _context.contact.Any(e => e.Id == id);
        }

    }
}
