﻿using System;

using CDataSampleApp.Models;
using CDataSampleApp.Views;
using Xamarin.Forms;

namespace CDataSampleApp.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
            
            // Added Delete Processing.
            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DeleteItem", async (obj, deleteItem) =>
            {
                await DataStore.DeleteItemAsync(deleteItem.Id);
            });
        }
    }
}
