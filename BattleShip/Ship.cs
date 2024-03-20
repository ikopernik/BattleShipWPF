using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace BattleShip
{
    internal class Ship
    {
        private static int _mapWidth;
        private static int _mapHeight;

        public int Length { get; }
        public bool Positioned { get; set; }
        public int StartX;
        public int StartY;
        public List<ShipCell> Coordinates { get; }
        public Orientation Orientation;

        public Ship(int length)
        {
            Length = length;
            Coordinates = new List<ShipCell>(length);
        }

        public static void GlobalInit(int mapWidth, int mapHeight)
        {
            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
        }
        
        public void Clear()
        {
            Coordinates.Clear();
            Positioned = false;
        }

        internal void Shot(int X, int Y)
        {
            ShipCell cell = Coordinates.Where(coord => coord.X == X && coord.Y == Y).FirstOrDefault();
            if (cell != null)
            {
                cell.isHit = true;
            }
        }

        internal bool IsDrowned()
        {
            if (Coordinates.Where(x => !x.isHit).Count() > 0)
                return false;
            else
                return true;
        }

        internal (int minX, int minY, int maxX, int maxY) GetSurroundingRect()
        {
            int minX, minY, maxX, maxY;

            minX = Coordinates.Min(value => value.X) - 1;
            maxX = Coordinates.Max(value => value.X) + 1;
            minY = Coordinates.Min(value => value.Y) - 1;
            maxY = Coordinates.Max(value => value.Y) + 1;

            if (minX < 0) minX = 0;
            if (minY < 0) minY = 0;
            if (maxX > _mapWidth - 1) maxX = _mapWidth - 1;
            if (maxY > _mapHeight - 1) maxY = _mapHeight - 1;

            return (minX, minY, maxX, maxY);
        }
    }
}
