using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace PracticaMAUI.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Character> characters;

        public ObservableCollection<Character> Characters
        {
            get => characters;
            set
            {
                characters = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Characters = new ObservableCollection<Character>();
            LoadData();
        }

        private async void LoadData()
        {
            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetFromJsonAsync<ApiResponse>("https://rickandmortyapi.com/api/character");

                if (response != null && response.Results != null)
                {
                    foreach (var item in response.Results)
                    {
                        Characters.Add(new Character
                        {
                            Name = item.Name,
                            Image = item.Image,
                            Status = item.Status,
                            Species = item.Species,
                            Type = item.Type,
                            Description = $"Status: {item.Status}, Species: {item.Species}"
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class ApiResponse
    {
        public List<Character> Results { get; set; }
    }
}
