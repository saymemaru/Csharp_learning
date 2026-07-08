using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PanelTest.Data;
using PanelTest.ViewModels;
using PanelTest.Windows;

namespace PanelTest
{ 
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            //设置依赖注入 P73
            ServiceCollection services = new();
            ConfigureService(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureService(ServiceCollection services)
        {
            services.AddTransient<Window1>();
            services.AddTransient<LoginWindow>(); services.AddSingleton<MainViewModel>();
            services.AddSingleton<ProductsViewModel>();
            services.AddSingleton<PeopleViewModel>();

            services.AddTransient<IPersonDataProvider, PersonDataProvider>();
            services.AddTransient<IProductDataProvider, ProductDataProvider>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window1 = _serviceProvider.GetService<Window1>();
            //var loginWindow = _serviceProvider.GetService<LoginWindow>();

            window1?.Show();
            //loginWindow.Show();
        }
    }

}
