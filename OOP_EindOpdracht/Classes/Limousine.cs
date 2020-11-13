using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_EindOpdracht.Classes
{
    class Limousine : Auto
    {
        public bool Minibar { get; private set; }

        public Limousine(int id, string maker, string model, int bouwjaar, string kenteken, float kilometerTelling, bool minibar) : base(id, maker, model, bouwjaar, kenteken, kilometerTelling)
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
