using Avalonia.Threading;
using DynamicData;
using Promise.Domain.Contracts;
using Promise.Domain.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;

namespace Promise.Application.ViewModels
{
    public class NotesViewModel : BaseViewModel, IRoutableViewModel
    {
        private readonly ILogger<NotesViewModel> _logger;
        private readonly IRepository<Note> _noteRepository;

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
            }
        }

        public ReactiveCommand<Unit, Unit> CreateNoteCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteNoteCommand { get; }

        public NotesViewModel(IScreen screen, ILogger<NotesViewModel> logger, IRepository<Note> noteRepository)
        {
            HostScreen = screen;
            _logger = logger;
            _noteRepository = noteRepository;

            CreateNoteCommand = ReactiveCommand.Create(() => { /* TODO */ });
            DeleteNoteCommand = ReactiveCommand.Create(() => { /* TODO */ });
        }
    }
}
