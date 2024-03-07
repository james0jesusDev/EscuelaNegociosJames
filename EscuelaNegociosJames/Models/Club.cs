namespace EscuelaNegociosJames.Models
{
    public class Club
    {
        public int ClubID { get; set; }
        public string? ClubName { get; set; }
        public int NumberOfStudents { get; set; }
        public int DepartmentID { get; set; }
        public virtual Departamento? Departamento { get; set; }

    }
}
