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
        }
    }
}
