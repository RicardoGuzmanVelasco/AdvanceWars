using System.Collections.Generic;
using System.Linq;

namespace AdvanceWars.Runtime.DataStructures
{
    public class RotarySwitch<T>
    {
        readonly T[] members;
        int turn;

        public RotarySwitch(IEnumerable<T> orderedMembers)
        {
            members = orderedMembers.ToArray();
        }

        public T Current => members[turn % members.Length];
        public int Round => 1 + turn / members.Length;
        public int Turn => turn;
        
        public void Next()
        {
            turn++;
        }
    }
}