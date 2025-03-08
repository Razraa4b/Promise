using Avalonia.ReactiveUI;
using Promise.Application.ViewModels;

namespace Promise.UI.Views
{
    public partial class MainView : ReactiveWindow<MainViewModel>
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}