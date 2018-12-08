using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CDataSampleApp.Models;
using CDataSampleApp.ViewModels;

namespace CDataSampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateItemPage : ContentPage
    {
        ItemUpdateViewModel viewModel; 

        public UpdateItemPage(ItemUpdateViewModel viewModel)
        {
            InitializeComponent();
            
            BindingContext = this.viewModel = viewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var accepted = await this.DisplayAlert("タスクを更新します。", "よろしいですか？", "Yes", "No");

            if (accepted)
            {
                MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
                await Navigation.PopAsync();
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}