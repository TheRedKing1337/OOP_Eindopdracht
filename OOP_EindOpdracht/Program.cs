using System;
using OOP_EindOpdracht.Classes;

namespace OOP_EindOpdracht
{
    class Program
    {
        static void Main(string[] args)
        {
            Truck newTruck = AutoAdministratie.AddTruck("Volvo","Truck7",2001,"72-NS-HH",0,true);
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

           int autoID = AutoAdministratie.GetByID(newTruck.ID).ID;

            Console.WriteLine(AutoAdministratie.GetByID(autoID));
            Console.WriteLine("Hiring");
            Console.ReadLine();

            if (AutoAdministratie.HuurAuto(autoID))
            {
                Console.WriteLine("Hired ID: " + autoID);
            }
            else
            {
                Console.WriteLine(autoID + " was already hired");
            }

            Console.WriteLine(AutoAdministratie.GetByID(autoID));
            Console.WriteLine("Inleveren");
            Console.ReadLine();

            Console.WriteLine("Ingeleverd! Costs are: " + AutoAdministratie.LeverIn(autoID, 50));

            Console.WriteLine(AutoAdministratie.GetByID(autoID));
            Console.WriteLine("Cleaning");
            Console.ReadLine();

            if (AutoAdministratie.MaakSchoon(autoID))
            {
                Console.WriteLine("Car cleaned and set for hire");
            }
            else
            {
                Console.WriteLine("Car was already cleaned");
            }

            Console.WriteLine(AutoAdministratie.GetByID(autoID));
            Console.WriteLine("Deleting");
            Console.ReadLine();

            AutoAdministratie.RemoveAuto(autoID);
        }
    }
}
