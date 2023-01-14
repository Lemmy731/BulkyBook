using BulkyBookWeb.Date;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> result = _appDbContext.Categories;
            return View(result);
        }
        // GET: Jokes/ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View();
        }
        //Post: Category/ShowSearchResult
        public IActionResult ShowSearchResult(string SearchPhrase)
        {
            
            return View("Index", _appDbContext.Categories.Where(x => x.Name.Contains(SearchPhrase)).OrderBy(x => x.Name).ToList());
        }
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _appDbContext.Categories.Add(obj);
                _appDbContext.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);  
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryfromDb = _appDbContext.Categories.Find(id);
            if (categoryfromDb == null)
            {
                return NotFound();  
            }

            return View(categoryfromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _appDbContext.Categories.Update(obj);
                _appDbContext.SaveChanges();
                 TempData["success"] = "Category update successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryfromDb = _appDbContext.Categories.Find(id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }

            return View(categoryfromDb);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _appDbContext.Categories.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }
            _appDbContext.Categories.Remove(obj);
            _appDbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");    
        }
    }
}
