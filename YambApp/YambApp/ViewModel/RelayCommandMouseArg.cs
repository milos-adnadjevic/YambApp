﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace YambApp.ViewModel
{
    public class RelayCommandMouseArg<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommandMouseArg(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is T param)
            {
                return _canExecute == null || _canExecute(param);
            }
            return _canExecute == null || _canExecute(default(T));
        }

        public void Execute(object parameter)
        {
            if (parameter is T param)
            {
                _execute(param);
            }
        }
    }


}
