namespace PROJECT_S19.DTOs.Ticket
{
    public class GetTicketRequestDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
