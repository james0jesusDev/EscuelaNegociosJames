using EscuelaNegociosJames.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscuelaNegociosJames.ViewModels
{
    public class ClubAddClubViewModel
    {
        public Club? Club { get; set; }
        public Departamento? Departamento { get; set; }
        public SelectList? DepartamentoList { get; set; }
    }
}
