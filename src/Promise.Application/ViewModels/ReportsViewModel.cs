using ReactiveUI;

namespace Promise.Application.ViewModels
{
    public class ReportsViewModel : BaseViewModel, IRoutableViewModel
    {
        public string? UrlPathSegment { get; } = "/reports";
        public IScreen HostScreen { get; }

        public ReportsViewModel(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
