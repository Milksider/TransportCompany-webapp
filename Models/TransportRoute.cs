using System.ComponentModel.DataAnnotations.Schema;

namespace TransportCompany.Models
{
    public class TransportRoute
    {
        public int Id { get; set; }
        public int StartBusStopId { get; set; }
        public int EndBusStopId { get; set;}
        [ForeignKey("StartBusStopId")]
        public virtual BusStop StartBususStop { get; set; }
        [ForeignKey("EndBusStopId")]
        public virtual BusStop EndBusStop { get; set; }
    }
}
