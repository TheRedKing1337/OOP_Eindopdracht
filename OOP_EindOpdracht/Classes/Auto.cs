namespace OOP_EindOpdracht.Classes
{
    abstract class Auto
    {
        public int ID { get; private set; }
        public string Maker { get; private set; }
        public string Model { get; private set; }
        public int Bouwjaar { get; private set; }
        public string Kenteken { get; private set; }
        public bool MoetSchoonmaken { get; set; }
        public float KilometerTelling { get; set; }
        public bool IsTeHuur { get; set; } 

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
        public abstract decimal BerekenKosten(float km);
        public override string ToString()
        {
            return this.GetType().Name + " ID: " + ID + ", Maker: " + Maker + ", Model: " + Model + ", Bouwjaar: " + Bouwjaar + ", Kenteken: " + Kenteken + ", Kilometer Telling: " + KilometerTelling + ", Is te huur: " + IsTeHuur + ", Moet Schoonmaken: " + MoetSchoonmaken;
        }
    }
}

