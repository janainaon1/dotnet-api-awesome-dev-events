using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;

        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }

        // GET: api/dev-events
        [HttpGet]
        public IActionResult GetAll() 
        {
            var devEvents = _context.DevEvents.Where(d => !d.IsDeleted).ToList();
            return Ok(devEvents);
        }

        // GET: api/dev-events/1234545454
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id) 
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id && !d.IsDeleted);

            if (devEvent == null)
            {
                return NotFound();
            }

            return Ok(devEvent);
        }

        // POST: api/dev-events
        [HttpPost]
        public IActionResult Post(DevEvent devEvent) 
        {
            _context.DevEvents.Add(devEvent);
            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);

        }

        // PUT: api/dev-events/1234545454
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, DevEvent devEventInput)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(devEventInput.Title, devEventInput.Description, devEventInput.StartDate, devEventInput.EndDate);

            return NoContent();
        }

        // DELETE: api/dev-events/1234545454
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) 
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();

            return NoContent();
        }

        // POST: api/dev-events/1234545454/speakers
        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, DevEventSpeaker speaker)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Speakers.Add(speaker);

            return NoContent();
        }
    }
}
