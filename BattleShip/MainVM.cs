using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal partial class MainVM: ObservableRecipient
    {
        const int WIDTH = 10;
        const int HEIGHT = 10;
        public bool PlayerTurn { get; set; } = false;
        public bool EnemyTurn { get => !PlayerTurn; }

        public BattleMap PlayerMap { get; }
        public BattleMap EnemyMap { get; }

        public MainVM()
        {
            Ship.GlobalInit(WIDTH, HEIGHT);
            PlayerMap = new BattleMap(WIDTH, HEIGHT);
            EnemyMap = new BattleMap(WIDTH, HEIGHT);
        }

        [RelayCommand]
        public void ShotCell(MapCell curCell)
        {
            BattleMap map = PlayerTurn ? PlayerMap : EnemyMap;

            MakeShot(curCell, map);

            PlayerTurn = !PlayerTurn;
            OnPropertyChanged(nameof(PlayerTurn));
            OnPropertyChanged(nameof(EnemyTurn));
        }

        private void MakeShot(MapCell cell, BattleMap map)
        {
            cell.ShotCell();

            // Check if we hit the ship
            Ship ship = FindShipByCell(cell, map);
            if (ship != null)
            {
                ship.Shot(cell.X, cell.Y);

                // Check if the whole ship is hit
                if (ship.IsDrowned())
                {
                    (int minX, int minY, int maxX, int maxY) = ship.GetSurroundingRect();
                    for (int i = minX; i <= maxX; i++)
                        for (int j = minY; j <= maxY; j++)
                        {
                            map[i,j].IsShot = true;
                        }
                }

            }

        }

        private Ship FindShipByCell(MapCell cell, BattleMap map)
        {
            foreach (Ship ship in map.Ships)
            {
                foreach (var shipCell in ship.Coordinates)
                {
                    if (shipCell.X == cell.X && shipCell.Y == cell.Y)
                    {
                        shipCell.isHit = true;
                        return ship;
                    }
                }
            }
            
            return null;
        }
    }
}
