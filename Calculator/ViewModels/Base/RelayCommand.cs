using System;
using System.Windows.Input;

namespace SimpleCalc.ViewModels.Base
{
    public class RelayCommand<T> : ICommand
    {
        private Action<T> methodToExecute;
        private Func<T, bool> canExecuteEvaluator;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> methodToExecute, Func<T, bool> canExecuteEvaluator)
        {
            this.methodToExecute = methodToExecute;
            this.canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action<T> methodToExecute)
            : this(methodToExecute, null)
        {
        }

        private bool CanExecute(T parameter)
        {
            if (canExecuteEvaluator == null) return true;

            return canExecuteEvaluator.Invoke(parameter);
        }

        private void Execute(T parameter)
        {
            methodToExecute.Invoke(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            Execute((T)parameter);
        }
    }
}