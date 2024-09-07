using Libra.DataAccess.Data;
using Libra.DataAccess.Repository.IRepository;
using Libra.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", "The Display Order cannot exactly match the Name.");
            }

            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("Name", $"{obj.Name} is an invalid value.");
            }

            if (obj.Name != null && !System.Text.RegularExpressions.Regex.IsMatch(obj.Name.ToLower(), "[a-z]"))
            {
                ModelState.AddModelError("name", "The Name should contain at least one character from a-z.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? CategoryId)
        {
            if (CategoryId == null || CategoryId <= 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.CategoryId == CategoryId);
            //Category? categoryFromDb= _db.Categories.FirstOrDefault(u=>u.CategoryId == CategoryId);
            //Category? categoryFromDb= _db.Categories.Where(u=>u.CategoryId == CategoryId).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("", "The Display Order cannot exactly match the Name.");
            }

            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("Name", $"{obj.Name} is an invalid value.");
            }

            if (obj.Name != null && !System.Text.RegularExpressions.Regex.IsMatch(obj.Name.ToLower(), "[a-z]"))
            {
                ModelState.AddModelError("name", "The Name should contain at least one character from a-z.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? CategoryId)
        {
            if (CategoryId == null || CategoryId <= 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.CategoryId == CategoryId);
            //Category? categoryFromDb= _db.Categories.FirstOrDefault(u=>u.CategoryId == CategoryId);
            //Category? categoryFromDb= _db.Categories.Where(u=>u.CategoryId == CategoryId).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? CategoryId)
        {
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.CategoryId == CategoryId);
            if (categoryFromDb == null) { return NotFound(); }
            _unitOfWork.Category.Remove(categoryFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
