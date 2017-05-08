using MentData;
using MentProject.Helper;
using MentRepository.RepModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace MentRepository.Repository
{
    public class AccRepository : IAccRepository
    {
        public AccRepository()
        {
            _db = new MentDBEntities();
        }
        private MentDBEntities _db;

        AccRepModel IAccRepository.GetAcc(string log, string pass)
        {
            return GetAcc(log, pass);
        }

        private AccRepModel GetAcc(string log, string pass)
        {
            return AccRepMapper.AccToAccRepModelMapper(_db.Accs.Where(_ => _.Login == log && _.Pass == pass).FirstOrDefault());
        }

        AccRepModel IAccRepository.Register(AccRepModel entity)
        {
            _db.Set<Acc>().AddOrUpdate(AccRepMapper.AccRepModelToAccMapper(entity));
            _db.SaveChanges();
            return GetAcc(entity.Login, entity.Pass);
        }
    }
}