using PanelTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelTest.Data
{
    public interface IProductDataProvider
    {
        Task<IEnumerable<Product>?> GetAllAsync();
    }

    public class ProductDataProvider : IProductDataProvider
    {
        async Task<IEnumerable<Product>?> IProductDataProvider.GetAllAsync()
        {
            await Task.Delay(100);
            return new List<Product>
            {
                new Product() { Id = 1, Name = "Tank", Description = "an armor" },
                new Product() { Id = 2, Name = "Destoryer", Description = "a ship" },
                new Product() { Id = 3, Name = "Bomber", Description = "an aircraft"},
            };
        }
    }
}
