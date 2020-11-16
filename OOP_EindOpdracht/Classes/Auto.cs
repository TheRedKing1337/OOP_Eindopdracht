using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_EindOpdracht.Classes
{
    abstract class Auto
    {
        public int ID { get; private set; }
        public string Maker { get; private set; }
        public string Model { get; private set; }
        public int Bouwjaar { get; private set; }
        public string Kenteken { get; private set; }
        public bool MoetSchoonmaken { get; private set; }
        public float KilometerTelling { get; private set; }
        public bool IsTeHuur { get; private set; } = true;

        public Auto(int id, string maker, string model, int bouwjaar, string kenteken, bool moetSchoonmaken, float kilometerTelling, bool isTeHuur)
        {
            ID = id;
            Maker = maker;
            Model = model;
            Bouwjaar = bouwjaar;
            Kenteken = kenteken;
            MoetSchoonmaken = moetSchoonmaken;
            KilometerTelling = kilometerTelling;
            IsTeHuur = isTeHuur;
        }
        //functions
        public bool Huur()
        {
            if (IsTeHuur)
            {
                IsTeHuur = false;
                AutoAdministratie.UpdateDB(this);
                return true;
            }
            else
            {
                return false;
            }
        }
        public decimal LeverIn(float km)
        {
            KilometerTelling += km;
            MoetSchoonmaken = true;
            AutoAdministratie.UpdateDB(this);
            return BerekenKosten(km);
        }
        public bool Schoonmaak()
        {
            if (MoetSchoonmaken)
            {
                MoetSchoonmaken = false;
                IsTeHuur = true;
                AutoAdministratie.UpdateDB(this);
                return true;
            }
            else
            {
                return false;
            }
        }
        protected abstract decimal BerekenKosten(float km);
        public override string ToString()
        {
            return this.GetType().Name + " ID: " + ID + ", Maker: " + Maker + ", Model: " + Model + ", Bouwjaar: " + Bouwjaar + ", Kenteken: " + Kenteken + ", Kilometer Telling: " + KilometerTelling + ", Is te huur: " + IsTeHuur + ", Moet Schoonmaken: " + MoetSchoonmaken;
        }
    }
}
