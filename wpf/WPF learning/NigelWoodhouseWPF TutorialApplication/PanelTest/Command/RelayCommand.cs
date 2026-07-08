using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PanelTest.Command
{
    public class RelayCommand : ICommand
    {
        readonly Action<object?> _excute;
        readonly Func<object?, bool>? _canExcute;
        public RelayCommand(Action<object?> excute, Func<object?, bool>? canExcute = null)
        {
            ArgumentNullException.ThrowIfNull(excute);
            _excute = excute;
            _canExcute = canExcute;
        }

        public event EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public bool CanExecute(object? parameter) => _canExcute is null || _canExcute(parameter);
        public void Execute(object? parameter) => _excute(parameter);


    }
}
