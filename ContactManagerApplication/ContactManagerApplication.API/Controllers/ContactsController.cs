using ContactManagerApplication.Application.Models;
using ContactManagerApplication.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagerApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ICsvService<ContactModel> _csvService;

        public ContactsController(IContactService contactService, ICsvService<ContactModel> csvService)
        {
            _contactService = contactService;
            _csvService = csvService;
        }

        [HttpPost("Csv")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UploadCsv(IFormFile file, CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                await _contactService.SaveContactsToDbAsync(_csvService.ImportEntitiesFromCsvAsync(file.OpenReadStream(), ct), ct);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
