using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new CompanyModel());
            }
            else
            {
                CompanyModel companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                if (companyObj == null)
                {
                    companyObj = new CompanyModel();
                }
                return View(companyObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(CompanyModel CompanyObj)
        {
            if (ModelState.IsValid)
            {
                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }
                _unitOfWork.Save();
                if (CompanyObj.Id == 0)
                {
                    TempData["success"] = "Company Created Successfully";
                }
                else
                {
                    TempData["success"] = "Company Updated Successfully";
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyObj);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<CompanyModel> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id != null && id > 0)
            {
                CompanyModel companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);

                if (companyToBeDeleted != null)
                {
                    _unitOfWork.Company.Remove(companyToBeDeleted);
                    _unitOfWork.Save();

                    return Json(new { success = true, message = "Delete Successful" });
                }
                return Json(new { success = false, message = "Error while deleting" });
            }
            return Json(new { success = false, message = "Not found" });
        }
        #endregion
    }
}
