using Libra.DataAccess.Repository.IRepository;
using Libra.Models;
using Libra.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product>objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
            .GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()

            });
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new ProductVM()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {
            if (ModelState.IsValid) {
                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index"); 
            }
            else
            {
                obj.CategoryList=_unitOfWork.Category.GetAll().Select(u=> 
                new SelectListItem()
                {
                   Text=u.Name,
                    Value = u.CategoryId.ToString()
                }); 
                return View(obj);
            }
           
         }
        public IActionResult Edit(int? ProductId)
        {
            if (ProductId == null || ProductId <= 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.ProductId == ProductId);
            //Product? productFromDb= _db.Categories.FirstOrDefault(u=>u.ProductId == ProductId);
            //Product? productFromDb= _db.Categories.Where(u=>u.ProductId == ProductId).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? ProductId)
        {
            if (ProductId == null || ProductId <= 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.ProductId == ProductId);
            //Product? productFromDb= _db.Categories.FirstOrDefault(u=>u.ProductId == ProductId);
            //Product? productFromDb= _db.Categories.Where(u=>u.ProductId == ProductId).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? ProductId)
        {
            Product? productFromDb = _unitOfWork.Product.Get(u => u.ProductId == ProductId);
            if (productFromDb == null) { return NotFound(); }
            _unitOfWork.Product.Remove(productFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
