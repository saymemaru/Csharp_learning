using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PanelTest.ViewModels
{
    public class ValidationViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errorsBypropertyName = new();
        public bool HasErrors => _errorsBypropertyName.Count != 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return propertyName is not null && _errorsBypropertyName.ContainsKey(propertyName)
                ? _errorsBypropertyName[propertyName] 
                : Enumerable.Empty<string>();
        }

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        protected void AddError(string error, [CallerMemberName]string? propertyName = null)
        {
            if (propertyName is null) return;
            if (!_errorsBypropertyName.ContainsKey(propertyName))
                _errorsBypropertyName[propertyName] = new();
            if(!_errorsBypropertyName[propertyName].Contains(error))
            {
                _errorsBypropertyName[propertyName].Add(error);
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                RaiseProertyChanged(nameof(HasErrors));
            }
        }
        protected void ClearError([CallerMemberName]string? propertyName = null)
        {
            if (propertyName is null) return;
            if (_errorsBypropertyName.ContainsKey(propertyName))
            {
                _errorsBypropertyName.Remove(propertyName);
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                RaiseProertyChanged(nameof(HasErrors));
            }
        }

    }
}
