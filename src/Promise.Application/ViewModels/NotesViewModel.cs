using DynamicData;
using Microsoft.Extensions.Logging;
using Promise.Domain.Contracts;
using Promise.Domain.Entities;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Promise.Application.ViewModels
{
    public class NotesViewModel : ViewModelBase, IRoutableViewModel
    {
        private readonly ILogger<NotesViewModel> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public string? UrlPathSegment { get; } = "notes";
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
                if(Notes.Count == 0)
                    await LoadNotes();

                // Interval-called function to update the current content for a note
                Observable.Interval(TimeSpan.FromSeconds(2))
                    .Where(_ => isNoteContentChanged)
                    // If content changed, wait 0.5 seconds and save it
                    .Throttle(TimeSpan.FromSeconds(0.5))
                    .SelectMany(_ => Observable.FromAsync(ScheduleContentSave))
                    .Subscribe()
                    .DisposeWith(disposables);
            });
        }

        public async Task LoadNotes()
        {
            Note[] notes = (await _unitOfWork.NoteRepository.GetAll()).ToArray();
            Notes.AddRange(notes);
        }

        private async Task ScheduleContentSave()
        {
            if (SelectedNote == null) return;

            await _semaphore.WaitAsync();

            await _unitOfWork.Begin();
            try
            {
                await _unitOfWork.NoteRepository.Update(SelectedNote);
                await _unitOfWork.Commit();

                isNoteContentChanged = false;
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Occursed exception: {ex.Message}");
                await _unitOfWork.Rollback();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task<Unit> CreateNote(string title)
        {
            Note note = new Note(title, "")
            {
                CreationTime = DateTime.Now,
                LastChangedTime = DateTime.Now
            };

            await _semaphore.WaitAsync();

            await _unitOfWork.Begin();
            try
            {
                await _unitOfWork.NoteRepository.Add(note);
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Occursed exception: {ex}");
                await _unitOfWork.Rollback();
            }
            finally
            {
                _semaphore.Release();
            }

            Notes.Add(note);

            return Unit.Default;
        }

        private async Task DeleteNote()
        {
            if (SelectedNote == null) return;

            await _semaphore.WaitAsync();

            await _unitOfWork.Begin();
            try
            {
                await _unitOfWork.NoteRepository.Delete(SelectedNote);
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Occursed exception: {ex}");
                await _unitOfWork.Rollback();
            }
            finally
            {
                _semaphore.Release();
            }

            Notes.Remove(SelectedNote);
        }
    }
}
