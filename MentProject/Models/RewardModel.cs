using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MentProject.Models
{
    public class Rewards : List<RewardModel>
    {    
    }

    public class RewardModel
    {
        public RewardModel()
        {
            Description = string.Empty;
        }

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s\-]{1,50}$", ErrorMessage = "Title is incorrect")]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }        
        public string Photo { get; set; }     
        [Required]   
        public HttpPostedFileBase File { get; set; }
        public int IsDeleted { get; set; }
        public bool IsSetRewardForUser { get; set; }
        public int UserId { get; set; }

    }
}