using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsInfo.DataModel_ns
{
    public class CarsCategory
    {
        public int BrandId { get; set; }
        public string BeginLetter { get; set; }
        public string BrandName { get; set; }

        private List<CarShort> cars;

        public List<CarShort> Cars
        {
            get
            {
                if (cars == null)
                {
                    cars = new List<CarShort>();
                }
                return cars;
            }
            set { cars = value; }
        }
    }

    public class CarShort
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string RetailPrice { get; set; }

        public string FactoryName { get; set; }

        public string ClassName { get; set; }

        public override string ToString()
        {
            return CarName + " (" + RetailPrice + ") ";
        }
    }
}
