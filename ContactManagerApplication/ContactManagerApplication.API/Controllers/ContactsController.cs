using ContactManagerApplication.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost("UploadCsv")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadCsv(IFormFile file, CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            await _contactService.ImportContactsFromCsvAsync(file.OpenReadStream(), ct);
            return Ok();
        }
    }
}
