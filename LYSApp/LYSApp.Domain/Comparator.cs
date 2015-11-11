using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model;

namespace LYSApp.Domain
{
    public class Comparator: IEqualityComparer<Room>
    {

        public bool Equals(Room obj1, Room obj2)
        {
            if (obj1.MonthlyRent == obj2.MonthlyRent)
            {
                return true;
            }
            else { return false; }
        }
        public int GetHashCode(Room codeh)
        {
            return 0;
        }
    }
}
