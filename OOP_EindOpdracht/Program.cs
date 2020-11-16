using System;
using OOP_EindOpdracht.Classes;

namespace OOP_EindOpdracht
{
    class Program
    {
        static void Main(string[] args)
        {
            //Truck newTruck = AutoAdministratie.AddTruck("Volvo","Truck7",2001,"72-NS-HH",0,true);
            //Limousine newLimo = AutoAdministratie.AddLimousine("Volvo", "Truck7", 2001, "72-NS-HH", 0, true);

            Auto[] autos = AutoAdministratie.GetAllAutos();

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

            Auto auto = AutoAdministratie.GetByID(autos[0].ID);
            Console.WriteLine(auto.ToString());

            if (auto.Huur())
            {
                Console.WriteLine("Hired ID: " + auto.ID);
            }
            else
            {
                Console.WriteLine(auto.ID + " was already hired");
            }

            Console.WriteLine(auto.ToString());

            Console.WriteLine("Costs are: " + auto.LeverIn(50));

            Console.WriteLine(auto.ToString());

            if (auto.Schoonmaak())
            {
                Console.WriteLine("Car cleaned and set for hire");
            }
            else
            {
                Console.WriteLine("Car was already cleaned");
            }

            Console.WriteLine(auto.ToString());

            //AutoAdministratie.RemoveAuto(auto.ID);
        }
    }
}
