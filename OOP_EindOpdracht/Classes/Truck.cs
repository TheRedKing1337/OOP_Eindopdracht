using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_EindOpdracht.Classes
{
    class Truck : Auto
    {
        public bool SleepTouw { get; private set; }

        public Truck(int id, string maker, string model, int bouwjaar, string kenteken, float kilometerTelling, bool sleepTouw):base(id, maker, model,bouwjaar,kenteken,kilometerTelling){
            this.SleepTouw = sleepTouw;
        }

        public override decimal BerekenKosten(float km)
        {
            float kosten = 950 + 0.15f * km;
            if (SleepTouw) kosten += 50;
            return (decimal)Math.Round(kosten);
        }
        public override string ToString()
        {
            return base.ToString() + ", Heeft Sleeptouw: "+SleepTouw;
        }
    }
}
