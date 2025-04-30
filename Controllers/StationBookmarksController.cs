using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EDStationDatabase.Models.ViewModels;
using EDStationDatabase.Models;

namespace EDStationDatabase.Controllers
{
    public class StationBookmarksController : Controller
    {
        private EDAssetsContext Context { get; set; }

        public StationBookmarksController(EDAssetsContext ctx)
        {
            Context = ctx;
        }

        [HttpGet]
        [Route("stationbookmarks/bookmarks/delete/{id}/")]
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
        [Route("stationbookmarks/bookmarks/delete/")]
        public IActionResult Delete(StationBookmark bookmark)
        {
            var bookmarkToDelete = Context.StationBookmarks.Find(bookmark.StationBookmarkId);
            if (bookmarkToDelete == null)
            {
                return NotFound();
            }

            Context.StationBookmarks.Remove(bookmarkToDelete);
            Context.SaveChanges();

            return RedirectToAction("Bookmarks");
        }

        [HttpGet]
        [Route("stationbookmarks/bookmarks/add/")]
        public IActionResult Add()
        {
            ViewBag.Action = "Add Station Bookmark";
            ViewBag.Economies = Context.Economies.OrderBy(g => g.EconomyName).ToList();
            ViewBag.Allegiances = Context.Allegiances.OrderBy(a => a.AllegianceName).ToList();
            ViewBag.StationTypes = Context.StationType.OrderBy(s => s.StationTypeName).ToList();
            return View("Edit", new StationBookmark());
        }

        [HttpGet]
        [Route("stationbookmarks/bookmarks/edit/{id}/")]
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
        [Route("stationbookmarks/bookmarks/edit/")]
        public IActionResult Edit(StationBookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                if (bookmark.StationBookmarkId == 0)
                {
                    Context.StationBookmarks.Add(bookmark);
                }
                else
                {
                    Context.StationBookmarks.Update(bookmark);
                }

                Context.SaveChanges();
                return RedirectToAction("Bookmarks");
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
        [Route("stationbookmarks/bookmarks/")]
        public IActionResult Bookmarks(int page = 1, int pageSize = 10)
        {
            var totalCount = Context.StationBookmarks.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var bookmarks = Context.StationBookmarks
                .Include(m => m.Economy)
                .Include(m => m.Allegiance)
                .Include(m => m.StationType)
                .OrderBy(m => m.StationName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var allegianceSuperpowerMapping = new Dictionary<int, string>
            {
                { 1, "Empire" }, { 2, "Empire" }, { 3, "Empire" }, { 4, "Empire" },
                { 5, "Federation" }, { 6, "Federation" },
                { 7, "Alliance" }, { 8, "Alliance" },
                { 9, "Independent" }, { 10, "Independent" }, { 11, "Independent" }, { 12, "Independent" },
                { 13, "Independent" }, { 14, "Independent" }
            };

            var viewModel = new BookmarkViewModel
            {
                StationBookmark = bookmarks,
                AllegianceWithSuperpower = Context.Allegiances
                    .ToDictionary(a => a.AllegianceId,
                                  a => allegianceSuperpowerMapping.ContainsKey(a.AllegianceId)
                                      ? allegianceSuperpowerMapping[a.AllegianceId]
                                      : "Unknown"),
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(viewModel);
        }
    }
}