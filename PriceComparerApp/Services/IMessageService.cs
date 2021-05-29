using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriceComparerApp.Services
{
    public interface IMessageService
    {
        Task<string> ShowAsync();
    }
}
