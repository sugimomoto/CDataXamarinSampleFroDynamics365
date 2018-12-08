using System;

using CDataSampleApp.Models;
using CDataSampleApp.Views;
using Xamarin.Forms;

namespace CDataSampleApp.ViewModels
{
    public class ItemUpdateViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemUpdateViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
            
            // Added Delete Processing.
            MessagingCenter.Subscribe<UpdateItemPage, Item>(this, "UpdateItem", async (obj, updateItem) =>
            {
                await DataStore.UpdateItemAsync(updateItem);
            });
        }
    }
}
