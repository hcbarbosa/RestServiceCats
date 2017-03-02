using RestServiceCats.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestServiceCats.ViewModels
{
    public class CatsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private bool Busy;
        public bool IsBusy
        {
            get
            {
                return Busy;
            }
            set
            {
                Busy = value;
                OnPropertyChanged();
                GetCatsCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<Cat> Cats { get; set; }
        public CatsViewModel()
        {
            Cats = new ObservableCollection<Models.Cat>();
            GetCatsCommand = new Command(async () => await GetCats(), () => !IsBusy);
        }

        async Task GetCats()
        {
            if (!IsBusy)
            {
                Exception Error = null;
                try
                {
                    IsBusy = true;
                    var Repository = new Repository();
                    var Items = await Repository.GetCats();
                    Cats.Clear();

                    foreach (var Cat in Items)
                    {
                        Cats.Add(Cat);
                    }
                }
                catch (Exception ex)
                {
                    Error = ex;
                }
                finally
                {
                    IsBusy = false;
                    if (Error != null)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        "Error!", Error.Message, "OK");
                    }
                }
            }
            return;
        }

        public Command GetCatsCommand { get; set; }


    }
}
