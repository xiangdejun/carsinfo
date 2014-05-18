using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarsInfo.DALSqlite_ns;
using CarsInfo.DataModel_ns;
using CarsInfo.DALFactory_ns;
using CarsInfo.IDAL_ns;

namespace CarsInfo.BusinessRepository_ns
{
    public class BrandRepository
    {
        private static IBaseOperations<Brand> _brand;

        public BrandRepository()
        {
            var factory = new DalFactory();
            _brand = factory.GetBrandFactory();
        }

        public static ICollection<Brand> GetAll()
        {
            _brand =  new BrandFactory();

            return _brand.GetAll();
        }
    }
}
