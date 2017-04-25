﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MentProject.Models
{
    public class Users : List<UserModel>
    {    
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime BDay { get; set; }
        public int Age { get { return (DateTime.Now.Year - BDay.Year); } }
        public string Photo { get; set; }
        public int IsDeleted { get; set; }
    }
}