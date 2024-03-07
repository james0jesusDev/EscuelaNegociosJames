namespace EscuelaNegociosJames.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public virtual Student? Student { get; set; }

        public virtual Club? Club { get; set; }
    }
}
