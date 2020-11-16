using System;

namespace OOP_EindOpdracht.Classes
{
    class Limousine : Auto
    {
        public bool Minibar { get; private set; }

        public Limousine(int id, string maker, string model, int bouwjaar, string kenteken, bool moetSchoonmaken, float kilometerTelling, bool isTeHuur, bool minibar) : base(id, maker, model, bouwjaar, kenteken,moetSchoonmaken, kilometerTelling,isTeHuur)
        {
            this.Minibar = minibar;
        }

        public override decimal BerekenKosten(float km)
        {
            float kosten = 450 + 3 * km;
            if (Minibar) kosten += 65;
            return (decimal)Math.Round(kosten);
        }
        public override string ToString()
        {
            return base.ToString()+", Heeft Minibar: "+Minibar;
        }
    }
}
