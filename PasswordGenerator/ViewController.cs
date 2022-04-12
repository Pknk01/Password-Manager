using System;
using System.IO;
using AppKit;
using Foundation;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Xml.Linq;

namespace PasswordGenerator
{
    public partial class ViewController : NSViewController
    {

        // ---- Variables ----

        static Random random = new Random();

        static string[] charset = { "A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h", "I", "i", "J", "j", "K", "k", "L", "l", "M", "m", "N", "n", "O", "o", "P", "p", "Q", "q", "R", "r", "S", "s", "T", "t", "U", "u", "V", "v", "W", "w", "X", "x", "Y", "y", "Z", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "/", "?", "|", "{", "}" };
        static int Charlength = charset.Length;
        static int Passlength;
        static int scrollthreshold = 90;

        static string Added_App_Name = "";
        static string App_Username = "";
        static string CurrPassword = "";

        // ---- Serialization variables ----

        private string storagefilename = "/SerializedAppData.xml";
        List<TableEntry> serializationlist = new List<TableEntry>();


        TableDataSource DataSource = new TableDataSource();


        // ---- Default Methods ----

        public ViewController(IntPtr handle) : base(handle)
        {
        }    
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PasswordReading(); //Call file Read method

            // Do any additional setup after loading the view.

        }
        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
                        
            TableDisplay.DataSource = DataSource;
            TableDisplay.Delegate = new TableDelegate(DataSource); 
        }

        // ---- Password Generation ----

        partial void PasswordLengthInput(NSTextField sender)
        {
            Passlength = sender.IntValue; //Saves UI input to variable
        }

        /// <Summary> Creates a random password with the character set of X length <Summary/>
        partial void GeneratePasswordButton(NSButton sender)
        {
            if(Passlength.Equals(null) || Passlength <= 0) //Nullcheck and if password length <= 0
            {
                //Creates Alert
                NSAlert Alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Warning,
                    MessageText = "Invalid Input",
                    InformativeText = "Password length invalid, must be a positive number",
                };
                
                Alert.RunModal();   //Displays Alert

                return;        
            }     

            string output = "";                         //Default output = null

            for(int i = 0; i < Passlength; i++)
            {
                int Index = random.Next(1, Charlength); //Selects Random Number
                string CurrentChar = charset[Index];    //Gets Selected Number
                output += CurrentChar;                  //Adds selected character to output string
            }

            //Hides scroll bar on text box if necessary cuz of password length

            if(output.Length >= scrollthreshold) { 
                ScrollerWheel.Hidden = false; } //shows
            else { 
                ScrollerWheel.Hidden = true; } //hides 
            
            CurrPassword = output;  //Saves variable
            
            PasswordDisplay.Value = output;
        }

        // --- Adding Table Entry ----

        partial void AppNameInput(NSTextField sender)
        {
            Added_App_Name = sender.StringValue;    //Saves UI input to Variable
        }

        partial void UsernameInput(NSTextField sender)
        {
            App_Username = sender.StringValue;      //Saves UI input to Variable
        }

        //Adds entry into password manager
        partial void AddEntryButton(NSButton sender)
        {
            if(Added_App_Name.Equals("") || App_Username.Equals("") || CurrPassword.Equals("")) //Empty checking
            {
                //Alert for missinput
                NSAlert ErrAlert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Warning,
                    MessageText = "Invalid Input",
                    InformativeText = "Missing inputs or duplicate input",
                };

                ErrAlert.RunModal();   //Displays Alert in window
                return;
            }

            //Creates Entry with information previously present
            TableEntry Entry = new TableEntry(Added_App_Name, App_Username, CurrPassword);
            DataSource.Entry.Add(Entry);
            serializationlist.Add(Entry);
            TableDisplay.ReloadData();  //Refresh Table
            PasswordStorage(); //Storing Current Password

            /// --- clearing variables ---
            Added_App_Name = "";
            App_Username = "";
            CurrPassword = "";

        }

        #region Password encryption Region

        private void PasswordStorage()
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/how-to-write-object-data-to-an-xml-file

            try
            {
                TableEntry[] Entries = serializationlist.ToArray(); 

                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(Entries.GetType()); //Creates serializer object

                var storage_path = System.IO.Directory.GetCurrentDirectory() + storagefilename; //targets application storage path as string

                //Opens file if it exists, creates it if it doesn't
                System.IO.FileStream Stream = File.Open(storage_path, FileMode.OpenOrCreate) ;
                Console.WriteLine("Writing Filepath = " + storage_path); 

                writer.Serialize(Stream, Entries); //Writes data to file
                Stream.Close(); //Closes file
            }
            catch(Exception error)
            {
                Console.WriteLine("Writing IO error: " + error);
            }
        }

        private void PasswordReading()
        {            
            try
            {
                string storage_path = System.IO.Directory.GetCurrentDirectory() + storagefilename; //Gets target storage path as string
                var entries = XDocument.Load(storage_path).Root.Elements().Select(y => y.Elements().ToDictionary(x => x.Name, x => x.Value)).ToArray(); //the hell???
                Console.WriteLine("Reading Filepath = " + storage_path);

                foreach(var entry in entries)
                {
                    TableEntry toadd = new TableEntry(entry["Entry_AppName"], entry["Entry_Username"], entry["Entry_Password"]);
                    DataSource.Entry.Add(toadd);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Reading IO error:" + error);
            }
        }
        #endregion
    }
}
