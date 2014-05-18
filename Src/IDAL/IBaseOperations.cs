using System.Collections.Generic;

namespace CarsInfo.IDAL_ns
{
    public interface IBaseOperations<T>
    {
        int InsertOne(T model);
        int InsertBatch(ICollection<T> model);

        int DeleteOne(T model);
        int DeleteBatch(ICollection<T> model);

        int UpdateOne(T model);
        int UpdateBatch(ICollection<T> model);

        T GetOne();
        ICollection<T> GetAll();
    }
}