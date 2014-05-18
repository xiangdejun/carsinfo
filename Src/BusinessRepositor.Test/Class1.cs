using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CarsInfo.BusinessRepository_ns;

namespace BusinessRepository.Test
{
    [TestFixture]
    public class BaseRepositoryTests
    {
        [Test]
        public void GetAllBrands_GetAll_ReturnAll()
        {
            BaseRepository baseRepository = new BaseRepository();
            Assert.AreEqual(baseRepository.GetAllBrands().Count, 16);
        }
    }
}
