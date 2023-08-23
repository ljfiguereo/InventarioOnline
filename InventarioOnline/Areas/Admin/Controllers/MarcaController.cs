using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;
using InventarioOnline.Utils;
using Microsoft.AspNetCore.Mvc;

namespace InventarioOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MarcaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var entity = new Marca();
            if (id == null)
            {
                // Agregar nueva Marca
                entity.Estado = true;
                return View(entity);
            }
            //Actualizar Marca
            entity = await _unitOfWork.Marca.GetById(id.Value);
            if (entity == null)
                return NotFound();
            
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Marca entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entity.Nombre = entity.Nombre.ToUpper();
                    if (entity.Id == 0)
                    {
                        await _unitOfWork.Marca.Add(entity);
                        TempData[DS.Success] = "Marca creada correctamente";
                    }
                    else
                    {
                        _unitOfWork.Marca.Update(entity);
                        TempData[DS.Success] = "Marca actualizada correctamente";
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
            var list = await _unitOfWork.Marca.GetAll();

            return Json(new { data = list }); 
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _unitOfWork.Marca.GetById(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Error al borrar la Marca" });
            }
            _unitOfWork.Marca.Delete(entity);
            await _unitOfWork.Save();

            return Json(new { success = true, message = "Marca borrada Correctamente" });
        }

        [ActionName("ValidaNombre")]
        public async Task<IActionResult> ValidaNombre(string nombre, int id = 0)
        {
            bool value = false;
            var list = await _unitOfWork.Marca.GetAll(x=> x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());

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
