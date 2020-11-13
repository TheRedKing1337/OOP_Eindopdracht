using System;
using OOP_EindOpdracht.Classes;

namespace OOP_EindOpdracht
{
    class Program
    {
        static void Main(string[] args)
        {
            //Truck truck = AutoAdministratie.AddTruck("Ford", "a", 2020, "11-22-33", 50000, true);
            //AutoAdministratie.AddTruck("Ford", "a", 2021, "11-22-33", 50000, true);
            //AutoAdministratie.AddTruck("Ford", "a", 2022, "11-22-33", 50000, true);

            //Console.WriteLine(truck + "");

            //if(truck.Huur()){
            //    Console.WriteLine("truck gehuurd");
            //} else {
            //    Console.WriteLine("Truck niet te huur");
            //}



            //truck.LeverIn(100);

            //Console.WriteLine("kilometerTelling: " + truck.KilometerTelling);

            //truck.Schoonmaak();

            //Console.WriteLine("isTeHuur: " + truck.IsTeHuur);

            //Auto truck = AutoAdministratie.GetByID(1);

            //Console.WriteLine(truck.ToString());

            //Auto truck2 = AutoAdministratie.GetByID(2);

            //Console.WriteLine(truck2.ToString());

            Auto[] autos = AutoAdministratie.GetAllAutos();

            if (autos != null)
            {
                for (int i = 0; i < autos.Length; i++)
                {
                    Console.WriteLine(autos[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("No autos in Database");
            }
            //Auto[] autos = AutoAdministratie.GetAllAutos();

            //Console.WriteLine(autos.Length + " autos in administratie");
        }
    }
}
