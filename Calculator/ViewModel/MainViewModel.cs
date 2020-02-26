using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _value;

        public RelayCommand Reset { get; set; }
        public RelayCommand Point { get; set; }
        public RelayCommand Sign { get; set; }

        public ParametrizedRelayCommand Number { get; set; }
        public ParametrizedRelayCommand Operation { get; set; }

        public MainViewModel()
        {
            Value = "0";
            Number = new ParametrizedRelayCommand(
                param =>
                {
                    string newChar = param.ToString();
                    if (Value == "0" && newChar == "0")
                    {
                        Value = "0";
                    }
                    else if (Value == "0") Value = newChar;
                    else if (Value.EndsWith(".0") && newChar != "0")
                    {
                        Value = Value.Remove(Value.Length - 1, 1) + newChar;
                    }
                    else Value += newChar;
                },
                param => true
                );
            Point = new RelayCommand(
                () =>
                {
                    Value += ".0";
                },
                () => (!Value.Contains("."))
                );
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                value = _value;
            }
        }

        public ParametrizedRelayCommand Play { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
