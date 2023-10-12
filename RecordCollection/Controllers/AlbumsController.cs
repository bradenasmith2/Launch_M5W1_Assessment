using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RecordCollection.DataAccess;
using RecordCollection.Models;
using Serilog;

namespace RecordCollection.Controllers
{
    public class AlbumsController : Controller {

        private readonly RecordCollectionContext _context;
        private readonly Serilog.ILogger _logger;

        public AlbumsController(RecordCollectionContext context, Serilog.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var albums = _context.Albums.ToList();
            if(albums == null)
            {
                return NotFound("Albums Not Found.");
            }
            return View(albums);
        }

        [Route("/albums/{id:int}")]
        public IActionResult Show(int? id)
        {
            int errorCount = 0;//count the total times this error is recieved
            if (id != null)//nullable
            {
                try
                {
                    var album = _context.Albums.FirstOrDefault(a => a.Id == id);

                    return View(album);
                }
                catch (Exception ex)
                {
                    Log.Information($"[#{errorCount++}] - {ex.Message}");//file and console log
                    return NotFound(ex.Message);
                }
            }
            else//(if id == null)
            {
                return NotFound("That Album ID was not found, please try another.");
            }
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Album album)//nullable
        {
            int failedCreate = 0;
            int errorCount = 0;//count the total times this error is recieved
            if(!ModelState.IsValid)
            {
                Log.Warning($"[#{failedCreate++}] Object ModelState was invalid.");
                return NotFound();
            }
            else
            {
                try
                {
                    _context.Albums.Add(album);
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    Log.Fatal($"[#{errorCount++}]DB SAVE FAIL: {ex.Message}");//file and console log
                }

                return Redirect("/albums");
            }
        }

        [HttpPost]
        [Route("/albums/{id:int}")]
        public IActionResult Delete(int? id)
        {
            int errorCount = 0;
                if(id != null)
                {
                    try 
                    {
                        var album = _context.Albums.FirstOrDefault(a => a.Id == id);
                        _context.Albums.Remove(album);
                        _context.SaveChanges();

                        Log.Information($"Success! {album.Title} was removed from the database.");//file and console log (not mine)
                    return Redirect("/albums");
                    }
                    catch (Exception ex)
                    {
                        Log.Fatal($"[#{errorCount++}]DB DELETE FAIL: {ex.Message}");//file and console log
                    return NotFound("There was an issue deleting this album, Please try again.");
                    }
                }
                else
                {
                    return NotFound("That Album ID was not found, please try another.");
                }
        }
    }
}