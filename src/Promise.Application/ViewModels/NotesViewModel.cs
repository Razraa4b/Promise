using Promise.Domain.Contracts;
using Promise.Domain.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Promise.Application.ViewModels
{
    public class NotesViewModel : BaseViewModel, IRoutableViewModel
    {
        private readonly IUnitOfWork<Note> _unitOfWork;
        private readonly IRepository<Note> _repository;

        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>() { };

        private Note? currentNote;
        public Note? CurrentNote
        {
            get => currentNote;
            set
            {
               currentNote = value;
                this.RaisePropertyChanged(nameof(CurrentNote));

                if (CurrentNote != null) IsCurrentNoteSelected = true;
                else IsCurrentNoteSelected = false;
                this.RaisePropertyChanged(nameof(IsCurrentNoteSelected));
            }
        }
        public bool IsCurrentNoteSelected { get; set; } = false;

        public string? UrlPathSegment { get; } = "/notes";
        public IScreen HostScreen { get; }

        public ReactiveCommand<Unit, Unit> UpdateNoteCommand { get; set; }
        public ReactiveCommand<Unit, Unit> DeleteNoteCommand { get; set; }

        public NotesViewModel(IScreen screen, IUnitOfWork<Note> unitOfWork)
        {
            HostScreen = screen;

            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository();

            UpdateNoteCommand = ReactiveCommand.Create(UpdateNote);
            DeleteNoteCommand = ReactiveCommand.Create(DeleteNote);
        }

        private void UpdateNote()
        {
            if (CurrentNote == null) return;

            try
            {
                _repository.Update(CurrentNote);
                _unitOfWork.SaveChanges();
            }
            catch (InvalidOperationException)
            {

            }
        }

        private void DeleteNote()
        {
            if (CurrentNote == null) return;

            try
            {
                _repository.Delete(CurrentNote);
                _unitOfWork.SaveChanges();
            }
            catch (InvalidOperationException)
            {

            }
        }
    }
}
