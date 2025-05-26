using ReactiveUI;

namespace Promise.Domain.Contracts
{
    public interface IViewManager
    {
        IViewFor<TViewModel> GetViewByViewModel<TViewModel>() where TViewModel : class;
        IViewFor<TViewModel> GetViewByViewModel<TViewModel>(TViewModel viewModel) where TViewModel : class;
        void ShowView<TView>(IViewFor<TView> view) where TView : class;
        Task<object?> ShowViewDialog<TViewOwner, TView>(IViewFor<TViewOwner> owner, IViewFor<TView> view) where TView : class 
                                                                                                          where TViewOwner : class;
        void CloseView<TView>(IViewFor<TView> view) where TView : class;
    }
}
