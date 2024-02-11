using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        //private readonly ApplicationDbContext _db;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }
        public IActionResult Index()
        {
            List<CategoryModel> objCategoryList = _categoryRepo.GetAll().ToList();  //_db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryModel obj)
        {
            // Custom Validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name cannot exactly match the Display Order.");
            }
            //if (obj.Name != null && obj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //}
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);    //_db.Categories.Add(obj);
                _categoryRepo.Save();    //_db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Different approaches to retrieve data from db, but the most prefered one is FirstOrDefault()
            CategoryModel? categoryFromDb = _categoryRepo.Get(u => u.Id == id);    //_db.Categories.Find(id);
            //CategoryModel? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //CategoryModel? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(CategoryModel obj)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(obj);    //_db.Categories.Update(obj);
                _categoryRepo.Save();    //_db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            CategoryModel? categoryFromDb = _categoryRepo.Get(u => u.Id == id);    //_db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            CategoryModel? obj = _categoryRepo.Get(u => u.Id == id);   //_db.Categories.FirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);    //_db.Categories.Remove(obj);
            _categoryRepo.Save();    //_db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
