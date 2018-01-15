using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityDemo
{
    class Car
    {
        private IDriver _driver;

        public Car(IDriver driver)
        {
            _driver = driver;
        }

        public void Drive()
        {
            _driver.Distance += 1;
        }

        public int GetDistanceForCurrentDriver()
        {
            return _driver.Distance;
        }
    }
}
