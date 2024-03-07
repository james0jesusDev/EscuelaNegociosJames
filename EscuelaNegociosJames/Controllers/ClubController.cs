using EscuelaNegociosJames.DbContext;
using EscuelaNegociosJames.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EscuelaNegociosJames.Controllers
{
    public class ClubController : Controller
    {
        Context _db;

        public ClubController(Context db)
        {
            _db = db;
        }

        public async Task<IActionResult> AllClub()
        {
            var clubs = await _db.Clubs.Include(c => c.Departamento).ToListAsync();
            return View(clubs);
        }
        public async Task<IActionResult> AddClub()
        {
            var departamentoDisplay = await _db.Departamentos.Select(x => new
            {
                Id = x.DepartamentoId,
                Value = x.DepartmentName
            }).ToListAsync();

            ClubAddClubViewModel vm = new ClubAddClubViewModel();
            vm.DepartamentoList = new SelectList(departamentoDisplay, "Id", "Value");

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddClub(ClubAddClubViewModel vm)
        {
            var departamento = await _db.Departamentos.
                SingleOrDefaultAsync(i => i.DepartamentoId == vm.Departamento.DepartamentoId);

            vm.Club.Departamento = departamento;
            _db.Add(vm.Club);
            await _db.SaveChangesAsync();
            return RedirectToAction("AllClub");
        }

    }
}
// hacer el controller de students , hacer las vistas de EnrollCourse
// y las de AllClassmate , AllStudent