using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EDStationDatabase.Models;

namespace EDStationDatabase.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/stationbookmarks/bookmarks")]
    public class StationBookmarksController : Controller
    {
        private EDAssetsContext Context { get; set; }

        public StationBookmarksController(EDAssetsContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        [Route("delete/{id}/")]
        public IActionResult Delete(int id)
        {
            var bookmark = Context.StationBookmarks.Find(id);
            if (bookmark == null)
            {
                return NotFound();
            }
            return View(bookmark);
        }

        [HttpPost]
        [Route("delete/{id}")]
        public IActionResult Delete(StationBookmark bookmark)
        {
            var bookmarkToDelete = Context.StationBookmarks.Find(bookmark.StationBookmarkId);
            if (bookmarkToDelete == null)
            {
                return NotFound();
            }

            Context.StationBookmarks.Remove(bookmarkToDelete);
            Context.SaveChanges();

            TempData["SuccessMessage"] = "Station bookmark deleted successfully.";
            return RedirectToAction("Bookmarks", new { area = "Admin" });
        }

        [HttpGet]
        [Route("add/")]
        public IActionResult Add()
        {
            ViewBag.Action = "Add Station Bookmark";
            ViewBag.Economies = Context.Economies.OrderBy(g => g.EconomyName).ToList();
            ViewBag.Allegiances = Context.Allegiances.OrderBy(a => a.AllegianceName).ToList();
            ViewBag.StationTypes = Context.StationType.OrderBy(s => s.StationTypeName).ToList();
            return View("Edit", new StationBookmark());
        }

        [HttpGet]
        [Route("edit/{id}/")]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit Station Bookmark";
            ViewBag.Economies = Context.Economies.OrderBy(g => g.EconomyName).ToList();
            ViewBag.Allegiances = Context.Allegiances.OrderBy(a => a.AllegianceName).ToList();
            ViewBag.StationTypes = Context.StationType.OrderBy(s => s.StationTypeName).ToList();

            var bookmark = Context.StationBookmarks.Find(id);

            return View(bookmark);
        }

        [HttpPost]
        [Route("edit/{id}/")]
        public IActionResult Edit(StationBookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                if (bookmark.StationBookmarkId == 0)
                {
                    Context.StationBookmarks.Add(bookmark);
                    TempData["SuccessMessage"] = "Station bookmark added successfully.";
                }
                else
                {
                    Context.StationBookmarks.Update(bookmark);
                    TempData["SuccessMessage"] = "Station bookmark updated successfully.";
                }

                Context.SaveChanges();
                return RedirectToAction("Bookmarks", new { area = "Admin" });
            }
            else
            {
                ViewBag.Economies = Context.Economies.OrderBy(g => g.EconomyName).ToList();
                ViewBag.Allegiances = Context.Allegiances.OrderBy(a => a.AllegianceName).ToList();
                ViewBag.StationTypes = Context.StationType.OrderBy(s => s.StationTypeName).ToList();
                return View(bookmark);
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult Bookmarks()
        {
            var bookmarks = Context.StationBookmarks
                .Include(m => m.Economy)
                .Include(m => m.Allegiance)
                .Include(m => m.StationType)
                .OrderBy(m => m.StationName)
                .ToList();

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View(bookmarks);
        }
    }
}