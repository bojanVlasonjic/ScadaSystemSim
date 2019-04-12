using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScadaModel;
using Newtonsoft.Json;
using System.IO;

namespace Service
{
    public class TagIO
    {

        private string currentPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
        private const string directory_path = "/data";
        private const string file_path = "/tags.txt";

        public TagIO()
        {

        }

        public TagsToSerialize readTagsFromFile()
        {

            try
            {
                using (StreamReader r = new StreamReader(currentPath + directory_path + file_path))
                {
                    string json = r.ReadToEnd();
                    TagsToSerialize tags = JsonConvert.DeserializeObject<TagsToSerialize>(json);
                    return tags;
                }

            } catch(Exception)
            {
                return null;
            }
            
        }

        public bool writeTagsToFile(TagsToSerialize tags)
        {

            try
            {
                using (StreamWriter file = File.CreateText(currentPath + directory_path + file_path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, tags);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
           

        }
    }
}