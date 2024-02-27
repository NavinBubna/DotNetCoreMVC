using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<ProductModel> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
            //    .GetAll().Select(u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });

            ////ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new ProductModel()
            };

            if (id == null || id == 0)
            {
                // insert or create
                return View(productVM);
            }
            else
            {
                // update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id != null && id > 0)
            {
                ProductModel productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

                if (productFromDb != null)
                {
                    return View(productFromDb);
                }
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id != null && id > 0)
            {
                ProductModel productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

                if (productFromDb != null)
                {
                    _unitOfWork.Product.Remove(productFromDb);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Deleted Successfully";
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
