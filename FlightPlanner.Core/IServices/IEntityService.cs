using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IEntityService<T> : IDbService where T : Entity
    {
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public T GetById(int id);
        IQueryable<T> Query();

    }
}
