using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Btg.Models
{
    public class Cliente : INotifyPropertyChanged
    {
        private string name;
        private string lastname;
        private int age;
        private string address;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string Lastname
        {
            get => lastname;
            set { lastname = value; OnPropertyChanged(); }
        }

        public int Age
        {
            get => age;
            set { age = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get => address;
            set { address = value; OnPropertyChanged(); }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}


