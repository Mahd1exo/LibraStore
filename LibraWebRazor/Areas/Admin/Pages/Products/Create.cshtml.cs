using Libra.DataAccess.Data;
using Libra.DataAccess.Repository.IRepository;
using Libra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraWebRazor.Pages.Products
{
    [Area("Admin")]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public Product product { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost() 
        {
            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();
            TempData["success"] = "Product created successfully";
            return RedirectToPage("Index");
        }
    }
}
