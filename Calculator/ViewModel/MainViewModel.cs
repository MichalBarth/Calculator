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
        private string _value1;
        private string _value2;
        private string _oper;
        private int _result;

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
                    else if (Value.EndsWith(".0") && newChar == "0")
                    {
                        Value = "0" + newChar;
                    }
                    else Value += newChar;
                },
                param => true
                );
            Point = new RelayCommand(
                () =>
                {
                    Value += ".0";
                    Point.RaiseCanExecuteChanged();
                },
                () => (!Value.Contains("."))
                );
            Sign = new RelayCommand(
                () =>
                {
                    if (Value.StartsWith("-"))
                        Value = Value.Substring(1, Value.Length - 1);
                    else Value = "-" + Value;
                },
                () => true
                );
            Reset = new RelayCommand(
                () =>
                {
                    Value = "0";
                    Point.RaiseCanExecuteChanged();
                },
                () => true
                );
            Operation = new ParametrizedRelayCommand(
                param =>
                {
                    Point.RaiseCanExecuteChanged();
                    switch (param)
                    {
                        case "+":
                            Value1 = Value;
                            Value = "0";
                            Oper = "+";
                            break;
                        case "-":
                            Value1 = Value;
                            Value = "0";
                            Oper = "-";
                            break;
                        case "*":
                            Value1 = Value;
                            Value = "0";
                            Oper = "*";
                            break;
                        case "/":
                            Value1 = Value;
                            Value = "0";
                            Oper = "/";
                            break;
                        case "^":
                            Value1 = Value;
                            Value = "0";
                            Oper = "^";
                            break;
                        case "√x":
                            Value1 = Value;
                            Oper = "√x";
                            break;
                        case "sin(x)":
                            Value1 = Value;
                            Oper = "sin(x)";
                            break;
                        case "cos(x)":
                            Value1 = Value;
                            Oper = "cos(x)";
                            break;

                        case "=":
                            Value2 = Value;
                            double x = Double.Parse(Value1);
                            double y = Double.Parse(Value2);
                            double result;
                            switch (Oper)
                            {
                                case "+":
                                    result = x + y;
                                    Value = result.ToString();
                                    break;
                                case "-":
                                    result = x - y;
                                    Value = result.ToString();
                                    break;
                                case "*":
                                    result = x * y;
                                    Value = result.ToString();
                                    break;
                                case "/":
                                    if (y == 0)
                                    {
                                        Value = "Dělení nulou";
                                    }
                                    else
                                    {
                                        result = x / y;
                                        Value = result.ToString();
                                    }
                                    break;
                                case "^":
                                    result = Math.Pow(x, y);
                                    Value = result.ToString();
                                    break;
                                case "√x":
                                    if (x < 0)
                                    {
                                        Value = "Odmocnění záporného čísla";
                                    }
                                    else
                                    {
                                        result = Math.Sqrt(x);
                                        Value = result.ToString();
                                    }
                                    break;
                                case "sin(x)":
                                    result = Math.Sin(x);
                                    Value = result.ToString();
                                    break;
                                case "cos(x)":
                                    result = Math.Cos(x);
                                    Value = result.ToString();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                },
                param => true
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
                _value = value;
                NotifyPropertyChanged();
            }
        }

        public string Value1
        {
            get
            {
                return _value1;
            }
            set
            {
                _value1 = value;
                NotifyPropertyChanged();
            }
        }

        public string Value2
        {
            get
            {
                return _value2;
            }
            set
            {
                _value2 = value;
                NotifyPropertyChanged();
            }
        }

        public string Oper
        {
            get
            {
                return _oper;
            }
            set
            {
                _oper = value;
                NotifyPropertyChanged();
            }
        }
        public int Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                NotifyPropertyChanged();
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
