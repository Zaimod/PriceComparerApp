﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PriceComparerApp.Models.DataTransferObjects
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string City { get; set; }
        public string Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
