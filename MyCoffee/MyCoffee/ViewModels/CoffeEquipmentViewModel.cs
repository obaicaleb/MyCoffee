using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using Command = MvvmHelpers.Commands.Command;

namespace MyCoffee.ViewModels
{
    public class CoffeEquipmentViewModel : ViewModelBase

    {

        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Coffee>> CoffeeGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public CoffeEquipmentViewModel()
        {
            // IncreaseCount = new Command(OnIncrease);
            //CallServerCommand = new AsyncCommand(CallServer);
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Coffee>>();

            Title = "Coffee Equipment";

            var image = "https://www.yesplz.coffee/app/uploads/2020/11/emptybag-min";

            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Sip of Sunshine", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Potent Potable", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "KKH", Image = image });

            CoffeeGroups.Add(new Grouping<string, Coffee>("Blue Bottle", Coffee.Where(c => c.Roaster == "Blue Bottle")));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Plz", Coffee.Where(c => c.Roaster == "Yes Plz")));

            RefreshCommand = new AsyncCommand(Refresh);
        }

        public ICommand CallServerCommand { get; }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            IsBusy = false;
        }

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
