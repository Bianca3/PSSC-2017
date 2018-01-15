using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace UnityDemo
{
    class Program
    {
        private static UnityContainer _container = new UnityContainer();

        static void Main(string[] args)
        {
            _container = new UnityContainer();
            RegisterDriver();
            
            var car = _container.Resolve<Car>();
            car.Drive();
            Console.WriteLine("Current distance is {0}!", car.GetDistanceForCurrentDriver());
            Console.ReadLine();
        }

        static void RegisterDriver()
        {
            _container.RegisterType<IDriver, RaceDriver>();
            //_container.RegisterType<IDriver, TaxiDriver>();
            //_container.RegisterType<IDriver, RaceDriver>();

        }
    }
}
