using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Service
{
    public class DBTagContext: DbContext
    {
        public DBTagContext() : base("DBTagContext") { }
        public virtual DbSet<DBTagInfo> Tags { get; set; }
    }
}