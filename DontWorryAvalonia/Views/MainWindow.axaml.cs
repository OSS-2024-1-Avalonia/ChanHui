using Avalonia.Controls;
using Avalonia.ReactiveUI;
using DontWorryAvalonia.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            callGovData();

        }
        private async Task DoShowDialogAsync(InteractionContext<MusicStoreViewModel,
                                        AlbumViewModel?> interaction)
        {
            var dialog = new MusicStoreWindow();
            dialog.DataContext = interaction.Input;

            var result = await dialog.ShowDialog<AlbumViewModel?>(this);
            interaction.SetOutput(result);

        }

        private async void callGovData()
        {
            string goUrl = "http://apis.data.go.kr/";
            string apiKey = "LaKtPwuX2ODy4mmV6wImpCKXhwR10x9IhU5oyr7vhq5wpoKGJLL8tJhsTcoeMBaV5TF%2BmzUO42DfWmORSSS72Q%3D%3D";
            int pageno = 1;
            while (true)
            {
                var param = new { serviceKey = apiKey, pageNo = pageno, numOfRows = "1000", type = "json" };

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri(goUrl);
                    var queryParams = new Dictionary<string, string>
                    {
                        {"serviceKey", apiKey },
                        {"pageNo", pageno.ToString() },
                        {"numOfRows", "1000" },
                        {"type","json" }
                    };
                    var queryString = new FormUrlEncodedContent(queryParams);
                    string url = $"1741000/StanReginCd?{queryString}";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(result);
                    }
                }
            }
        }
    }
}