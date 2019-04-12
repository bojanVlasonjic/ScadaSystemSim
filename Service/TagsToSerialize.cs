using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScadaModel;

namespace Service
{
    public class TagsToSerialize
    {
        public Dictionary<string, AnalogInput> AnalogInputs { get; set; }
        public Dictionary<string, DigitalInput> DigitalInputs { get; set; }
        public Dictionary<string, AnalogOutput> AnalogOutputs { get; set; }
        public Dictionary<string, DigitalOutput> DigitalOutputs { get; set; }

        public TagsToSerialize()
        {
            AnalogInputs = new Dictionary<string, AnalogInput>();
            DigitalInputs = new Dictionary<string, DigitalInput>();
            AnalogOutputs = new Dictionary<string, AnalogOutput>();
            DigitalOutputs = new Dictionary<string, DigitalOutput>();
        }


        public void addTag(Tag tag)
        {
            if(tag is AnalogInput)
            {
                AnalogInputs[tag.ID] = (AnalogInput)tag;
            } else if(tag is AnalogOutput)
            {
                AnalogOutputs[tag.ID] = (AnalogOutput)tag;
            } else if(tag is DigitalInput)
            {
                DigitalInputs[tag.ID] = (DigitalInput)tag;
            } else if (tag is DigitalOutput)
            {
                DigitalOutputs[tag.ID] = (DigitalOutput)tag;
            } else
            {
                throw new Exception("Unknown tag type");
            }
        }

        public void removeTag(Tag tag)
        {
            if (tag is AnalogInput)
            {
                AnalogInputs.Remove(tag.ID);
            }
            else if (tag is AnalogOutput)
            {
                AnalogOutputs.Remove(tag.ID);
            }
            else if (tag is DigitalInput)
            {
                DigitalInputs.Remove(tag.ID);
            }
            else if (tag is DigitalOutput)
            {
                DigitalOutputs.Remove(tag.ID);
            }
            else
            {
                throw new Exception("Unknown tag type");
            }
        }

        
    }
}