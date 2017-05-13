namespace MentData
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AccModel : DbContext
    {
        // Your context has been configured to use a 'AccModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MentData.AccModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AccModel' 
        // connection string in the application configuration file.
        public AccModel()
            : base("name=AccModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        //public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
    }

//    public class AspNetUsers
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//    }
}