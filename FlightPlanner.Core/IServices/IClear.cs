using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IClear : IDbService
    {
        public void Clear<T>() where T : Entity;
    }
}
