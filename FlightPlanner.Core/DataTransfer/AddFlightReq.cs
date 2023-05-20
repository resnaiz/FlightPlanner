namespace FlightPlanner.Core.DataTransfer
{
    public class AddFlightReq
    {
        public string Carrier { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public AddAirportReq From { get; set; }
        public AddAirportReq To { get; set; }
    }
}
