using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparerApp.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// Shows the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<string> ShowAsync();
    }
}
