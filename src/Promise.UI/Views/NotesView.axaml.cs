using Avalonia.ReactiveUI;
using Promise.Application.ViewModels;

namespace Promise.UI.Views
{
    public partial class NotesView : ReactiveUserControl<NotesViewModel>
    {
        public NotesView()
        {
            InitializeComponent();
        }
    }
}