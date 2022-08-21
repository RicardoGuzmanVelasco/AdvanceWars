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

        public (Batallion Attacker, Batallion Defender) PredictOutcome()
        {
            var attack = new Offensive(
                attacker: this.attackingTheaterOps.Troops,
                defender: this.defendingTheaterOps.Troops,
                battlefield: this.attackingTheaterOps.Battlefield);

            return (attack.Outcome(), new Batallion());
        }
    }
}