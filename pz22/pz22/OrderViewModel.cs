using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pz22
{
    public class OrderViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double coffeePrice;
        public double CoffeePrice
        {
            get => coffeePrice;
            set
            {
                coffeePrice = value;
                OnPropertyChanged(nameof(CoffeePrice));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private double sizePrice;
        public double SizePrice
        {
            get => sizePrice;
            set
            {
                sizePrice = value;
                OnPropertyChanged(nameof(SizePrice));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private bool milk;
        public bool Milk
        {
            get => milk;
            set
            {
                milk = value;
                OnPropertyChanged(nameof(Milk));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private bool syrup;
        public bool Syrup
        {
            get => syrup;
            set
            {
                syrup = value;
                OnPropertyChanged(nameof(Syrup));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private bool extraShot;
        public bool ExtraShot
        {
            get => extraShot;
            set
            {
                extraShot = value;
                OnPropertyChanged(nameof(ExtraShot));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double TotalPrice
        {
            get
            {
                double addons = 0;

                if (Milk) addons += 0.5;
                if (Syrup) addons += 0.7;
                if (ExtraShot) addons += 1.2;

                return CoffeePrice + SizePrice + addons;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
