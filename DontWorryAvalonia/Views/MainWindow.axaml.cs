using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DontWorryAvalonia.ViewModels;
using ReactiveUI;
using System;
using System.Threading.Tasks;

namespace DontWorryAvalonia.Views
{
    // ReactiveWindow°¡ ¹ºÁö ¸ð¸§...
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }
        private async Task DoShowDialogAsync(InteractionContext<MusicStoreViewModel,
                                        AlbumViewModel?> interaction)
        {
            var dialog = new MusicStoreWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<AlbumViewModel?>(this);
            interaction.SetOutput(result);

        }
    }
}