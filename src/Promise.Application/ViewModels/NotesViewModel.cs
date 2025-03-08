using ReactiveUI;

namespace Promise.Application.ViewModels
{
    public class NotesViewModel : BaseViewModel, IRoutableViewModel
    {
        public string? UrlPathSegment { get; } = "/notes";
        public IScreen HostScreen { get; }

        public NotesViewModel(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
