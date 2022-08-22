namespace AdvanceWars.Runtime
{
    public class Combat
    {
        readonly TheaterOps attackingTheaterOps;
        readonly TheaterOps defendingTheaterOps;

        public Combat(TheaterOps attackingTheaterOps, TheaterOps defendingTheaterOps)
        {
            this.attackingTheaterOps = attackingTheaterOps;
            this.defendingTheaterOps = defendingTheaterOps;
        }

        public (Battalion Attacker, Battalion Defender) PredictOutcome()
        {
            var attack = new Offensive
            (
                attacker: this.attackingTheaterOps.Troops,
                defender: this.defendingTheaterOps.Troops,
                battlefield: this.defendingTheaterOps.Battlefield
            );

            var counterAttack = new Offensive
            (
                attacker: attack.Outcome(),
                defender: this.attackingTheaterOps.Troops,
                battlefield: this.attackingTheaterOps.Battlefield
            );

            return (counterAttack.Outcome(), attack.Outcome());
        }
    }
}