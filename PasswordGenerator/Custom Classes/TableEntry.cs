using System;
using AppKit;

namespace PasswordGenerator.CustomClasses
{
    public class TableEntry
    {
        // --- Variables ---
        public string Entry_AppName {get; set;} = "";
        public string Entry_Username {get; set;} = "";
        public string Entry_Password {get; set;} = "";

        // --- Constructor ---
        public TableEntry(string AppName, string Username, string Password)
        {
            this.Entry_AppName = AppName;
            this.Entry_Username = Username;
            this.Entry_Password = Password;
        }

        public TableEntry() {} //Possible alternative method with no inputs, no really sure, leaving here to avoid issues
    }
}
