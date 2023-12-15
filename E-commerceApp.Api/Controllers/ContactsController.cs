using E_commerceApp.Core.Entities;
using E_commerceApp.Data.DAL;
using E_commerceApp.Service.Dtos.Contacts;
using E_commerceApp.Service.Dtos.Email;
using E_commerceApp.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_commerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly WatchesDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public ContactsController(WatchesDbContext context, UserManager<AppUser> userManager,IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        [Authorize(Roles ="Member")]
        [HttpPost("")]
        public IActionResult SendMessage(ContactSendMessageDto contactDto)
        {

            var userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
            if (userId == null) return NotFound();

            UserContact model = new UserContact
            {
                AppUserId = userId,
                Email = contactDto.Email,
                Phone = contactDto.PhoneNumber,
                FullName = contactDto.FullName,
                Subject = contactDto.Subject,
                Text = contactDto.Text,
            };

            _context.UserContacts.Add(model);

            _context.SaveChanges();

            return StatusCode(201, new { id = model.Id });

        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var messages = _context.UserContacts.Include(x => x.AppUser).ToList();
            
            List<UserContact> contacts = messages.Select(x=> new UserContact
            {
                Id = x.Id,
                FullName = x.FullName,
                Phone = x.Phone,
                Email=x.Email,
                Subject = x.Subject,
                Text=x.Text,

            }).ToList();

            return Ok(contacts);

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var message = _context.UserContacts.Include(x => x.AppUser).FirstOrDefault(x => x.Id == id);

            ContactGetDto getDto = new ContactGetDto
            {
                FullName = message.FullName,
                Phone = message.Phone,
                Email = message.Email,
                Subject = message.Subject,
                Note = message.Text,
            };

            return Ok(getDto);
        }

        [HttpPost("response")]
        public IActionResult ResponseMessage(ContactResponseMessageDto messageDto)
        {
            var message = _context.UserContacts.Find(messageDto.Id);

            if (message == null) return NotFound();

            var response = new Message(new string[] { message.Email! }, "Response Message", messageDto.Response);
            _emailService.SendEmail(response);

            return NoContent();

        }
    }
}
