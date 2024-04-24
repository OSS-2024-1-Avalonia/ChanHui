using ReactiveUI;
using System.Reactive.Linq;
using System.Windows.Input;

namespace DontWorryAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // 아래의 ICommand 기반으로 컨트롤 할 수 있다.
        public ICommand BuyMusicCommand { get; }

        public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }

        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();

            BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();

                var result = await ShowDialog.Handle(store);
            });
        }
    }
}
