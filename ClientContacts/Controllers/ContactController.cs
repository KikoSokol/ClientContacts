using Microsoft.AspNetCore.Mvc;
using ClientContacts.DAL;
using ClientContacts.Models;
using Microsoft.EntityFrameworkCore;
using ClientContacts.Services;
using ClientContacts.Exceptions;

namespace ClientContacts.Controllers
{
    [Route("api/contact")]
    [ApiController]
    public class ContactController : Controller
    { 

        private readonly ContactService contactService;

        public ContactController(DbContextOptions options)
        {
            contactService = new ContactService(options);
        }


        // GET: api/Contact
        [HttpGet]
        public IEnumerable<Contact> GetContact()
        {
            return this.contactService.GetAllContact();
        }

        // GET: api/Contact/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            Contact contact = this.contactService.GetContactById(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] int id, [FromBody] Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            try
            {
                this.contactService.UpdateContact(contact);
            }
            catch(ObjectNotFoundException a)
            {
                return NotFound();
            }

            return NoContent();
        }

        //POST: api/Contact
       [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] Contact contact)
        {
            Contact newContact = this.contactService.AddNewContact(contact);

            return CreatedAtAction("GetContact", new { id = newContact.Id }, newContact);
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {

            Contact contact = this.contactService.DeleteContact(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
    }
}
