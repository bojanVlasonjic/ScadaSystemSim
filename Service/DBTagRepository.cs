using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service
{
    public class DBTagRepository
    {
        private DBTagContext context;


        public DBTagRepository()
        {
            context = new DBTagContext();
        }



        public List<DBTagInfo> getTags()
        {
            return context.Tags.ToList();
        }

        public DBTagInfo getTag(string tagID)
        {
            var tag = (from t in context.Tags
                       where t.tagID == tagID
                       select t);

            return tag.FirstOrDefault();
        }

        public bool saveTag(DBTagInfo tagInfo)
        {
            if(getTag(tagInfo.tagID) != null)
            {
                return false;
            }

            context.Tags.Add(tagInfo);
            context.SaveChanges();

            return true;
        }

        public bool removeTag(String tagID)
        {

            // context.Remove(context.Tags.Single(t => t.tagID == tagID));
            //context.SaveChanges();
            return true;
        }

    }
}