using Libra.DataAccess.Data;
using Libra.DataAccess.Repository.IRepository;
using Libra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraWebRazor.Pages.Categories
{
    [BindProperties]
    [Area("Admin")]
    public class DeleteModel : PageModel
    {
        public Category category { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int? CategoryId)
        {
            category = _unitOfWork.Category.Get(u => u.CategoryId == CategoryId);
        }
        public IActionResult OnPost()
        {
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
