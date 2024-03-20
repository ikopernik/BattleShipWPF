using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class ShipCell
    {
        public int X;
        public int Y;
        public bool isHit;

        public ShipCell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        internal void Clear()
        {
            isHit = false;
        }
    }
}
