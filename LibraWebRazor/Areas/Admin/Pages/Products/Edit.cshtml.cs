
using Libra.DataAccess.Repository.IRepository;
using Libra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraWebRazor.Pages.Products
{
    [BindProperties]
    [Area("Admin")]
    public class EditModel : PageModel
    {
        public Product product { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int? ProductId)
        {
            if (ProductId != null && ProductId != 0)
            {
                product = _unitOfWork.Product.Get(u => u.ProductId == ProductId);
            }
            
        }
        public IActionResult OnPost() 
        {
            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfully";
            return RedirectToPage("Index");
        }
    }
}
