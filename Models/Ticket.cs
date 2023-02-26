namespace TransportCompany.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        //дата поездки
        public DateTime DateOfTrip { get; set; }
        //номер рейса
        public int TripId { get; set; }
        public Trip trip { get; set; }
        //Статус, отвечающий за подтверждение покупки билета диспетчером
        public int Status { get; set; }
    }
}
