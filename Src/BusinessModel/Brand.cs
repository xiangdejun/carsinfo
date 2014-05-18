namespace CarsInfo.BusinessModel_ns
{
    public abstract class CarBase
    {
        public int Id { get; set; }

        public int Name { get; set; }
    }

    public class Brand : CarBase { }

    public class Factory : CarBase { }

    public class Category : CarBase { }

    public class Serie : CarBase
    {
        public int FactoryId { get; set; }
    }

    public class Model : CarBase
    {
        public int SerieId { get; set; }
    }
}
