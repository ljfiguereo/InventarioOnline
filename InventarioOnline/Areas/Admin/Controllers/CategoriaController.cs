using InventarioOnline.DataAccess.Repository.IRepository;
using InventarioOnline.Models.Inventario;
using InventarioOnline.Utils;
using Microsoft.AspNetCore.Mvc;

namespace InventarioOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var entity = new Categoria();
            if (id == null)
            {
                // Agregar nueva Categoria
                entity.Estado = true;
                return View(entity);
            }
            //Actualizar Categoria
            entity = await _unitOfWork.Categoria.GetById(id.Value);
            if (entity == null)
                return NotFound();
            
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Categoria entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entity.Nombre = entity.Nombre.ToUpper();
                    if (entity.Id == 0)
                    {
                        await _unitOfWork.Categoria.Add(entity);
                        TempData[DS.Success] = "Categoria creada correctamente";
                    }
                    else
                    {
                        _unitOfWork.Categoria.Update(entity);
                        TempData[DS.Success] = "Categoria actualizada correctamente";
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
            var list = await _unitOfWork.Categoria.GetAll();

            return Json(new { data = list }); 
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _unitOfWork.Categoria.GetById(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Error al borrar la Categoria" });
            }
            _unitOfWork.Categoria.Delete(entity);
            await _unitOfWork.Save();

            return Json(new { success = true, message = "Categoria borrada Correctamente" });
        }

        [ActionName("ValidaNombre")]
        public async Task<IActionResult> ValidaNombre(string nombre, int id = 0)
        {
            bool value = false;
            var list = await _unitOfWork.Categoria.GetAll(x=> x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());

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
