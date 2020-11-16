using System;
using OOP_EindOpdracht.Classes;

namespace OOP_EindOpdracht
{
    class Program
    {
        static void Main(string[] args)
        {          
            //Limousine newLimo = AutoAdministratie.AddLimousine("Volvo", "Truck7", 2001, "72-NS-HH", 0, true);

            //Auto[] autos = AutoAdministratie.GetAllAutos();

            //if (autos != null)
            //{
            //    for (int i = 0; i < autos.Length; i++)
            //    {
            //        Console.WriteLine(autos[i].ToString());
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No autos in Database");
            //}
            Console.WriteLine("Press enter to create new Truck");
            Console.ReadLine();

            Truck newTruck = AutoAdministratie.AddTruck("Volvo", "Truck7", 2001, "72-NS-HH", 0, true);

            Console.WriteLine(AutoAdministratie.GetByID(newTruck.ID));
            Console.WriteLine("Press enter to hire");
            Console.ReadLine();

            if (AutoAdministratie.HuurAuto(newTruck.ID))
            {
                Console.WriteLine("Hired ID: " + newTruck.ID);
            }
            else
            {
                Console.WriteLine(newTruck.ID + " was already hired");
            }

            Console.WriteLine(AutoAdministratie.GetByID(newTruck.ID));
            Console.WriteLine("Press enter to get costs");
            Console.ReadLine();

            Console.WriteLine("Ingeleverd! Costs are: " + AutoAdministratie.LeverIn(newTruck.ID, 50));

            Console.WriteLine(AutoAdministratie.GetByID(newTruck.ID));
            Console.WriteLine("Press enter to clean");
            Console.ReadLine();

            if (AutoAdministratie.MaakSchoon(newTruck.ID))
            {
                Console.WriteLine("Car cleaned and set for hire");
            }
            else
            {
                Console.WriteLine("Car was already cleaned");
            }

            Console.WriteLine(AutoAdministratie.GetByID(newTruck.ID));
            Console.WriteLine("Press enter to delete");
            Console.ReadLine();

            AutoAdministratie.RemoveAuto(newTruck.ID);
        }
    }
}
