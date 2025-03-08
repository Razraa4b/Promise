using Autofac;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Promise.Application.ViewModels;
using Promise.Domain.Contracts;
using Promise.Domain.Models;
using Promise.Infrastructure.Database;
using Promise.Infrastructure.Repositories;
using Promise.Infrastructure.Services.Loggers;
using Promise.UI.Views;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using System.Reflection;

namespace Promise.UI
{
    public partial class App : Avalonia.Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            BindingPlugins.DataValidators.RemoveAt(0);

            ContainerBuilder builder = new ContainerBuilder();

            // Main
            builder.Register(c => this).SingleInstance();

            builder.RegisterGeneric(typeof(FileLogger<>)).As(typeof(ILogger<>));

            builder.RegisterType<ApplicationContext>().InstancePerLifetimeScope();

            // Repositories
            builder.RegisterType<NotesRepository>().As<IRepository<Note>>();
            builder.RegisterType<ReportsRepository>().As<IRepository<Report>>();
            // View Models
            builder.RegisterType<MainViewModel>().SingleInstance();
            // Views
            builder.Register(c => new MainView() { DataContext = c.Resolve<MainViewModel>() }).SingleInstance();

            AutofacDependencyResolver resolver = builder.UseAutofacDependencyResolver();

            builder.RegisterInstance(resolver);

            resolver.InitializeSplat();
            resolver.InitializeReactiveUI();

            Locator.SetLocator(resolver);
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

            IContainer container = builder.Build();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = container.Resolve<MainView>();
            } 

            base.OnFrameworkInitializationCompleted();
        }
    }
}