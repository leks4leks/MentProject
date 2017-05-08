using MentData;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MentProject.Helper
{
    public static class AccRepMapper
    {
        /// <summary>
        /// Acc to AccRepModel
        /// </summary>
        /// <param name="user">Acc</param>
        /// <returns>AccRepModel</returns>
        public static AccRepModel AccToAccRepModelMapper(Acc acc)
        {
            return new AccRepModel()
            {
                Id = acc.Id,
                Login = acc.Login,
                Pass = acc.Pass
            };
        }

        /// <summary>
        /// AccRepModel to Acc
        /// </summary>
        /// <param name="userRepModel">AccRepModel</param>
        /// <returns>Acc</returns>
        public static Acc AccRepModelToAccMapper(AccRepModel accRep)
        {
            return new Acc()
            {
                Id = accRep.Id,
                Pass = accRep.Pass,
                Login = accRep.Login
            };
        }        
    }
}