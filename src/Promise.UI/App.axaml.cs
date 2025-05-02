using Autofac;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Styling;
using Microsoft.EntityFrameworkCore;
using Promise.Application.ViewModels;
using Promise.Domain.Contracts;
using Promise.Domain.Enums;
using Promise.Domain.Models;
using Promise.Infrastructure.Database;
using Promise.Infrastructure.Repositories;
using Promise.Infrastructure.Services.Loggers;
using Promise.UI.Services;
using Promise.UI.Views;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using System.Reflection;

namespace Promise.UI
{
    public partial class App : Avalonia.Application
    {
        private ILogger<App> _logger = null!;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            BindingPlugins.DataValidators.RemoveAt(0);

            ContainerBuilder builder = new ContainerBuilder();

            // Theme Manager
            builder.RegisterType<ThemeManager>().SingleInstance();
            // Logger
            builder.RegisterGeneric(typeof(FileLogger<>)).As(typeof(ILogger<>));
            // Database
            builder.Register(c =>
            {
                DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlite("Data Source=app.db");
                ApplicationContext context = new ApplicationContext(optionsBuilder.Options);
                return context;
            }).InstancePerLifetimeScope();
            // Repositories
            builder.RegisterType<NotesRepository>().As<IRepository<Note>>().InstancePerLifetimeScope();
            builder.RegisterType<ReportsRepository>().As<IRepository<Report>>().InstancePerLifetimeScope();
            // Notification Service
            builder.RegisterType<MessageBoxService>().As<INotificationService>().InstancePerLifetimeScope();
            // View Models
            builder.RegisterType<MainViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<NotesViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ReportsViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            // Views
            builder.Register(c => new MainView() { DataContext = c.Resolve<MainViewModel>() }).SingleInstance();
            builder.Register(c => new NotesView() { DataContext = c.Resolve<NotesViewModel>() }).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => new ReportsView() { DataContext = c.Resolve<NotesViewModel>() }).AsImplementedInterfaces().SingleInstance();
            // View Locator
            builder.Register(c => new RxViewLocator(c.Resolve<ILifetimeScope>()));

            AutofacDependencyResolver resolver = builder.UseAutofacDependencyResolver();

            builder.RegisterInstance(resolver);

            IContainer container = builder.Build();

            resolver.InitializeSplat();
            resolver.InitializeReactiveUI();

            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());


            // Setup logger
            _logger = container.Resolve<ILogger<App>>();

            // Setup view locator
            Locator.CurrentMutable.RegisterLazySingleton(() => container.Resolve<RxViewLocator>(), typeof(IViewLocator));

            // Select theme by system default
            ThemeManager manager = container.Resolve<ThemeManager>();
            ThemeMode theme = ActualThemeVariant == ThemeVariant.Light ? ThemeMode.Light : ThemeMode.Dark;
            manager.Select(theme);

            // Initialize database
            container.Resolve<ApplicationContext>();

            // Start main view
            _logger.Log(Domain.Enums.LogLevel.Debug, "The application is running, opening the main view...");
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = container.Resolve<MainView>();
            }
            _logger.Log(Domain.Enums.LogLevel.Debug, "Framework initializetion completed successfuly");

            base.OnFrameworkInitializationCompleted();
        }
    }
}