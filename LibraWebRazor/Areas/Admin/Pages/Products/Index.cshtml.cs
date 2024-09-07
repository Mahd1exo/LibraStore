using Libra.DataAccess.Data;
using Libra.DataAccess.Repository.IRepository;
using Libra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraWebRazor.Pages.Products
{
    [Area("Admin")]
    public class IndexModel : PageModel
    {
        public List<Product> ProductList { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            ProductList = _unitOfWork.Product.GetAll().ToList();
        }
    }
}
