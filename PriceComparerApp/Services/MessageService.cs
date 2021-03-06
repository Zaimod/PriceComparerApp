using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PriceComparerApp.Services
{
    public class MessageService : IMessageService
    {
        /// <summary>
        /// Shows the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ShowAsync()
        {
           return await App.Current.MainPage.DisplayPromptAsync("Attention", "Invalid Check email.\nWe sent message with code on your email.Please check and put code here\nExpiration time: 3 minutes!", maxLength: 6, keyboard: Keyboard.Numeric);
        }
    }
}
