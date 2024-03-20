using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BattleShip
{
    internal class BattleMap
    {
        readonly int WIDTH;
        readonly int HEIGHT;
        internal List<Ship> Ships { get; } = new List<Ship>();
        MapCell[,] data = new MapCell[10,10];
        public MapCell this[int x, int y]
        {
            get { return data[x, y]; }
            set { data[x, y] = value; }
        }

        public BattleMap(int width, int height)
        {
            WIDTH = width;
            HEIGHT = height;
            CreateShips();
            CreateCells();
            ResetMap();
        }

        public void ResetMap()
        {
            ClearMap();
            ClearShips();
            PositionShips();
        }
        

        private void CreateShips()
        {
            Ships.Add(new Ship(4));
            Ships.Add(new Ship(3));
            Ships.Add(new Ship(3));
            Ships.Add(new Ship(2));
            Ships.Add(new Ship(2));
            Ships.Add(new Ship(1));
            Ships.Add(new Ship(1));
            Ships.Add(new Ship(1));
            Ships.Add(new Ship(1));
        }

        private void CreateCells() {
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    data[i, j] = new MapCell() { X = i, Y = j};
                }
            }
        }

        private void ClearMap()
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j].Clear();
                }
            }
        }

        private void ClearShips()
        {
            foreach (Ship ship in Ships)
            {
                ship.Clear();
            }
        }

        private void PositionShips()
        {
            Random random = new Random();

            while (Ships.Where(x => x.Positioned == false).Count() > 0)
            {
                // Select largest ship
                Ship ship = Ships.Where(x => x.Positioned == false).OrderByDescending(x => x.Length).FirstOrDefault();

                Orientation orientation;
                if (random.Next(0, 2) == 1)
                {
                    orientation = Orientation.Horizontal;
                }
                else
                {
                    orientation = Orientation.Vertical;
                }

                int maxX = WIDTH - 1;
                int maxY = HEIGHT - 1;

                bool shipPositioned = false;

                while (!shipPositioned)
                {
                    if (orientation == Orientation.Horizontal)
                        maxX = WIDTH - ship.Length;
                    else
                        maxY = HEIGHT - ship.Length;

                    ship.Orientation = orientation;
                    ship.StartX = random.Next(0, maxX + 1);
                    ship.StartY = random.Next(0, maxY + 1);

                    bool canPosition = TryPositionShip(ship);
                    if (canPosition)
                    {
                        PositionShip(ship);
                        shipPositioned = true;
                    }

                }
            }
        }

        private bool TryPositionShip(Ship ship)
        {
            ship.Coordinates.Clear();

            if (ship.Orientation == Orientation.Horizontal)
            {
                for (int i = ship.StartX; i < ship.StartX + ship.Length; i++)
                {
                    if (data[i, ship.StartY].CanPosition == false)
                        return false;

                    ship.Coordinates.Add(new ShipCell(i, ship.StartY));
                }
            }
            else
            {
                for (int i = ship.StartY; i < ship.StartY + ship.Length; i++)
                {
                    if (data[ship.StartX, i].CanPosition == false)
                        return false;

                    ship.Coordinates.Add(new ShipCell(ship.StartX, i));
                }
            }
            
            return true;
        }

        private void PositionShip(Ship ship)
        {
            (int minX, int minY, int maxX, int maxY) = ship.GetSurroundingRect();

            for (int i = minX; i <= maxX; i++)
                for (int j = minY; j <= maxY; j++)
                {
                    data[i, j].CanPosition = false;
                }

            foreach (var cell in ship.Coordinates)
            {
                data[cell.X, cell.Y].IsShip = true;
            }

            ship.Positioned = true;
        }
    }
}