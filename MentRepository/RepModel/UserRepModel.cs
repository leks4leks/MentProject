using MentData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentRepository.RepModel
{
    public class UsersRepModel : List<UserRepModel>
    { }
    public class UserRepModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BDay { get; set; }
        public string Photo { get; set; }
        public int IsDeleted { get; set; }
    }
}