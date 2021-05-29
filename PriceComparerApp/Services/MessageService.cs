using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PriceComparerApp.Services
{
    public class MessageService : IMessageService
    {
        public async Task<string> ShowAsync()
        {
           return await App.Current.MainPage.DisplayPromptAsync("Attention", "Invalid Check email.\nWe sent message with code on your email.Please check and put code here", maxLength: 6, keyboard: Keyboard.Numeric);
        }
    }
}
