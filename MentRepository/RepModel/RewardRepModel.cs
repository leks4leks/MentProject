using MentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentRepository.RepModel
{
    public class RewardsRepModel : List<RewardRepModel>
    { }
    public class RewardRepModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int IsDeleted { get; set; }
    }
}