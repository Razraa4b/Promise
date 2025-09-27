using Autofac;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Styling;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Promise.Application.ViewModels;
using Promise.Domain.Contracts;
using Promise.Domain.Enums;
using Promise.Domain.Entities;
using Promise.Infrastructure.Database;
using Promise.Infrastructure.Repositories;
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

        public IContainer Services { get; private set; } = null!;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void RegisterServices()
        {
            base.RegisterServices();

            ContainerBuilder builder = new ContainerBuilder();

            // Theme Manager
            builder.RegisterType<ThemeManager>().SingleInstance();
            // Logger factory
            builder.Register(c =>
            {
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.ClearProviders();
                    builder.AddDebug();
                    builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                });
                return loggerFactory;
            }).As<ILoggerFactory>().SingleInstance();
            // Logger
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
            // Database context
            builder.Register(c =>
            {
                DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlite("Data Source=app.db");
                ApplicationContext context = new ApplicationContext(optionsBuilder.Options);
                return context;
            }).InstancePerLifetimeScope();
            // UoW
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            // Repositories
            builder.RegisterType<NoteRepository>().As<IRepository<Note>>().InstancePerLifetimeScope();
            builder.RegisterType<ReportRepository>().As<IRepository<Report>>().InstancePerLifetimeScope();
            // View Models
            builder.RegisterType<MainViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<NotesViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ReportsViewModel>().AsSelf().AsImplementedInterfaces().SingleInstance();
            // Views
            builder.Register(c => new MainWindow() { DataContext = c.Resolve<MainViewModel>() }).AsSelf().AsImplementedInterfaces().SingleInstance();
            builder.Register(c => new NotesView() { DataContext = c.Resolve<NotesViewModel>() }).AsImplementedInterfaces().SingleInstance();
            builder.Register(c => new ReportsView() { DataContext = c.Resolve<NotesViewModel>() }).AsImplementedInterfaces().SingleInstance();
            // View Locator
            builder.Register(c => new RxViewLocator(c.Resolve<ILifetimeScope>()));

            AutofacDependencyResolver resolver = builder.UseAutofacDependencyResolver();

            builder.RegisterInstance(resolver);

            Services = builder.Build();

            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // Setup logger
            _logger = Services.Resolve<ILogger<App>>();

            // Setup view locator
            Locator.CurrentMutable.RegisterLazySingleton(() => Services.Resolve<RxViewLocator>(), typeof(IViewLocator));

            // Select theme by system default
            ThemeManager manager = Services.Resolve<ThemeManager>();
            ThemeMode theme = ActualThemeVariant == ThemeVariant.Light ? ThemeMode.Light : ThemeMode.Dark;
            manager.ChangeTheme(theme);

            // Initialize the database so as not to freeze the UI when receiving the service
            Services.Resolve<ApplicationContext>();

            _logger.LogDebug("Opening the main view...");
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = Services.Resolve<MainWindow>();
            }

            base.OnFrameworkInitializationCompleted();
            _logger.LogDebug("Framework initialization completed successful");
        }
    }
}
