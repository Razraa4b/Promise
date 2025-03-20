using Avalonia.ReactiveUI;
using Promise.Application.ViewModels;

namespace Promise.UI.Views
{
    public partial class ReportsView : ReactiveUserControl<ReportsViewModel>
    {
        public ReportsView()
        {
            InitializeComponent();
        }
    }
}