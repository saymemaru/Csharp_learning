using PanelTest.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelTest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public MainViewModel(PeopleViewModel peopleViewModel, ProductsViewModel productsViewModel)
        {
            PeopleViewModel = peopleViewModel;
            ProductsViewModel = productsViewModel;
            _selectedViewModel = peopleViewModel;

            SelectViewModelCommand = new(SelectViewModel);
        }
        public ViewModelBase? SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                RaiseProertyChanged();
            }
        }
        public RelayCommand SelectViewModelCommand { get;}
        public PeopleViewModel PeopleViewModel { get; }
        public ProductsViewModel ProductsViewModel { get; }

        public override async Task LoadAsync()
        {
            if (SelectedViewModel != null)
            {
               await SelectedViewModel.LoadAsync();
            }
        }

        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
    }
}
