using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using Command = MvvmHelpers.Commands.Command;
using System;

namespace MyCoffee.ViewModels
{
    public class CoffeEquipmentViewModel : ViewModelBase

    {

        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Coffee>> CoffeeGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Coffee> FavoriteCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; private set; }
        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }

        public CoffeEquipmentViewModel()
        {
            Title = "Coffee Equipment";
            Coffee = new ObservableRangeCollection<Coffee>();
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Coffee>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Coffee>(Favorite);
            SelectedCommand = new AsyncCommand<object>(Selected);
            LoadMoreCommand = new Command(LoadMore);
            ClearCommand = new Command(Clear);
            DelayLoadMoreCommand = new Command(DelayLoadMore);

            if (Coffee.Count >= 20)
                return;

            var image = "coffee.png";

            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Sip of Sunshine", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Potent Potable", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "KKH", Image = image });
            Coffee.Add(new Coffee { Roaster = "Red Bottle", Name = "KKH", Image = image });
            Coffee.Add(new Coffee { Roaster = "Bottle", Name = "new", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Huge", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "old", Image = image });
            Coffee.Add(new Coffee { Roaster = "Brown Bottle", Name = "new", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yellow Bottle", Name = "BB", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "NN", Image = image });

            CoffeeGroups.Clear();

            CoffeeGroups.Add(new Grouping<string, Coffee>("Blue Bottle", Coffee.Where(c => c.Roaster == "Blue Bottle")));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Plz", Coffee.Where(c => c.Roaster == "Yes Plz")));

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Coffee>(Favorite);
        }
        async Task Favorite(Coffee coffee)
        {
            if (coffee == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Favorite", coffee.Name, "OK");

        }
        void LoadMore()
        {
            throw new NotImplementedException();
        }



        Coffee previouslySelected;
        Coffee selectedCoffee;
        public Coffee SelectedCoffee
        {
            get => SelectedCoffee;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "OK");
                    previouslySelected = value;
                    value = null;
                }

                selectedCoffee = value;
                OnPropertyChanged();
            }
        }

        public Func<object, Task> Selected { get; }
        public Action<object> Clear { get; private set; }
        public Action<object> DelayLoadMore { get; private set; }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Coffee.Clear();
            LoadMore();

            IsBusy = false;
        }

       /* void DelayLoadMore()
        {
            if (Coffee.Count <= 10)
                return;

            LoadMore();

        }*/
        /*public ICommand IncreaseCount { get; }

        int count = 0;
        string countDisplay = "Click me";
       /* public string CountDisplay
        {
            get => countDisplay;
            set => SetProperty(ref countDisplay, value);
            {
                if (value == countDisplay)
                    return;
                Console.WriteLine(countDisplay);
                countDisplay = value;
                OnPropertyChanged();

            }

        }*/

        //public Func<Task> CallServer { get; }

        /* void OnIncrease()
         {
             count++;
             CountDisplay = $"You clicked {count} time(s) ";
             Console.WriteLine(CountDisplay);
         }*/
    }

    public class Coffee
    {
        public string Roaster { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
