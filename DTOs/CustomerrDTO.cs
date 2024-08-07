namespace cinema_ticket_seller_pdi.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Document { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Active { get; set; }
    }
}