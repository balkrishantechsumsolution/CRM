﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeedManagement.Models
{
    interface User
    {
        public string Id { get; set; }
        
        public string Password { get; set; }
       
        public string Name { get; set; }
    }
    public class UsersModel: User
    {
        [Required(ErrorMessage = "Please Enter UserName")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
