using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MentProject.Enums
{
    public enum RolesEnum
    {
        [Display(Name = "admin")]
        admin,

        [Display(Name = "aspadmin")]
        aspadmin,

        [Display(Name = "user")]
        user
    }
}