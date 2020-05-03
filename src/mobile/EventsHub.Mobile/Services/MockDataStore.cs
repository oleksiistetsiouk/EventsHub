using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsHub.Mobile.Models;

namespace EventsHub.Mobile.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Sixth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Seventh item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Eigth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Nineth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Tenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Eleventh item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twelveth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Thirteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Fourteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Fifteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Sixteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Seventeenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Eighteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Nineteenth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentieth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentyfirst item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentysecond item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentythird item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentyfourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentyfifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentysixth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentyseventh item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentyeight item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Twentynineth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/The_Scream.jpg/800px-The_Scream.jpg", Text = "Thirtieth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false, int lastIndex = 0)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            var numberOfItemsPerPage = 6;

            return await Task.FromResult(items.Skip(lastIndex).Take(numberOfItemsPerPage));
        }
    }
}