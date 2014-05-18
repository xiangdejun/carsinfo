using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CarsInfo.IDAL_ns;
using CarsInfo.DALSqlite_ns;
using CarsInfo.DataModel_ns;

namespace CarsInfo.DALFactory_ns
{
    public class DalFactory<T>
    {
        public IBaseOperations<T>  brandFactory;

        public DalFactory()
        {
            this.brandFactory = GetBrandFactory(); // =  new BrandFactory()
        }

        public IBaseOperations<T> GetBrandFactory()
        {
            return brandFactory;
        }

        public IBaseOperations<T> GetFactory()
        {
            return brandFactory;
        }
    }


}
