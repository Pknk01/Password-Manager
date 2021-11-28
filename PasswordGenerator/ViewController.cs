using System;

using AppKit;
using Foundation;

namespace PasswordGenerator
{
    public partial class ViewController : NSViewController
    {

        static Random random = new Random();

        //charset
        static string[] Characters = { "A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h", "I", "i", "J", "j", "K", "k", "L", "l", "M", "m", "N", "n", "O", "o", "P", "p", "Q", "q", "R", "r", "S", "s", "T", "t", "U", "u", "V", "v", "W", "w", "X", "x", "Y", "y", "Z", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "/", "?", "|", "{", "}" };
        //charset length
        static int Charlength = Characters.Length;
        static int Passlength;

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

        //Insert Code Here

        //gets desired password length and saves to variable
        partial void PasswordLen_Input(NSTextField sender)
        {
            Passlength = sender.IntValue;
        }

        partial void Generate_Password(NSButton sender)
        {
            if(Passlength == null) { return; }  //Nullcheck

            string output = "";

            for(int i = 0; i <= Passlength; i++)
            {
                int Index = random.Next(1, Charlength); //Selects Random Number
                string CurrentChar = Characters[Index]; //Gets Selected Number
                output += CurrentChar;
            }

            if(true)
            {
                PasswordScroll.Hidden = true;   //Hides scroll on necessity
            }
            else
            {   
                PasswordScroll.Hidden = false;  //Shows scroll on necessity
            }

            CurrPassword_TextBox.Value = output;

        }

    }
}
