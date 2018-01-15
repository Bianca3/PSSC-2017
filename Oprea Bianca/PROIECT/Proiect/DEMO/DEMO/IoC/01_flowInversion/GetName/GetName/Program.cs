using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetName
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is your first name? ");
            var firstName = Console.ReadLine();

            Console.WriteLine("What is your last name? ");
            var lastName = Console.ReadLine();


            Console.WriteLine("Hello {0} {1}.", firstName, lastName);
            Console.ReadLine();
        }
    }
}
