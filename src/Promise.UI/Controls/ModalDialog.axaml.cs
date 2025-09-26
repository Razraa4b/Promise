using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;

namespace Promise.UI.Controls
{
    public partial class ModalDialog : UserControl
    {
        public event Action<ModalDialog>? OnDialogShowed;
        public event Action<ModalDialog>? OnDialogClosed;

        public static readonly DirectProperty<ModalDialog, Control?> DialogProperty = AvaloniaProperty.RegisterDirect<ModalDialog, Control?>(
            nameof(Dialog),
            o => o.Dialog,
            (o, s) => o.Dialog = s
        );
        
        private Control? dialog;
        public Control? Dialog
        {
            get => dialog;
            set => SetAndRaise(DialogProperty, ref dialog, value);
        }

        public static ReactiveCommand<ModalDialog, Unit> ShowDialogCommand => ReactiveCommand.Create<ModalDialog, Unit>(dialog =>
        {
            Control dialogContent = dialog.GetTemplateChildren().First(c => c.Name == "PART_DialogContent");
            dialogContent.IsVisible = true;

            dialog.OnDialogShowed?.Invoke(dialog);

            return Unit.Default;
        });

        public static ReactiveCommand<ModalDialog, Unit> CloseDialogCommand => ReactiveCommand.Create<ModalDialog, Unit>(dialog =>
        {
            Control dialogContent = dialog.GetTemplateChildren().First(c => c.Name == "PART_DialogContent");
            dialogContent.IsVisible = false;

            dialog.OnDialogClosed?.Invoke(dialog);

            return Unit.Default;
        });

        public ModalDialog()
        {
            InitializeComponent();
        }
    }
}
