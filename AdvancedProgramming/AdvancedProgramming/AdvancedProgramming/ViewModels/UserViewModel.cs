﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvancedProgramming.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }  // Ejemplo: Admin, User

    }
}