using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ScadaModel;
using System.Threading;

namespace Service
{

    public class ScadaService : I_RTU, I_DB_Manager, I_Trending, I_Alarm_Display
    {

        /* Service attributes */

        //drajveri
        static RealTimeDriver rt_driver = new RealTimeDriver();
        static SimulationDriver sim_driver = new SimulationDriver();

        static Dictionary<int, string> rtus = new Dictionary<int, string>(); // id rtu-a: adresa na koju je povezan
        static Dictionary<string, Tag> tags = new Dictionary<string, Tag>(); // tag-id: Tag

        //Callback channels
        static I_Alarm_Display_CB alarmDisplayProxy = null;
        static I_Trending_CB trendingProxy = null;

        //procesiranje tag-ova
        static Dictionary<string, Thread> tag_threads = new Dictionary<string, Thread>();

        //za upis i citanje iz fajla
        static TagIO tagIO = new TagIO();
        static AlarmIO alarmIO = new AlarmIO();

        //tagovi koje upisujem i citam iz fajla
        static TagsToSerialize tagsToSer = new TagsToSerialize();




        /******************************** I_RTU_IMPLEMENTED METHODS ********************************/

        public bool register_RTU(string address)
        {

            bool addrAvailable;

            lock (rt_driver)
            {
                addrAvailable = rt_driver.addressAvailable(address);
            }


            if (addrAvailable)
            {
                rtus[rtus.Count + 1] = address; //memorisem id rtu-a i adresu na koju je povezan
                return true;

            }

            return false;

        }

        public string getAvailableAddresses() {

            lock (rt_driver)
            {
                return rt_driver.getAvailableAddresses();
            }

        }

        public void sendDataToSvc(string address, int value)
        {

            //TODO: digitalni potpis

            lock (rt_driver)
            {
                rt_driver.addValue(address, value);
            }

        }


        /******************************** I_DB_Manager implemented methods ******************************** */

        public bool addTag(Tag tag, string driver)
        {

            if (driver.Equals("Real time driver"))
            {
                tag.Driver = rt_driver;
                tag.DriverID = "rtd";
            } else
            {
                tag.Driver = sim_driver;
                tag.DriverID = "sd";
            }

            lock (tags)
            {
                if (tags.ContainsKey(tag.ID))
                {
                    return false;
                }

                tags[tag.ID] = tag;

                //Start tag processing
                processTag(tag.ID);

                tagsToSer.addTag(tag);
                tagIO.writeTagsToFile(tagsToSer);
                addTagToDatabase(new DBTagInfo { tagID = tag.ID, tagDescription = tag.Description, IOaddress = tag.IOAddress });
            }

            return true;
        }


        public bool removeTag(string tagID)
        {
            lock (tags)
            {
                if (!tags.ContainsKey(tagID))
                {
                    return false;
                }

                abortProcessing(tagID);

                tagsToSer.removeTag(tags[tagID]);
                tags.Remove(tagID);

                tagIO.writeTagsToFile(tagsToSer); //memorize changes
            }

            //TODO: ukloni iz baze
            return true;
        }


        public bool addAlarm(Alarm alarm)
        {
            lock (tags) {

                if (!tags.ContainsKey(alarm.TagID))
                {
                    return false;
                }

                if (tags[alarm.TagID] is TagInput)
                {
                    ((TagInput)tags[alarm.TagID]).addAlarm(alarm);
                    //TODO; sacuvaj u bazu

                    tagsToSer.removeTag(tags[alarm.TagID]);
                    tagsToSer.addTag(tags[alarm.TagID]);

                    tagIO.writeTagsToFile(tagsToSer);
                    return true;
                }

            }

            return false;
        }


        public bool removeAlarm(string tagID, string alarmID)
        {
            lock (tags)
            {

                if (!tags.ContainsKey(tagID))
                {
                    return false;
                }

                if (tags[tagID] is TagInput)
                {
                    int indexToRemove = -1;
                    for (int i = 0; i < ((TagInput)tags[tagID]).Alarms.Count; i++)
                    {
                        if (((TagInput)tags[tagID]).Alarms[i].AlarmID.Equals(alarmID)) {
                            indexToRemove = i;
                            break;
                        }
                    }

                    if (indexToRemove != -1)
                    {
                        ((TagInput)tags[tagID]).Alarms.RemoveAt(indexToRemove);

                        tagsToSer.removeTag(tags[tagID]);
                        tagsToSer.addTag(tags[tagID]);

                        tagIO.writeTagsToFile(tagsToSer);
                        //TODO: sacuvaj  u bazu
                        return true;
                    }
                }

            }

            return false;
        }


        public List<string> getRTDAddresses()
        {
            lock (rt_driver)
            {
                return rt_driver.getAddresses();
            }
        }


        public List<string> getSDAvailableAddr()
        {
            lock (sim_driver)
            {
                return sim_driver.getAvailableAddresses();
            }
        }

        public Tag[] getTags()
        {
            lock (tags)
            {

                if (tags.Count == 0)
                {
                    tagsToSer = tagIO.readTagsFromFile();
                    if (tagsToSer != null)
                    {
                        processLoadedTags();
                    } else
                    {
                        tagsToSer = new TagsToSerialize();
                    }


                }

                return tags.Values.ToArray();
            }
        }

        public bool sendManualValue(string tagID, int value)
        {
            if(tags[tagID] is TagOutput)
            {
                return false;
            }

            if(tags[tagID].Driver is RealTimeDriver)
            {
                return false;
            }

            lock(tags)
            {
                if(((TagInput)tags[tagID]).Read == readType.AUTO)
                {
                    return false;
                }

                lock(sim_driver)
                {
                    sim_driver.manualWrite(tags[tagID].IOAddress, value);
                }
            }

            return true;
        }

        public Tag getTagForEditing(string tagID)
        {
            Tag tagToReturn = null;

            lock(tags)
            {
                if(tags.ContainsKey(tagID))
                {
                    tagToReturn = tags[tagID];
                    removeTag(tagID);
                }
            }

            return tagToReturn;
        }

        public List<Alarm> getAlarms(string tagID)
        {
            
            lock(tags)
            {
                if(tags.ContainsKey(tagID) && tags[tagID] is TagInput)
                {
                    return ((TagInput)tags[tagID]).Alarms;
                }
               
            }

            return null;
        }


        /****************************************** I_TRENDING IMPLEMENTED METHODS *******************************************/

        public void initTrending()
        {
            trendingProxy = OperationContext.Current.GetCallbackChannel<I_Trending_CB>();
        }


        /****************************************** I_ALARM_DISPLAY IMPLEMENTED METHODS *******************************************/

        public void initAlarmDisplay()
        {
            alarmDisplayProxy = OperationContext.Current.GetCallbackChannel<I_Alarm_Display_CB>();
        }



        /****************************************** TAG PROCESSING *******************************************/

        public void processTag(string tagID)
        {

            if (tag_threads.ContainsKey(tagID))
            {
                return;
            }


            if (tags[tagID] is TagInput)
            {
                tag_threads[tagID] = new Thread(new ParameterizedThreadStart(processInputTag));

            }
            else if (tags[tagID] is TagOutput)
            {
                tag_threads[tagID] = new Thread(new ParameterizedThreadStart(processOutputTag));

            }
            else
            {
                throw new Exception("Unknown tag type");
            }

            tag_threads[tagID].Start(tagID);

        }

        private void processInputTag(object inputTagID)
        {
            string tagID = (string)inputTagID;

            while (true)
            {
                //Iscitaj vrednost sa uredjaja
                int scannedVal = 0;

                if(!tags.ContainsKey(tagID))
                {
                    return;
                }

                //Proveri koji drajver je u pitanju
                if (tags[tagID].Driver is RealTimeDriver)
                {
                    lock (rt_driver)
                    {
                        scannedVal = rt_driver.readValue(tags[tagID].IOAddress);
                    }
                }
                else
                {
                    //ako je simulacioni moramo proveriti koje skeniranje je ukljuceno
                    if(((TagInput)tags[tagID]).Read == readType.AUTO)
                    {
                        lock (sim_driver)
                        {
                            scannedVal = sim_driver.readValue(tags[tagID].IOAddress);
                        }
                    } else
                    {
                        lock(sim_driver)
                        {
                            scannedVal = sim_driver.manualRead(tags[tagID].IOAddress); //scan manual value
                            sim_driver.manualWrite(tags[tagID].IOAddress, 0); //put back default value

                        }
                    }
                    
                }


                //provera za alarm da li se dogodio
                foreach (Alarm alarm in ((TagInput)tags[tagID]).Alarms)
                {
                    if (scannedVal < alarm.LowLimit || scannedVal > alarm.HightLimit)
                    {
                        if (alarmDisplayProxy != null)
                        {
                            DateTime currentTime = DateTime.Now;
                            alarm.AlarmTime = currentTime;

                            //ispisi na konzolu ako je ukljuceno skeniranje
                            if(((TagInput)tags[tagID]).Scan == scanType.ON)
                            {
                                alarmDisplayProxy.logAlarmToConsole(alarm, scannedVal);
                            }

                            lock(alarmIO)
                            {
                                alarmIO.writeAlarmToFile(alarm);
                            }
                            
                        }
                    }
                }

                //Posalji podatke na trending
                
                if (trendingProxy != null)
                {
                     trendingProxy.addNewValue(tagID, scannedVal);
                }

                Thread.Sleep(((TagInput)tags[tagID]).ScanTime * 1000);

            }

        }

        private void processOutputTag(object outputTag)
        {

            String tagID = (string)outputTag;

            if (!tags.ContainsKey(tagID))
            {
                return;
            }

            int scannedVal = 0;

            if (tags[tagID].Driver is RealTimeDriver)
            {
                lock (rt_driver)
                {
                    scannedVal = rt_driver.readValue(tags[tagID].IOAddress);
                }
            }
            else
            {
                lock (sim_driver)
                {
                    scannedVal = sim_driver.readValue(tags[tagID].IOAddress);
                }

            }

            if (trendingProxy != null)
            {
                trendingProxy.addNewValue(tagID, scannedVal);
            }


        }

        public void abortProcessing(string tagID)
        {
            if (!tag_threads.ContainsKey(tagID))
            {
                return;
            }

            tag_threads[tagID].Abort();
            tag_threads.Remove(tagID);
        }


        public void processLoadedTags()
        {
            foreach(string tagID in tagsToSer.AnalogInputs.Keys)
            {
                tags[tagID] = tagsToSer.AnalogInputs[tagID];
                addDrivers(tags[tagID]);
                processTag(tagID);
            }

            foreach (string tagID in tagsToSer.AnalogOutputs.Keys)
            {
                tags[tagID] = tagsToSer.AnalogOutputs[tagID];
                addDrivers(tags[tagID]);
                processTag(tagID);
            }

            foreach (string tagID in tagsToSer.DigitalInputs.Keys)
            {
                tags[tagID] = tagsToSer.DigitalInputs[tagID];
                addDrivers(tags[tagID]);
                processTag(tagID);
            }

            foreach (string tagID in tagsToSer.DigitalOutputs.Keys)
            {
                tags[tagID] = tagsToSer.DigitalOutputs[tagID];
                addDrivers(tags[tagID]);
                processTag(tagID);
            }

        }


        public void addDrivers(Tag tag)
        {
            if(tag.DriverID.Equals("rtd"))
            {
                tags[tag.ID].Driver = rt_driver;

            } else if(tag.DriverID.Equals("sd"))
            {
                tags[tag.ID].Driver = sim_driver;

            } else
            {
                throw new Exception("Unknown driver");
            }
        }

        public void addTagToDatabase(DBTagInfo tag)
        {
            using(DBTagContext context = new DBTagContext())
            {
                context.Tags.Add(tag);
                context.SaveChanges();
            }
        }

    }
}

