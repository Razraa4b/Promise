using DynamicData;
using Microsoft.Extensions.Logging;
using Promise.Domain.Contracts;
using Promise.Domain.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Promise.Application.ViewModels
{
    public class NotesViewModel : ViewModelBase, IRoutableViewModel
    {
        private readonly ILogger<NotesViewModel> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public string? UrlPathSegment { get; } = "/notes";
        public IScreen HostScreen { get; }

        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        private Note? selectedNote;
        public Note? SelectedNote
        {
            get => selectedNote;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedNote, value);
                this.RaisePropertyChanged(nameof(SelectedNoteContent));
            }
        }

        public string? SelectedNoteContent
        {
            get => selectedNote?.Content;
            set
            {
                if (SelectedNote == null || value == null) return;

                SelectedNote.Content = value;
                isNoteContentChanged = true;
            }
        }

        private bool isNoteContentChanged;

        public ReactiveCommand<Unit, Unit> ScheduleContentSaveCommand { get; init; }
        public ReactiveCommand<string, Unit> CreateNoteCommand { get; init; }
        public ReactiveCommand<Unit, Unit> DeleteNoteCommand { get; init; }

        public NotesViewModel(IScreen screen, ILogger<NotesViewModel> logger, IUnitOfWork unitOfWork)
        {
            HostScreen = screen;
            _logger = logger;
            _unitOfWork = unitOfWork;

            ScheduleContentSaveCommand = ReactiveCommand.CreateFromTask(ScheduleContentSave);
            CreateNoteCommand = ReactiveCommand.CreateFromTask<string, Unit>(CreateNote);
            DeleteNoteCommand = ReactiveCommand.CreateFromTask(DeleteNote);

            this.WhenActivated(async (CompositeDisposable disposables) =>
            {
                await LoadNotes();

                Observable.Timer(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1))
                    .Subscribe(x => Task.Run(ScheduleContentSave))
                    .DisposeWith(disposables);

                Disposable.Create(Notes.Clear).DisposeWith(disposables);
            });
        }

        public async Task LoadNotes()
        {
            Note[] notes = (await _unitOfWork.NoteRepository.GetAll()).ToArray();
            Notes.AddRange(notes);
        }

        private async Task ScheduleContentSave()
        {
            if (SelectedNote == null || !isNoteContentChanged) return;

            isNoteContentChanged = false;
            try
            {
                await _unitOfWork.Begin();
                await _unitOfWork.NoteRepository.Update(SelectedNote);
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Occursed exception: {ex.Message}");
                await _unitOfWork.Rollback();
            }
        }

        private async Task<Unit> CreateNote(string title)
        {
            Note note = new Note(title, "")
            {
                CreationTime = DateTime.Now,
                LastChangedTime = DateTime.Now
            };

            try
            {
                await _unitOfWork.Begin();
                await _unitOfWork.NoteRepository.Add(note);
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Occursed exception: {ex.Message}");
                await _unitOfWork.Rollback();
            }

            Notes.Add(note);

            return Unit.Default;
        }

        private async Task DeleteNote()
        {
            if (SelectedNote == null) return;

            Note? note = await _unitOfWork.NoteRepository.GetByTitle(SelectedNote.Title);
            if(note == null) return;

            try
            {
                await _unitOfWork.Begin();
                await _unitOfWork.NoteRepository.Delete(note);
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Occursed exception: {ex.Message}");
                await _unitOfWork.Rollback();
            }

            Notes.Remove(note);
        }
    }
}
