using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal partial class MapCell: ObservableRecipient
    {
        public int X { get; set; }
        public int Y { get; set; }

        private bool _isShip;
        public bool IsShip {
            get => _isShip;
            set {
                _isShip = value;
                OnPropertyChanged(nameof(IsShip));
                OnPropertyChanged(nameof(IsHit));
                OnPropertyChanged(nameof(IsMissed));
            }
        }

        private bool _isShot;
        public bool IsShot {
            get => _isShot;
            set {
                _isShot = value;
                OnPropertyChanged(nameof(IsShot));
                OnPropertyChanged(nameof(IsHit));
                OnPropertyChanged(nameof(IsMissed));

            }
        }
        public bool CanPosition { get; set; }
        public bool IsHit { get => IsShip && IsShot; }
        public bool IsMissed { get => !IsShip && IsShot; }

        public void Clear()
        {
            IsShip = false;
            IsShot = false;
            CanPosition = true;
        }
        internal void ShotCell()
        {
            IsShot = true;
        }
    }
}