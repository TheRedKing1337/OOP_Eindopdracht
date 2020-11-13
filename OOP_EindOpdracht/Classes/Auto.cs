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

        public Auto(int id, string maker, string model, int bouwjaar, string kenteken, float kilometerTelling) {
            ID = id;
            Maker = maker;
            Model = model;
            Bouwjaar = bouwjaar;
            Kenteken = kenteken;
            KilometerTelling = kilometerTelling;
        }
        //functions
        public bool Huur(){
            if (IsTeHuur)
            {
                IsTeHuur = false;
                return true;
            } else {
                return false;
            }
        }
        public decimal LeverIn(float km){
            KilometerTelling += km;
            MoetSchoonmaken = true;
            return BerekenKosten(km);
        }
        public void Schoonmaak(){
            MoetSchoonmaken = false;
            IsTeHuur = true;
        }
        public abstract decimal BerekenKosten(float km);  
        public override string ToString(){
            return "ID: " + ID + ", Maker: " + Maker + ", Model: " + Model + ", Bouwjaar: "+Bouwjaar+", Kenteken: "+Kenteken+", Kilometer Telling: "+KilometerTelling+", Is te huur: "+IsTeHuur+", Moet Schoonmaken: "+MoetSchoonmaken;
        }
    }
}
