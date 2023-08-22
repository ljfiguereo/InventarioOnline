using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;
using InventarioOnline.Utils;
using Microsoft.AspNetCore.Mvc;

namespace InventarioOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlmacenController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AlmacenController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var almacen = new Almacen();
            if (id == null)
            {
                // Agregar nuevo almacen
                almacen.Estado = true;
                return View(almacen);
            }
            //Actualizar almacen
            almacen = await _unitOfWork.Almacen.GetById(id.Value);
            if (almacen == null)
                return NotFound();
            
            return View(almacen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Almacen entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entity.Nombre = entity.Nombre.ToUpper();
                    if (entity.Id == 0)
                    {
                        await _unitOfWork.Almacen.Add(entity);
                        TempData[DS.Success] = "Almacen creado correctamente";
                    }
                    else
                    {
                        _unitOfWork.Almacen.Update(entity);
                        TempData[DS.Success] = "Almacen actualizado correctamente";
                    }
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                        TempData[DS.Error] = "Error al guardar los cambios";
                }
                return RedirectToAction(nameof(Index));
            }

            return View(entity);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var list = await _unitOfWork.Almacen.GetAll();

            return Json(new { data = list }); 
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _unitOfWork.Almacen.GetById(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Error al borrar el Almacen" });
            }
            _unitOfWork.Almacen.Delete(entity);
            await _unitOfWork.Save();

            return Json(new { success = true, message = "Almacen borrado Correctamente" });
        }

        [ActionName("ValidaNombre")]
        public async Task<IActionResult> ValidaNombre(string nombre, int id = 0)
        {
            bool value = false;
            var list = await _unitOfWork.Almacen.GetAll(x=> x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());

            if (id == 0)
            {
                value = list.Any();
            }
            else
            {
                value = list.Any(x => x.Id != id);
            }

            return Json(new { data = value });
        }
        #endregion
    }
}
