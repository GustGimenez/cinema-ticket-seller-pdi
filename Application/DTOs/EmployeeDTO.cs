namespace Application.DTOs
{
    public class EmployeeDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public DateTime BirthDate { get; set; }

        public long MovieTheaterId { get; set; }
        
        public bool Active { get; set; }
    }
}