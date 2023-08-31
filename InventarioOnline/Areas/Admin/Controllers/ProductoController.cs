using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;
using InventarioOnline.Models.ViewModels;
using InventarioOnline.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Packaging.Signing;

namespace InventarioOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductoController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        ProductoVM InitViewModel(Producto entity = null)
        {
            var productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _unitOfWork.Producto.GetAllDropownList("Categoria"),
                MarcaLista = _unitOfWork.Producto.GetAllDropownList("Marca"),
                ParentLista = _unitOfWork.Producto.GetAllDropownList("Producto")
            };
            if (entity != null)
            {
                productoVM.Producto = entity;
            }

            return productoVM;
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            var productoVM = InitViewModel();

            if (id == null)
            {
                productoVM.Producto.Estado = true;
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = await _unitOfWork.Producto.GetById(id.GetValueOrDefault());
                if (productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }
        }

        string AddProducto(IFormFileCollection files, string webRootPath, bool isNewItem = false, Producto entity = null)
        {
            string upload = webRootPath + DS.urlProductos;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            if (isNewItem!)
            {
                var fileNameAnterior = Path.Combine(upload, entity.ImagenUrl);
                if (System.IO.File.Exists(fileNameAnterior))
                {
                    System.IO.File.Delete(fileNameAnterior);
                }
            }

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            return fileName + extension;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productoVM.Producto.Id == 0)
                {
                    // Crear producto
                    var fileName = AddProducto(files,webRootPath);

                    productoVM.Producto.ImagenUrl = fileName;
                    await _unitOfWork.Producto.Add(productoVM.Producto);
                }
                else
                {
                    var entity = _unitOfWork.Producto.GetFirst(x => x.Id == productoVM.Producto.Id, isTracking: false).Result;
                    if (files.Count>0)// Si es una nueva imagen
                    {
                        var fileName = AddProducto(files,webRootPath);
                        
                        productoVM.Producto.ImagenUrl = fileName;
                    }
                    else
                        productoVM.Producto.ImagenUrl = entity.ImagenUrl;
                    
                    _unitOfWork.Producto.Update(productoVM.Producto);
                }
                TempData[DS.Success] = "Producto Guardado Correctamente";
                await _unitOfWork.Save();

                return View("Index");
            }
            TempData[DS.Error] = "Modelo es Invalido";
            productoVM = InitViewModel(productoVM.Producto);

            return View(productoVM);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var list = await _unitOfWork.Producto.GetAll(includeProperties: "Categoria,Marca");

            return Json(new { data = list }); 
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _unitOfWork.Producto.GetById(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Error al borrar el Producto" });
            }

            DeleteImage(entity.ImagenUrl);

            _unitOfWork.Producto.Delete(entity);
            await _unitOfWork.Save();

            return Json(new { success = true, message = "Producto borrado Correctamente" });
        }
        void DeleteImage(string fileName)
        {
            var url = _webHostEnvironment.WebRootPath + DS.urlProductos;
            var file = Path.Combine(url, fileName);

            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }
        }
        [ActionName("ValidaSerie")]
        public async Task<IActionResult> ValidaSerie(string serie, int id = 0)
        {
            bool value = false;
            var list = await _unitOfWork.Producto.GetAll(x=> x.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim());

            if (id == 0)
                value = list.Any();
            else
                value = list.Any(x => x.Id != id);

            return Json(new { data = value });
        }
        #endregion
    }
}
