using System;
using System.IO;
using AppKit;
using Foundation;

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
        static string Password = "";

        // ---- Default Methods ----

        public ViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

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

        // ---- Password Generation ----

        partial void PasswordLen_Input(NSTextField sender)
        {
            Passlength = sender.IntValue; //Saves UI input to variable
        }

        /// <Summary> Creates a random password with the character set of X length <Summary/>
        partial void Generate_Password(NSButton sender)
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
                PasswordScroll.Hidden = false; } //shows
            else { 
                PasswordScroll.Hidden = true; } //hides 
            
            Password = output;  //Saves variable
            CurrPassword_TextBox.Value = output;
        }

        // --- Adding Table Entry ----

        partial void App_Name(NSTextField sender)
        {
            if(sender.StringValue.Equals(null)) { return; } //Nullcheck
            Added_App_Name = sender.StringValue;    //Saves UI input to Variable
        }

        partial void Username_Input(NSTextField sender)
        {
            if(sender.StringValue.Equals(null)) { return; } //Nullcheck
            App_Username = sender.StringValue;      //Saves UI input to Variable
        }

        //Adds entry into password manager
        partial void AddEntry_button(NSButton sender)
        {
            if(Added_App_Name.Equals("") || App_Username.Equals("") || Password.Equals("")) //Empty checking
            {

                //Alert for missinput
                NSAlert Alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Warning,
                    MessageText = "Invalid Input",
                    InformativeText = "Missing one or more inputs to add entry",
                };

                Alert.RunModal();
                return;
            }

            


        }
    }
}
