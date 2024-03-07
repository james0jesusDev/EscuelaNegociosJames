using EscuelaNegociosJames.DbContext;
using EscuelaNegociosJames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EscuelaNegociosJames.Controllers
{
    public class DepartamentoController : Controller
    {
        Context _db;
        public DepartamentoController(Context db)
        {
            _db = db;
        }

        public async Task<IActionResult> AllDepartamento()
        {
            var departamentos = await _db.Departamentos.ToListAsync();
            return View(departamentos);
        }


        public IActionResult AddDepartamento()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartamento(Departamento departamento)
        {
            _db.Add(departamento);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllDepartamento");
        }

        // aqui falta el de departamentos de detalles 

        public async Task<IActionResult> DepartamentoDetails(int id)
        {
            Departamento departamento = await _db.Departamentos.
                SingleOrDefaultAsync(i => i.DepartamentoId == id);
            return View(departamento);
        }

    }
}
