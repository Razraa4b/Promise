using Promise.Domain.Contracts;
using Promise.Domain.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Promise.Application.ViewModels
{
    public class NotesViewModel : BaseViewModel, IRoutableViewModel
    {
        private readonly ILogger<NotesViewModel> _logger;
        private readonly IUnitOfWork<Note> _unitOfWork;
        private readonly IRepository<Note> _repository;

        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

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

        public ReactiveCommand<Note, Unit> UpdateNoteCommand { get; set; }
        public ReactiveCommand<Note, Unit> DeleteNoteCommand { get; set; }

        public NotesViewModel(IScreen screen, ILogger<NotesViewModel> logger, IUnitOfWork<Note> unitOfWork)
        {
            HostScreen = screen;

            _logger = logger;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository();

            UpdateNoteCommand = ReactiveCommand.Create((Note n) => UpdateNote(n));
            DeleteNoteCommand = ReactiveCommand.Create((Note n) => DeleteNote(n));
        }

        private void UpdateNote(Note note)
        {
            try
            {
                _repository.Update(note);
                _unitOfWork.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                _logger.Log(Domain.Enums.LogLevel.Error, "Error when updating data in UpdateNote() method");
            }
        }

        private void DeleteNote(Note note)
        {
            try
            {
                _repository.Delete(note);
                _unitOfWork.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                _logger.Log(Domain.Enums.LogLevel.Error, "Error when deleting data in DeleteNote() method");
            }
        }
    }
}
