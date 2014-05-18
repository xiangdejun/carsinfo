using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarsInfo.DALSqlite_ns;
using CarsInfo.IDAL_ns;
using CarsInfo.DALFactory_ns;
using CarsInfo.DataModel_ns;

namespace CarsInfo.BusinessRepository_ns
{
    public class BaseRepository<T> where T:CarBase
    {
        public IBaseOperations<T> baseOperations;

        public BaseRepository()
        {
            var factory = new DalFactory();
            baseOperations = factory.GetFactory();
        }

        public ICollection<T> GetAll()
        {
            return baseOperations.GetAll(); ;
        }   
     


    }
}
