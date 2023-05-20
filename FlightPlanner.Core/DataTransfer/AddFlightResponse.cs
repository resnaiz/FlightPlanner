namespace FlightPlanner.Core.DataTransfer
{
    public class AddFlightResponse
    {
        public int Id { get; set; }
        public AddAirportResponse To { get; set; }
        public AddAirportResponse From { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
