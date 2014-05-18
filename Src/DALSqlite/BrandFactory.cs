using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CarsInfo.IDAL_ns;
using CarsInfo.DataModel_ns;
using System.Data.SQLite;


namespace CarsInfo.DALSqlite_ns
{
    public class BrandFactory : IBaseOperations<Brand>
    {
        public int InsertOne(Brand model)
        {
            throw new NotImplementedException();
        }

        public int InsertBatch(ICollection<Brand> model)
        {
            throw new NotImplementedException();
        }

        public int DeleteOne(Brand carBase)
        {
            throw new NotImplementedException();
        }

        public int DeleteBatch(ICollection<Brand> model)
        {
            throw new NotImplementedException();
        }

        public int UpdateOne(Brand carBase)
        {
            throw new NotImplementedException();
        }

        public int UpdateBatch(ICollection<Brand> model)
        {
            throw new NotImplementedException();
        }

        public Brand GetOne()
        {
            throw new NotImplementedException();
        }

        public ICollection<Brand> GetAll()
        {
            var dt = SqliteHelper.GetDataTable("Select * from brand");

            var list = (from DataRow dr in dt.Rows
                        select new Brand
                            {
                                Id = int.Parse(dr["bid"].ToString()),
                                Name = dr["bname"].ToString()
                            }).ToList();

            return list;
        }
    }
}
