using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DontWorryAvalonia.ViewModels;
using ReactiveUI;
using System;

namespace DontWorryAvalonia.Views
{
    public partial class MusicStoreWindow : ReactiveWindow<MusicStoreViewModel>
    {
        public MusicStoreWindow()
        {
            InitializeComponent();

            if (Design.IsDesignMode) return;

            this.WhenActivated(action => action(ViewModel!.BuyMusicCommand.Subscribe(Close)));
        }
    }
}
