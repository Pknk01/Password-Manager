using System;
using System.IO;
using AppKit;
using Foundation;
using System.Security.Cryptography;

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

        #region Encryption Key

            byte[] Key = 
            {
                0x49, 0x20, 0x61, 0x6d, 0x20, 0x61, 0x20, 0x6e, 0x65, 0x72, 0x64
            };

        #endregion

        TableDataSource DataSource = new TableDataSource();

        // ---- Default Methods ----

        public ViewController(IntPtr handle) : base(handle)
        {
        }    
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.

            PasswordReading(); //Call file Read method
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
            DataSource.Entry.Add(new TableEntry(Added_App_Name, App_Username, CurrPassword));
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
            //https://docs.microsoft.com/en-us/dotnet/standard/security/encrypting-data

            try
            {            
                using(FileStream stream = new FileStream("PasswordDump.txt", FileMode.OpenOrCreate)) //Creates or opens file storing documents
                {
                    using (Aes Instance = Aes.Create()) //creates AES encryption instance
                    {
                        Instance.Key = Key; //Sets global key as instance

                        byte[] iv = Instance.IV; //encryption initialization vector  (starting conditions)
                        stream.Write(iv, 0, iv.Length); //no idea what this does ._. https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream.write?view=net-6.0

                        using(CryptoStream cryptostream = new CryptoStream(stream, Instance.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using(StreamWriter encryptwriter = new StreamWriter(cryptostream))
                            {

                                //Todo Dump all passwords through here

                                encryptwriter.WriteLine(CurrPassword); //Writes into file i think?
                            }
                        } 
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("encryption storage error: " + err);
            }
        }

        private void PasswordReading()
        {
            try
            {
                using(FileStream stream = new FileStream("PasswordDump.txt", FileMode.OpenOrCreate))
                {
                    using(Aes Instance = Aes.Create())  //AES encryption intance
                    {
                        byte[] iv = Instance.IV; //Encryption inizalization vector (starting conditions)
                        int numBytesToRead = Instance.IV.Length;
                        int numBytesRead = 0;

                        while (numBytesToRead > 0)
                        {
                            int n = stream.Read(iv, numBytesRead, numBytesToRead);
                            if (n == 0) { break; }

                            numBytesRead += n;
                            numBytesToRead -= n;
                        }

                        using (CryptoStream cryptoStream = new CryptoStream(stream, Instance.CreateDecryptor(Key, iv), CryptoStreamMode.Read))
                        {
                            using(StreamReader decryptReader = new StreamReader(cryptoStream))
                            {
                                //Todo Get all passwords from here

                                string DecryptedMessage = decryptReader.ReadToEnd();
                                Console.WriteLine("Decrypted message: " + DecryptedMessage);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Encryption reading failed: " + err);
            }
        }
        #endregion
    }
}
