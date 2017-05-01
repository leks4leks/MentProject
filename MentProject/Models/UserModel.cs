using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MentProject.Models
{
    public class Users : List<UserModel>
    {    
    }

    public class UserModel
    {
        public UserModel()
        {
            BDay = DateTime.Now;
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [ValidateDateRange]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BDay { get; set; }

        public int Age { get { return (DateTime.Now.Year - BDay.Year); } }
        public string Photo { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int IsDeleted { get; set; }
    }
}