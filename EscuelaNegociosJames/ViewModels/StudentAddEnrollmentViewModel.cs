using EscuelaNegociosJames.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscuelaNegociosJames.ViewModels
{
    public class StudentAddEnrollmentViewModel
    {
        public Enrollment Enrollment { get; set; }
        public SelectList StudentList { get; set; }
    }
}
