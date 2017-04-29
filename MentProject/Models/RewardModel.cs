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
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int IsDeleted { get; set; }
    }
}