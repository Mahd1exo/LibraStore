
using Libra.DataAccess.Repository.IRepository;
using Libra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraWebRazor.Pages.Categories
{
    [BindProperties]
    [Area("Admin")]
    public class EditModel : PageModel
    {
        public Category category { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int? CategoryId)
        {
            if (CategoryId != null && CategoryId != 0)
            {
                category = _unitOfWork.Category.Get(u => u.CategoryId == CategoryId);
            }
            
        }
        public IActionResult OnPost() 
        {
            _unitOfWork.Category.Update(category);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToPage("Index");
        }
    }
}
