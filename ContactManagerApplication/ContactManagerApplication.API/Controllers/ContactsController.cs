using ContactManagerApplication.Application.Models;
using ContactManagerApplication.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerApplication.API.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;
        private const int PAGE_SIZE = 20;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllAsync(0, PAGE_SIZE, CancellationToken.None);

            return View(contacts);
        }

        [HttpPost("contacts/csv")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadCsv(IFormFile file, CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                await _contactService.ImportContactsAndSaveToDbAsync(file.OpenReadStream(), ct);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("contacts/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ContactModel model, CancellationToken ct = default)
        {
            await _contactService.UpdateAsync(id, model, ct);
            return Ok();
        }

        [HttpDelete("contacts/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct = default)
        {
            await _contactService.DeleteAsync(id, ct);
            return Ok();
        }
    }
}
