namespace TransportCompany.Models
{
    //Модель рейса
    public class Trip
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public TransportRoute route { get; set; }
        public int DriverId { get; set; }
        public Driver driver { get; set; }
        public int ConductorId { get; set; }
        public Conductor conductor { get; set; }
        public int TransportId { get; set; }
        public Transport transport { get; set; }
        public DateTime TripTime { get; set; }
    }
}
