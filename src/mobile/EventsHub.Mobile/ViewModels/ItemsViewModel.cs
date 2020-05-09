using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using EventsHub.Mobile.Models;
using System.Linq;
using EventsHub.Mobile.Extensions;

namespace EventsHub.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command ItemTresholdReachedCommand { get; set; }
        public Command RefreshItemsCommand { get; set; }
        public const string ScrollToPreviousLastItem = "Scroll_ToPrevious";
        private int _itemTreshold;
        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        public ItemsViewModel()
        {
            ItemTreshold = 1;
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            RefreshItemsCommand = new Command(async () =>
            {
                await ExecuteLoadItemsCommand();
                IsRefreshing = false;
            });
        }

        async Task ItemsTresholdReached()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await DataStore.GetItemsAsync(true, Items.Count);

                var previousLastItem = Items.Last();
                Items.AddRange(items);
                Debug.WriteLine($"{items.Count()} {Items.Count} ");
                if (items.Count() == 0)
                {
                    ItemTreshold = -1;
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ItemTreshold = 1;
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Items.AddRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}