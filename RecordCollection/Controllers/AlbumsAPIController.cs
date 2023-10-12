using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordCollection.DataAccess;
using Serilog;

namespace RecordCollection.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumsAPIController : ControllerBase
    {
        private readonly RecordCollectionContext _context;

        public AlbumsAPIController(RecordCollectionContext context, Serilog.ILogger logger)
        {
            _context = context;
        }

        public IActionResult GetAll()
        {
            var albums = _context.Albums.ToList();
            if (albums != null)
            {
                return new JsonResult(albums);
            }
            else
            {
                return new JsonResult("There was an issue, please try again.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int? id)
        {
            int missedEndpoints = 0;
            if (id != null)
            {
                var album = _context.Albums.FirstOrDefault(a => a.Id == id);
                return new JsonResult(album);
            }
            else if (_context.Albums.Where(e => e.Id == id).Any())
            {
                return new JsonResult("That ID does not exist, please try another.");
            }
            else
            {
                Log.Information($"[#{missedEndpoints++}] User's have missed the Albums/GetOne endpoint.");
                return new JsonResult("That endpoint was not found, please check spelling and try again.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOne(int id)
        {
            int errorCount = 0;
            if(id != null)
            {
                try
                {
                    var album = _context.Albums.FirstOrDefault(a => a.Id == id);
                    _context.Albums.Remove(album);
                    _context.SaveChanges();

                    return new JsonResult("Deleted.");
                }
                catch (Exception ex)
                {
                    Log.Fatal($"[#{errorCount++}]DB DELETE FAIL: {ex.Message}");//file and console log
                    return new JsonResult("There was an issue deleting this album, please try again.");
                }
            }
            else
            {
                return new JsonResult("That Album ID was not found, please try another.");
            }
            
        }
    }
}
