using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;
using InventarioOnline.Models.Specifications;
using InventarioOnline.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventarioOnline.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int pageNumber = 1, string search = "", string searchCurrent = "")
        {
            if (!String.IsNullOrEmpty(search))
            {
                pageNumber = 1;
            }
            else
            {
                search = searchCurrent;
            }
            ViewData["SearchCurrent"] = search;

            if (pageNumber <= 1) { pageNumber = 1; }

            Params param = new Params() { 
            PageNumber = pageNumber,
            PageSize = 4
            };

            var results = _unitOfWork.Producto.GetAllPaged(param);

            if (!String.IsNullOrEmpty(search))
            {
                results = _unitOfWork.Producto.GetAllPaged(param, p => p.Nombre.Contains(search));
            }
            
            SetViewDataPaged(results, pageNumber);

            return View(results);
        }
        void SetViewDataPaged(PagedList<Producto> results, int pageNumber)
        {
            ViewData["TotalPages"] = results.MetaData.TotalPages;
            ViewData["TotalRows"] = results.MetaData.TotalCount;
            ViewData["PageSize"] = results.MetaData.TotalSizes;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previous"] = "disabled"; // clase css para desactivar boton
            ViewData["Next"] = "";

            if (pageNumber > 1) { ViewData["Previous"] = ""; }
            if (results.MetaData.TotalPages <= pageNumber) { ViewData["Next"] = "disabled"; }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}