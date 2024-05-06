using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using DontWorryAvalonia.Models;
using System.Reactive.Concurrency;
using System.Net.Http;
using System.Collections.Generic;
using System;

namespace DontWorryAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // 아래의 ICommand 기반으로 컨트롤 할 수 있다.
        public ICommand BuyMusicCommand { get; }

        public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }

        // Observable Collection
        public ObservableCollection<AlbumViewModel> Albums { get; } = new();

        public MainWindowViewModel()
        {

            ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();

            RxApp.MainThreadScheduler.Schedule(LoadAlbums);

            BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();

                var result = await ShowDialog.Handle(store);
                if (result != null)
                {
                    Albums.Add(result);
                    // load data
                    await result.SaveToDiskAsync();
                }
            });
        }
        private async void LoadAlbums()
        {
            var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x));

            foreach (var album in albums)
            {
                Albums.Add(album);
            }

            foreach (var album in Albums.ToList())
            {
                await album.LoadCover();
            }
        }

    }
}
