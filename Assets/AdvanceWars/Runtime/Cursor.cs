namespace AdvanceWars.Runtime
{
    public class Cursor
    {
        readonly Operation operation;

        public Cursor(Operation operation)
        {
            this.operation = operation;
        }

        public void EndTurn()
        {
            operation.NextTurn();
        }
    }
}