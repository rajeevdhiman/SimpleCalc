using SimpleCalc.ViewModels.Base;
using SimpleCalc.Extensions;
using System.Text;
using System.Windows.Input;
using SimpleCalc.Models;

namespace SimpleCalc.ViewModels
{
    class CalculatorViewModel : ViewModelBase
    {
        private ICommand _numericInputCommand;
        private ICommand _operatorCommand;
        private ICommand _clearExpressionCommand;
        private string _displayText = "0";

        #region Public Properties

        public StringBuilder CurrentExpression { get; } = new StringBuilder();

        public string DisplayText
        {
            get
            {
                return _displayText;
            }
            private set
            {
                _displayText = value;
                NotifyPropertyChanged(() => DisplayText);
            }
        }

        public ICommand ClearExpressionCommand
        {
            get
            {
                return _clearExpressionCommand ?? (
                    _clearExpressionCommand = new RelayCommand<object>(o =>
                    {
                        CurrentExpression.Clear();
                        DisplayText = "0";
                    },
                    o => DisplayText.Equals("0") == false));
            }
        }

        public ICommand NumericInputCommand
        {
            get
            {
                return _numericInputCommand ?? (
                    _numericInputCommand = new RelayCommand<string>(exp =>
                    {
                        CurrentExpression.Append(exp);
                        DisplayText = CurrentExpression.ToString();
                    },
                    exp => !string.IsNullOrWhiteSpace(exp)));
            }
        }
        public ICommand OperatorCommand
        {
            get
            {
                return _operatorCommand ?? (
                    _operatorCommand = new RelayCommand<string>(oprtr => AssignOperatorToExpression(oprtr),
                    exp => !string.IsNullOrWhiteSpace(exp)));
            }
        }

        public ICommand CalculateCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    ICalculator calculator = SimpleIoC.ResolveSingle<ICalculator>();

                    var result = calculator.Calculate(CurrentExpression.ToString());
                    CurrentExpression.Clear();
                    DisplayText = result.ToString();

                }, obj => DisplayText.IndexOfAny(Models.Calculator.OPERATORS) >= 0);
            }
        }

        #endregion Public Properties

        private void AssignOperatorToExpression( string oprtr)
        {
            if (DisplayText.EndWithAny(Models.Calculator.OPERATORS))
            {
                CurrentExpression.Remove(CurrentExpression.Length - 1, 1);
            }
            CurrentExpression.Append(oprtr);
            DisplayText = CurrentExpression.ToString();
        }

    }
}
