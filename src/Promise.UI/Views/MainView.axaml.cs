using Avalonia.Controls;
using Promise.Application.ViewModels;

namespace Promise.UI.Views
{
    public partial class MainView : Window
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        // Not used, needed to display the window constructor in my Visual Studio
        public MainView()
        {
            InitializeComponent();
        }
    }
}