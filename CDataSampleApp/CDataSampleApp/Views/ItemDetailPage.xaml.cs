using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CDataSampleApp.Models;
using CDataSampleApp.ViewModels;

namespace CDataSampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            var accepted = await this.DisplayAlert("タスクを削除します。", "よろしいですか？", "Yes", "No");

            if (accepted)
            {
                MessagingCenter.Send(this, "DeleteItem", viewModel.Item);
                await Navigation.PopAsync();
            }
        }

        async void UpdateItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UpdateItemPage(new ItemUpdateViewModel(viewModel.Item)));
        }
    }
}