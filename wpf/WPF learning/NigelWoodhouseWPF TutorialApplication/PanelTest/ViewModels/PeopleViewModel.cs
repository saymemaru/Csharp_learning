using PanelTest.Data;
using PanelTest.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using PanelTest.Command;

namespace PanelTest.ViewModels
{
    public class PeopleViewModel : ViewModelBase
    {
        private readonly IPersonDataProvider _personDataProvider;
        private PersonItemViewModel? _selectedPerson;
        private NavigationSide _navigationColumn;

        public PeopleViewModel(IPersonDataProvider personDataProvider)
        {
            _personDataProvider = personDataProvider;
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            MoveNavigationCommand = new RelayCommand(MoveNavigation);
        }

        public ObservableCollection<PersonItemViewModel> People { get; } = new();
        public PersonItemViewModel? SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                RaiseProertyChanged();
                RaiseProertyChanged(nameof(IsPersonSelected));
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        public bool IsPersonSelected => SelectedPerson != null;
        public NavigationSide NavigationColumn
        {
            get => _navigationColumn;
            private set
            {
                _navigationColumn = value;
                RaiseProertyChanged();
            }
        }
        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand MoveNavigationCommand { get; }
        public override async Task LoadAsync()
        {
            if (People.Any())
                return;

            var people = await _personDataProvider.GetAllAsync();
            if (people is not null)
                foreach (Person person in people)
                    People.Add(new PersonItemViewModel(person));
        }
        private void Add(object? parameter)
        {
            Person person = new() { FirstName = "new" };
            PersonItemViewModel viewModel = new PersonItemViewModel(person);
            People.Add(viewModel);
            SelectedPerson = viewModel;
        }
        private bool CanDelete(object? arg) => SelectedPerson is not null;
        private void Delete(object? parameter)
        {
            if(SelectedPerson is not null)
            {
                People.Remove(SelectedPerson);
                SelectedPerson = null;
            }
                
        }
        private void MoveNavigation(object? parameter)
        {
            NavigationColumn = NavigationColumn == NavigationSide.Left ? NavigationSide.Right : NavigationSide.Left;
        }
    }

    public enum NavigationSide
    {
        Left = 0,
        Right = 2
    }
}
