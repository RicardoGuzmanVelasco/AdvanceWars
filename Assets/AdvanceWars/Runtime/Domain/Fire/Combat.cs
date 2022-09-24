namespace AdvanceWars.Runtime
{
    public class Combat
    {
        readonly TheaterOps atk;
        readonly TheaterOps def;

        public Combat(TheaterOps atk, TheaterOps def)
        {
            this.atk = atk;
            this.def = def;
        }

        public (Battalion Atk, Battalion Def) PredictOutcome()
        {
            var attack = new Offensive
            (
                attacker: this.atk.Battalion,
                defender: this.def.Battalion,
                battlefield: this.def.Battlefield
            );

            var counterAttack = new Offensive
            (
                attacker: attack.Outcome(),
                defender: this.atk.Battalion,
                battlefield: this.atk.Battlefield
            );

            return (counterAttack.Outcome(), attack.Outcome());
        }
    }
}