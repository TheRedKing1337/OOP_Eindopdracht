using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OOP_EindOpdracht.Classes
{
    class AutoAdministratie
    {
        private static List<Auto> autos;

        private const string connString = "Server=localhost;Database=oop_eindopdracht;uid=root;pwd=";

        static AutoAdministratie()
        {
            //get list from storage here

            autos = new List<Auto>();
        }

        public static Truck AddTruck(string maker, string model, int bouwjaar, string kenteken, float kilometerTelling, bool sleepTouw)
        {
            Truck truck = new Truck(autos.Count, maker, model, bouwjaar, kenteken, kilometerTelling, sleepTouw);
            autos.Add(truck);
            return truck;
        }
        public static Limousine AddLimousine(string maker, string model, int bouwjaar, string kenteken, float kilometerTelling, bool minibar)
        {
            Limousine limousine = new Limousine(autos.Count, maker, model, bouwjaar, kenteken, kilometerTelling, minibar);
            autos.Add(limousine);
            return limousine;
        }
        public static void RemoveAuto(int id)
        {
            autos.RemoveAt(id);
        }
        public static Auto GetByID(int id)
        {
            MySqlConnection conn = GetConnection();

            if (conn.State == ConnectionState.Open)
            {
                string query = "SELECT Maker, Model, Bouwjaar, Kenteken, Kilometertelling, AutoType FROM Autos WHERE ID = " + id;

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (!dataReader.HasRows)
                {
                    Console.WriteLine("Geen Auto met dat ID in het systeem");
                    return null;
                }

                //cache data from dataReader and close it because you cant have 2 active at once
                object[] dataCache = new object[6];
                dataReader.Read();
                dataReader.GetValues(dataCache);
                dataReader.Close();

                MySqlDataReader dataReaderInherited;

                //switch between AutoTypes (Truck 0 and Limousine 1), make new sql query to get their unique properties from their tables in DB, create instance and return it.
                switch (dataCache[5])
                {
                    case 0:
                        query = "SELECT Sleeptouw FROM Trucks where ID = " + id;

                        cmd = new MySqlCommand(query, conn);
                        dataReaderInherited = cmd.ExecuteReader();

                        dataReaderInherited.Read();
                        return new Truck(id, (string)dataCache[0], (string)dataCache[1], (int)dataCache[2], (string)dataCache[3], (float)dataCache[4], (bool)dataReaderInherited["Sleeptouw"]);
                    case 1:
                        query = "SELECT Minibar FROM Limousines WHERE ID = " + id;

                        cmd = new MySqlCommand(query, conn);
                        dataReaderInherited = cmd.ExecuteReader();

                        dataReaderInherited.Read();
                        return new Limousine(id, (string)dataCache[0], (string)dataCache[1], (int)dataCache[2], (string)dataCache[3], (float)dataCache[4], (bool)dataReaderInherited["Minibar"]);
                    default:
                        Console.WriteLine("Onbekend auto type");
                        break;
                }
            }
            return null;
        }
        public static Auto[] GetAllAutos()
        {
            //2 connections to have multiple datareaders simultaneously, one for autos one for getting the current type properties
            MySqlConnection conn = GetConnection();
            MySqlConnection conn2 = GetConnection();

            if (conn.State == ConnectionState.Open)
            {
                string query = "SELECT ID, Maker, Model, Bouwjaar, Kenteken, Kilometertelling, AutoType FROM Autos";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Auto> autos = new List<Auto>();

                MySqlDataReader dataReaderInherited;
                MySqlCommand cmd2;

                while (dataReader.Read())
                {
                    switch (dataReader["AutoType"])
                    {
                        case 0:
                            query = "SELECT Sleeptouw FROM Trucks where ID = " + (int)dataReader["ID"];

                            cmd2 = new MySqlCommand(query, conn2);
                            dataReaderInherited = cmd2.ExecuteReader();

                            dataReaderInherited.Read();
                            autos.Add(new Truck((int)dataReader["ID"], (string)dataReader["Maker"], (string)dataReader["Model"], (int)dataReader["Bouwjaar"], (string)dataReader["Kenteken"], (float)dataReader["KilometerTelling"], (bool)dataReaderInherited["Sleeptouw"]));

                            dataReaderInherited.Close();
                            break;
                        case 1:
                            query = "SELECT Minibar FROM Limousines WHERE ID = " + (int)dataReader["ID"];

                            cmd2 = new MySqlCommand(query, conn2);
                            dataReaderInherited = cmd2.ExecuteReader();

                            dataReaderInherited.Read();
                            autos.Add(new Limousine((int)dataReader["ID"], (string)dataReader["Maker"], (string)dataReader["Model"], (int)dataReader["Bouwjaar"], (string)dataReader["Kenteken"], (float)dataReader["KilometerTelling"], (bool)dataReaderInherited["Minibar"]));

                            dataReaderInherited.Close();
                            break;
                        default:
                            Console.WriteLine("Onbekend auto type");
                            break;
                    }
                }
                return autos.ToArray();
            }
            return null;
        }

        #region Database functions
        private static MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Kan geen verbinding maken met server");
                        break;

                    case 1045:
                        Console.WriteLine("Incorrecte inloginformatie");
                        break;
                }
            }

            return conn;
        }
        #endregion
    }
}
