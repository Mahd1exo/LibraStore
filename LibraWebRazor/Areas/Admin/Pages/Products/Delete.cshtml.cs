using Libra.DataAccess.Data;
using Libra.DataAccess.Repository.IRepository;
using Libra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraWebRazor.Pages.Products
{
    [BindProperties]
    [Area("Admin")]
    public class DeleteModel : PageModel
    {
        public Product product { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int? ProductId)
        {
            product = _unitOfWork.Product.Get(u => u.ProductId == ProductId);
        }
        public IActionResult OnPost()
        {
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
