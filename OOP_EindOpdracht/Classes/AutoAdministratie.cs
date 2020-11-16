using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace OOP_EindOpdracht.Classes
{
    static class AutoAdministratie
    {
        private const string connString = "Server=localhost;Database=oop_eindopdracht;uid=root;pwd=";

        #region Create/Get functions
        public static Truck AddTruck(string maker, string model, int bouwjaar, string kenteken, float kilometerTelling, bool sleepTouw)
        {
            MySqlConnection conn = GetConnection();

            if (conn.State == ConnectionState.Open)
            {
                //Insert data into Autos table
                string query = "INSERT INTO Autos(Maker, Model, Bouwjaar, Kenteken, KilometerTelling, AutoType) VALUES ('" + maker + "', '" + model + "', " + bouwjaar + ", '" + kenteken + "', " + kilometerTelling + ", " + 0 + ")";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                //Get ID of the inserted row
                query = "SELECT LAST_INSERT_ID()";
                cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Cache ID
                dataReader.Read();
                int ID = dataReader.GetInt32(0);
                dataReader.Close();

                //Insert data into Trucks table
                query = "INSERT INTO Trucks VALUES(" + ID + ", " + sleepTouw + ")";
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                conn.Close();

                //Return truck instance
                Truck truck = new Truck(ID, maker, model, bouwjaar, kenteken, false, kilometerTelling, true, sleepTouw);
                return truck;
            }

            Console.WriteLine("Failed to add new Truck to database");
            return null;
        }
        public static Limousine AddLimousine(string maker, string model, int bouwjaar, string kenteken, float kilometerTelling, bool minibar)
        {
            MySqlConnection conn = GetConnection();

            if (conn.State == ConnectionState.Open)
            {
                //Insert data into Autos table
                string query = "INSERT INTO Autos(Maker, Model, Bouwjaar, Kenteken, KilometerTelling, AutoType) VALUES ('" + maker + "', '" + model + "', " + bouwjaar + ", '" + kenteken + "', " + kilometerTelling + ", " + 1 + ")";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                //Get ID of the inserted row
                query = "SELECT LAST_INSERT_ID()";
                cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Cache ID
                dataReader.Read();
                int ID = dataReader.GetInt32(0);
                dataReader.Close();

                //Insert data into Limousines table
                query = "INSERT INTO Limousines VALUES(" + ID + ", " + minibar + ")";
                dataReader.Close();
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                conn.Close();

                //return Limousine instance
                Limousine limousine = new Limousine(ID, maker, model, bouwjaar, kenteken, false, kilometerTelling, true, minibar);
                return limousine;
            }

            Console.WriteLine("Failed to add new limousine to database");
            return null;
        }       
        public static Auto GetByID(int id)
        {
            MySqlConnection conn = GetConnection();

            if (conn.State == ConnectionState.Open)
            {
                string query = "SELECT Maker, Model, Bouwjaar, Kenteken, MoetSchoonmaken, Kilometertelling, IsTeHuur, AutoType FROM Autos WHERE ID = " + id;

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Check if not empty
                if (!dataReader.HasRows)
                {
                    Console.WriteLine("Geen Auto met dat ID in het systeem");
                    return null;
                }

                //cache data from dataReader and close it because you cant have 2 active at once
                object[] dataCache = new object[8];
                dataReader.Read();
                dataReader.GetValues(dataCache);
                dataReader.Close();

                MySqlDataReader dataReaderInherited;

                Auto toReturn;
                //switch between AutoTypes (Truck 0 and Limousine 1), make new sql query to get their unique properties from their tables in DB, create instance and return it.
                switch (dataCache[7])
                {
                    case 0:
                        query = "SELECT Sleeptouw FROM Trucks where ID = " + id;

                        cmd = new MySqlCommand(query, conn);
                        dataReaderInherited = cmd.ExecuteReader();

                        dataReaderInherited.Read();
                        toReturn = new Truck(id, (string)dataCache[0], (string)dataCache[1], (int)dataCache[2], (string)dataCache[3], (bool)dataCache[4], (float)dataCache[5], (bool)dataCache[6], (bool)dataReaderInherited["Sleeptouw"]);

                        break;
                    case 1:
                        query = "SELECT Minibar FROM Limousines WHERE ID = " + id;

                        cmd = new MySqlCommand(query, conn);
                        dataReaderInherited = cmd.ExecuteReader();

                        dataReaderInherited.Read();
                        toReturn = new Limousine(id, (string)dataCache[0], (string)dataCache[1], (int)dataCache[2], (string)dataCache[3], (bool)dataCache[4], (float)dataCache[5], (bool)dataCache[6], (bool)dataReaderInherited["Minibar"]);

                        break;
                    default:
                        Console.WriteLine("Onbekend auto type");
                        return null;
                }
                conn.Close();
                return toReturn;
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
                string query = "SELECT ID, Maker, Model, Bouwjaar, Kenteken, MoetSchoonmaken, Kilometertelling, IsTeHuur, AutoType FROM Autos";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Auto> autos = new List<Auto>();

                MySqlDataReader dataReaderInherited;
                MySqlCommand cmd2;

                while (dataReader.Read())
                {
                    //switch between autoTypes, get data from DB, create instance and add to list
                    switch (dataReader["AutoType"])
                    {
                        case 0:
                            query = "SELECT Sleeptouw FROM Trucks where ID = " + (int)dataReader["ID"];

                            cmd2 = new MySqlCommand(query, conn2);
                            dataReaderInherited = cmd2.ExecuteReader();

                            dataReaderInherited.Read();
                            autos.Add(new Truck((int)dataReader["ID"], (string)dataReader["Maker"], (string)dataReader["Model"], (int)dataReader["Bouwjaar"], (string)dataReader["Kenteken"], (bool)dataReader["MoetSchoonmaken"], (float)dataReader["KilometerTelling"], (bool)dataReader["IsTeHuur"], (bool)dataReaderInherited["Sleeptouw"]));

                            dataReaderInherited.Close();
                            break;
                        case 1:
                            query = "SELECT Minibar FROM Limousines WHERE ID = " + (int)dataReader["ID"];

                            cmd2 = new MySqlCommand(query, conn2);
                            dataReaderInherited = cmd2.ExecuteReader();

                            dataReaderInherited.Read();
                            autos.Add(new Limousine((int)dataReader["ID"], (string)dataReader["Maker"], (string)dataReader["Model"], (int)dataReader["Bouwjaar"], (string)dataReader["Kenteken"], (bool)dataReader["MoetSchoonmaken"], (float)dataReader["KilometerTelling"], (bool)dataReader["IsTeHuur"], (bool)dataReaderInherited["Minibar"]));

                            dataReaderInherited.Close();
                            break;
                        default:
                            Console.WriteLine("Onbekend auto type");
                            break;
                    }
                }
                conn.Close();
                return autos.ToArray();
            }
            return null;
        }
        #endregion

        #region Action functions
        public static bool HuurAuto(int id)
        {
            Auto auto = GetByID(id);
            if (auto.IsTeHuur)
            {
                auto.IsTeHuur = false;
                UpdateDB(auto);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool MaakSchoon(int id){
            Auto auto = GetByID(id);

            if (auto.MoetSchoonmaken)
            {
                auto.MoetSchoonmaken = false;
                auto.IsTeHuur = true;
                AutoAdministratie.UpdateDB(auto);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static decimal LeverIn(int id, float km)
        {
            Auto auto = GetByID(id);

            auto.KilometerTelling += km;
            auto.MoetSchoonmaken = true;
            UpdateDB(auto);
            return auto.BerekenKosten(km);
        }
        public static void RemoveAuto(int id)
        {
            MySqlConnection conn = GetConnection();

            if (conn.State == ConnectionState.Open)
            {
                //Delete record from main table
                string query = "DELETE FROM Autos where ID = " + id;
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                //Delete record Trucks table if exists
                query = "DELETE FROM Trucks where ID = " + id;
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                //Delete record Limousines table if exists
                query = "DELETE FROM Limousines where ID = " + id;
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Auto with id: " + id + " deleted succesfully");
            }
            conn.Close();
        }
        #endregion

        #region Database functions
        private static void UpdateDB(Auto auto)
        {
            MySqlConnection conn = GetConnection();

            if (conn.State == ConnectionState.Open)
            {
                string query = "UPDATE Autos SET Maker = '" + auto.Maker + "', Model = '" + auto.Model + "', Bouwjaar = " + auto.Bouwjaar + ", Kenteken = '" + auto.Kenteken + "', MoetSchoonmaken = " + auto.MoetSchoonmaken + ", KilometerTelling = " + auto.KilometerTelling + ", IsTeHuur = " + auto.IsTeHuur + " WHERE ID = " + auto.ID;

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
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
