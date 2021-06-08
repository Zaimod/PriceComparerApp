using System;
using System.Collections.Generic;
using System.Text;

namespace PriceComparerApp.Models.DataTransferObjects
{
    public class UserDtoForUpdate
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
