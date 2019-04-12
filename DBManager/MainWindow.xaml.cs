using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScadaModel;
using System.Runtime.Serialization;

namespace DBManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        static ServiceReference1.I_DB_ManagerClient service = new ServiceReference1.I_DB_ManagerClient();

        static Boolean editing = false;
        static List<Alarm> alarmsOfEditedTag = null;

        public MainWindow()
        {
            InitializeComponent();

            allTagsTextBox.IsReadOnly = true;
            allAlarmsTextBox.IsReadOnly = true;

            displayAllTags();
        }

        /*/******************************* Tag button events /********************************/

        private void addTagBtn_Click(object sender, RoutedEventArgs e)
        {

            if(isTagFieldEmpty())
            {
                MessageBox.Show("Empty field detected");
                return;
            }

            //Skeniranje veličine sa real-time drajvera ne sme biti podešeno na manual
            if (tagDriverComboBox.Text.ToLower().Contains("real"))
            {
                if(tagReadDataTypeComboBox.Text.ToLower().Equals("manual") &&
                    tagTypeComboBox.Text.ToLower().Contains("input"))
                {
                    MessageBox.Show("Real time driver does not support manual");
                    return;
                }

                 //niti se sme postavljati inicijalna vrednost te velicine
                 try {
                    if(tagTypeComboBox.Text.ToLower().Contains("output"))
                    {
                        if (Int32.Parse(tagInitValueTextBox.Text) != 0)
                        {
                            MessageBox.Show("Real time driver doesn't require an init value. Please put 0 there");
                            return;
                        }
                    }
                    
                }
                catch(FormatException)
                {
                    MessageBox.Show("Tag init value must be a number");
                    return;
                }
                 
            }

            bool tagAdded = false;
            tagAdded = addTag(editing, alarmsOfEditedTag);

            if (editing)
            {
                editing = false;
            }

            tagTypeComboBox.IsEnabled = true;

            if(tagAdded)
            {
                clearAllInputs(TagsGrid);
            }
            
        }

        private void removeTagBtn_Click(object sender, RoutedEventArgs e)
        {
            String tagName = tagNameTextBox.Text;

            if (tagName.Trim() == "")
            {
                MessageBox.Show("Tag name required");
                return;
            }

            if (!service.removeTag(tagName))
            {
                MessageBox.Show($"Tag with tag name {tagName} does not exist");
                return;
            }

            displayAllTags();
        }

        private void editTagBtn_Click(object sender, RoutedEventArgs e)
        {
            String tagName = tagNameTextBox.Text;

            if (tagName.Trim() == "")
            {
                MessageBox.Show("Tag name required");
                return;
            }

            Tag tag = service.getTagForEditing(tagName);

            if (tag == null)
            {
                MessageBox.Show($"Tag with tag name {tagName} does not exist");
                return;
            }

            tagTypeComboBox.IsEnabled = false;
            editing = true;

            //utvrdi vrstu taga i postavi odgovarajuce podatke
            if (tag is AnalogInput)
            {
                tagTypeComboBox.Text = "Analog input";
                analogInputSelected();
                alarmsOfEditedTag = ((AnalogInput)tag).Alarms;
                displayAnalogInputData((AnalogInput)tag);

            } else if (tag is DigitalInput)
            {
                tagTypeComboBox.Text = "Digital input";
                digitalInputSelected();
                alarmsOfEditedTag = ((DigitalInput)tag).Alarms;
                displayDigitalInputData((DigitalInput)tag);

            } else if (tag is AnalogOutput)
            {
                tagTypeComboBox.Text = "Analog output";
                analogOutputSelected();
                displayAnalogOutputData((AnalogOutput)tag);

            } else if (tag is DigitalOutput)
            {
                tagTypeComboBox.Text = "Digital output";
                digitalOutputSelected();
                displayDigitalOutputData((DigitalOutput)tag);

            } else
            {
                throw (new Exception("Unknown tag type"));
            }

        }

        /*/******************************* Tag comboBox events /********************************/

        private void tagTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(tagTypeComboBox.SelectedItem == null)
            {
                return;
            }

            var tokens = tagTypeComboBox.SelectedItem.ToString().Split(':');

            switch (tokens[1].Trim())
            {
                case "Digital input":
                    digitalInputSelected();
                    break;

                case "Digital output":
                    digitalOutputSelected();
                    break;

                case "Analog input":
                    analogInputSelected();
                    break;

                case "Analog output":
                    analogOutputSelected();
                    break;

            }
        }

        private void tagDriverComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(tagDriverComboBox.SelectedItem == null)
            {
                return;
            }

            var tokens = tagDriverComboBox.SelectedItem.ToString().Split(':');
            List<string> availAddr = new List<string>(); //data delivered by server

            tagAddressComboBox.Items.Clear();

            switch (tokens[1].Trim())
            {
                case "Real time driver":
                    availAddr = service.getRTDAddresses().ToList();

                    foreach (string addr in availAddr)
                    {
                        tagAddressComboBox.Items.Add(addr);
                    }

                    break;

                case "Simulation driver":
                    availAddr = service.getSDAvailableAddr().ToList();

                    foreach (string addr in availAddr)
                    {
                        tagAddressComboBox.Items.Add(addr);
                    }

                    break;
            }

        }

        private void tagAddressComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tagScanComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tagReadDataTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /******************************* Alarm button events /********************************/

        private void addAlarmBtn_Click(object sender, RoutedEventArgs e)
        {

            if(isAlarmFieldEmpty())
            {
                MessageBox.Show("Empty field detected");
                return;
            }

            int lowLimit = 0;
            int highLimit = 0;

           try
            {
                lowLimit = Int32.Parse(alarmLowLimitTxtBox.Text);
                highLimit = Int32.Parse(alarmHighLimitTxtBox.Text);
            } catch(FormatException)
            {
                MessageBox.Show("Please enter a number in low and high limit");
                return;
            }
            

            //checking lowLimit and high limit range
            if (lowLimit < Constants.RTU_low_limit || lowLimit > Constants.RTU_high_limit-5
                || highLimit > Constants.RTU_high_limit || highLimit < Constants.RTU_low_limit + 5)
            {
                MessageBox.Show($"Low limit must be > {Constants.RTU_low_limit}; High limit must be < {Constants.RTU_high_limit}");
                return;
            }

            Alarm alarm = new Alarm(alarmIDTextBox.Text, alarmTagNameTxtBox.Text, lowLimit, highLimit);

            if (!service.addAlarm(alarm))
            {
                MessageBox.Show($"Can't add alarm for tag '{alarmTagNameTxtBox.Text}'\n" +
                     "Tag either doesn't exists, or it isn't an input tag");
                return;
            }

            //update all alarms list
            displayAllAlarms(alarm.TagID, false);
            clearAllInputs(AlarmsGird);
        }

        private void removeAlarmBtn_Click(object sender, RoutedEventArgs e)
        {

            if(!service.removeAlarm(alarmTagNameTxtBox.Text, alarmIDTextBox.Text))
            {
                MessageBox.Show("Removing not successful. Please check the alarm and tag name once again");
                return;
            }

            displayAllAlarms(alarmTagNameTxtBox.Text, false);
        }




        /******************************* AUXILIARY METHODS ******************************/

        public bool isAlarmFieldEmpty()
        {
            if(alarmIDTextBox.Text.Trim() == ""
                || alarmLowLimitTxtBox.Text.Trim() == ""
                || alarmHighLimitTxtBox.Text.Trim() == ""
                || alarmTagNameTxtBox.Text.Trim() == "")
            {
                return true;
            }

            return false;
        }

        public bool isTagFieldEmpty()
        {

            if (tagTypeComboBox.Text.Trim() == "")
            {
                return true;
            }

            if (isBasicTagFieldsEmpty() == true)
            {
                return true;
            }

            var tokens = tagTypeComboBox.SelectedItem.ToString().Split(':');

            switch (tokens[1].Trim())
            {
                case "Digital input":
                    return isDigitalInputFieldEmpty();

                case "Digital output":
                    return isDigitalOutputFieldEmpty();

                case "Analog input":
                    return isAnalogInputFieldEmpty();

                case "Analog output":
                    return isAnalogOutputFieldEmpty();
            }

            return true;
        }

        public bool addTag(bool editing, List<Alarm> alarms)
        {
            var tokens = tagTypeComboBox.SelectedItem.ToString().Split(':');

            string id = tagNameTextBox.Text;
            string descr = tagDescrTextBox.Text;
            string driver = tagDriverComboBox.SelectedItem.ToString();
            string address = tagAddressComboBox.SelectedItem.ToString();

            scanType scan = scanType.ON;
            readType read = readType.AUTO;

            if(tagScanComboBox.SelectedItem != null)
            {
                if (tagScanComboBox.Text.ToLower().Equals("on"))
                {
                    scan = scanType.ON;
                }
                else
                {
                    scan = scanType.OFF;
                }
            }
            
            if(tagReadDataTypeComboBox.SelectedItem != null)
            {
                if (tagReadDataTypeComboBox.Text.ToLower().Equals("auto"))
                {
                    read = readType.AUTO;
                }
                else
                {
                    read = readType.MANUAL;
                }
            }
            
            var driverTokens = tagDriverComboBox.SelectedItem.ToString().Split(':');

            bool response = true;
            switch (tokens[1].Trim())
            {
                case "Digital input":

                    DigitalInput di;
                    try
                    {
                        di = new DigitalInput(id, descr, null, address,
                        Int32.Parse(tagScanTimeTextBox.Text), scan, read);
                    } catch(FormatException)
                    {
                        MessageBox.Show("Please enter a number on tag scan time");
                        return false;
                    }
                    
                    if(di.ScanTime < 1)
                    {
                        MessageBox.Show("Scan time must be > 1");
                        return false;
                    }

                    if (editing)
                    {
                        di.Alarms = alarms;
                    }

                    response = service.addTag(di, driverTokens[1].Trim());

                    break;

                case "Digital output":

                    DigitalOutput dig_out;

                    try
                    {
                        dig_out = new DigitalOutput(id, descr, null, address,
                        Int32.Parse(tagInitValueTextBox.Text));
                    } catch(FormatException)
                    {
                        MessageBox.Show("Please enter a number in init value");
                        return false;
                    }
                    
                    response = service.addTag(dig_out, driverTokens[1].Trim());

                    break;

                case "Analog input":

                    AnalogInput analog_input;

                    try
                    {
                        analog_input = new AnalogInput(id, descr, null, address, Int32.Parse(tagScanTimeTextBox.Text),
                        scan, read, Int32.Parse(tagLowLimitTextBox.Text), Int32.Parse(tagHighLimitTextBox.Text), tagDescrTextBox.Text);
                    } catch(FormatException)
                    {
                        MessageBox.Show("Please enter a number on scan time, low and high limits");
                        return false;
                    }

                    if (analog_input.ScanTime < 1)
                    {
                        MessageBox.Show("Scan time must be > 1");
                        return false;
                    }

                    if (editing)
                    {
                        analog_input.Alarms = alarms;
                    }

                    response = service.addTag(analog_input, driverTokens[1].Trim());
                    break;

                case "Analog output":

                    AnalogOutput analog_output;

                    try
                    {
                        analog_output = new AnalogOutput(id, descr, null, address, Int32.Parse(tagInitValueTextBox.Text),
                        Int32.Parse(tagLowLimitTextBox.Text), Int32.Parse(tagHighLimitTextBox.Text), tagDescrTextBox.Text);
                    } catch(FormatException)
                    {
                        MessageBox.Show("Please enter a number on scan time, low and high limits");
                        return false;
                    }
                    
                    response = service.addTag(analog_output, driverTokens[1].Trim());
                    break;
            }

            if (!response)
            {
                MessageBox.Show($"Tag name {tagNameTextBox.Text} already exists");
                return false;
            }

            displayAllTags();
            return true;
        }

        public void displayBasicTagData(Tag tag)
        {
            tagNameTextBox.Text = tag.ID;
            tagDescrTextBox.Text = tag.Description;

            if (tag.Driver is RealTimeDriver)
            {
                tagDriverComboBox.Text = "Real time driver";
            } else
            {
                tagDriverComboBox.Text = "Simulation driver";
            }

            tagAddressComboBox.Text = tag.IOAddress;
        }

        public bool isBasicTagFieldsEmpty()
        {
            if (tagNameTextBox.Text.Trim() == ""
                || tagDescrTextBox.Text.Trim() == ""
                || tagDriverComboBox.Text.Trim() == ""
                || tagAddressComboBox.Text.Trim() == "")
            {
                return true;
            }

            return false;
        }

        public void displayAnalogInputData(AnalogInput tag)
        {

            displayBasicTagData(tag);

            tagScanTimeTextBox.Text = tag.ScanTime.ToString();
            tagScanComboBox.Text = tag.Scan.ToString();
            tagReadDataTypeComboBox.Text = tag.Read.ToString();
            tagLowLimitTextBox.Text = tag.LowLimits.ToString();
            tagHighLimitTextBox.Text = tag.HighLimits.ToString();
            tagUnitsTextBox.Text = tag.Units.ToString();
        }

        public bool isAnalogInputFieldEmpty()
        {
            if (tagScanTimeTextBox.Text.Trim() == ""
                || tagScanComboBox.Text.Trim() == ""
                || tagReadDataTypeComboBox.Text.Trim() == ""
                || tagLowLimitTextBox.Text.Trim() == ""
                || tagHighLimitTextBox.Text.Trim() == ""
                || tagUnitsTextBox.Text.Trim() == "")
            {
                return true;
            }

            return false;
        }

        public void displayDigitalInputData(DigitalInput tag)
        {
            displayBasicTagData(tag);

            tagScanTimeTextBox.Text = tag.ScanTime.ToString();
            tagScanComboBox.Text = tag.Scan.ToString();
            tagReadDataTypeComboBox.Text = tag.Read.ToString();
        }

        public bool isDigitalInputFieldEmpty()
        {
            if(tagScanTimeTextBox.Text.Trim() == ""
                || tagScanComboBox.Text.Trim() == ""
                || tagReadDataTypeComboBox.Text.Trim() == "")
            {
                return true;
            }

            return false;
        }

        public void displayAnalogOutputData(AnalogOutput tag)
        {
            displayBasicTagData(tag);

            tagInitValueTextBox.Text = tag.InitialValue.ToString();
            tagLowLimitTextBox.Text = tag.LowLimits.ToString();
            tagHighLimitTextBox.Text = tag.HighLimits.ToString();
            tagUnitsTextBox.Text = tag.Units.ToString();

        }

        public bool isAnalogOutputFieldEmpty()
        {
            if (tagInitValueTextBox.Text.Trim() == ""
                || tagLowLimitTextBox.Text.Trim() == ""
                || tagHighLimitTextBox.Text.Trim() == ""
                || tagUnitsTextBox.Text.Trim() == "")
            {
                return true;
            }

            return false;
        }
    

        public void displayDigitalOutputData(DigitalOutput tag)
        {

            displayBasicTagData(tag);
            tagInitValueTextBox.Text = tag.InitialValue.ToString();
       
        }

        public bool isDigitalOutputFieldEmpty()
        {
            if(tagInitValueTextBox.Text.Trim() == "")
            {
                return true;
            }

            return false;
        }

        public void digitalInputSelected()
        {
            tagScanTimeTextBox.IsEnabled = true;
            tagInitValueTextBox.IsEnabled = false;
            tagScanComboBox.IsEnabled = true;
            tagReadDataTypeComboBox.IsEnabled = true;
            tagLowLimitTextBox.IsEnabled = false;
            tagHighLimitTextBox.IsEnabled = false;
            tagUnitsTextBox.IsEnabled = false;
        }

        public void digitalOutputSelected()
        {
            tagScanTimeTextBox.IsEnabled = false;
            tagInitValueTextBox.IsEnabled = true;
            tagScanComboBox.IsEnabled = false;
            tagReadDataTypeComboBox.IsEnabled = false;
            tagLowLimitTextBox.IsEnabled = false;
            tagHighLimitTextBox.IsEnabled = false;
            tagUnitsTextBox.IsEnabled = false;

        }

        public void analogInputSelected()
        {
            tagScanTimeTextBox.IsEnabled = true;
            tagInitValueTextBox.IsEnabled = false;
            tagScanComboBox.IsEnabled = true;
            tagReadDataTypeComboBox.IsEnabled = true;
            tagLowLimitTextBox.IsEnabled = true;
            tagHighLimitTextBox.IsEnabled = true;
            tagUnitsTextBox.IsEnabled = true;
        }

        public void analogOutputSelected()
        {
            tagScanTimeTextBox.IsEnabled = false;
            tagInitValueTextBox.IsEnabled = true;
            tagScanComboBox.IsEnabled = false;
            tagReadDataTypeComboBox.IsEnabled = false;
            tagLowLimitTextBox.IsEnabled = true;
            tagHighLimitTextBox.IsEnabled = true;
            tagUnitsTextBox.IsEnabled = true;
        }

        public void displayAllTags()
        {
            allTagsTextBox.Clear();

            var tags = service.getTags();

            if(tags == null)
            {
                allTagsTextBox.AppendText("No tags at the moment");
                return;
            }

            if (tags.Count() == 0)
            {
                allTagsTextBox.AppendText("No tags at the moment");
                return;
            }

            foreach (Tag t in tags)
            {
                allTagsTextBox.AppendText(t.displayTagData());
                allTagsTextBox.AppendText("\n");
            }
        }


        public void displayAllAlarms(string tagID, bool viewAlarmsClicked)
        {
            var alarms = service.getAlarms(tagID);

            if(alarms == null)
            {
                if(viewAlarmsClicked)
                {
                    allAlarmsTextBox.Text = "";
                    allAlarmsTextBox.AppendText("No alarms at the moment");
                } else
                {
                    MessageBox.Show($"Can't add alarm for tag '{tagID}'\n" +
                    "Tag either doesn't exists, or it isn't an input tag");
                }
                
                return;
            }

            allAlarmsTextBox.Text = "";

            if (alarms.Count() == 0)
            {
                allAlarmsTextBox.AppendText($"No alarms for tag: {tagID} have been added \n");
                return;
            }

            foreach(Alarm a in alarms)
            {
                allAlarmsTextBox.AppendText(a.displayAlarmData());
            }
        }

        private void viewAlarmsBtn_Click(object sender, RoutedEventArgs e)
        {

            displayAllAlarms(alarmTagNameTxtBox.Text, true);
        }


        private void clearAllInputs(Grid gridName)
        {
            foreach(UIElement element in gridName.Children)
            {
                if (element is TextBox)
                {
                    TextBox textbox = element as TextBox;
                    if (textbox != null)
                    {
                        textbox.Text = String.Empty;
                    }
                }

                if (element is ComboBox)
                {
                    ComboBox comboBox = element as ComboBox;
                    if (comboBox != null)
                    {
                        comboBox.Text = String.Empty;
                    }
                }
                
            }
        }

        private void sendManualBtn_Click(object sender, RoutedEventArgs e)
        {

            if(manualValueTxtBox.Text.Trim() == "")
            {
                MessageBox.Show("Empty field detected");
                return;
            }

            int valToSend = 0;

            try
            {
                valToSend = Int32.Parse(manualValueTxtBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a number for the manual value");
                return;
            }

            if(!service.sendManualValue(manualTagIDTxtBox.Text, valToSend))
            {
                MessageBox.Show($"Can't send manual value for tag {manualTagIDTxtBox.Text}");
                return;
            }
             
        }
    }
}
