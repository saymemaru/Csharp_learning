using PanelTest.Data;
using PanelTest.Models;
using System.Collections.ObjectModel;

namespace PanelTest.ViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        IProductDataProvider _productDataProvider;
        public ProductsViewModel(IProductDataProvider productDataProvider)
        {
            _productDataProvider = productDataProvider;
        }
        public ObservableCollection<Product> Products { get; } = new();
        public override async Task LoadAsync()
        {
            if (Products.Any())
                return;

            var products = await _productDataProvider.GetAllAsync();
            if (products is not null)
                foreach (Product product in products)
                    Products.Add(product);
        }



    }
}
