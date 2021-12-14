using System;
using AppKit;
using CoreGraphics;
using Foundation;
using System.Collections;
using System.Collections.Generic;

namespace PasswordGenerator
{
    public class TableDelegate : NSTableViewDelegate
    {
        // --- Constant ---
        public const string CellIdentifier = "EntryCell";
        
        // --- Private Vars ---
        private TableDataSource DataSource;

        // --- Constructors ---
        public TableDelegate(TableDataSource dataSource) 
        {
            this.DataSource = dataSource;
        }

        // --- Override Methods ---
        public override NSView GetViewForItem(NSTableView TableDisplay, NSTableColumn tableColumn, nint row)
        {

            NSTextField view = (NSTextField)TableDisplay.MakeView(CellIdentifier, this);

            if(view == null)
            {
                view = new NSTextField();
                view.Identifier = CellIdentifier;  
                view.BackgroundColor = NSColor.Clear;
                view.Bordered = false;
                view.Selectable = true;
                view.Editable = false;
            }
            //Adding To The Table
            string consoleOut = "";
            switch(tableColumn.Title) {
                case "App":
                    consoleOut += (" Adding to App column: " + DataSource.Entry[(int)row].Entry_AppName);
                    view.StringValue = DataSource.Entry[(int)row].Entry_AppName;
                    break;
                
                case "Username":
                    consoleOut += (" Adding to Username column: " + DataSource.Entry[(int)row].Entry_Username);
                    view.StringValue = DataSource.Entry[(int)row].Entry_Username;
                    break;

                case "Password":
                    consoleOut +=(" Adding to Password column: " + DataSource.Entry[(int)row].Entry_Password + "\n");
                    view.StringValue = DataSource.Entry[(int)row].Entry_Password;
                    break;
                
                default:
                    consoleOut += ("Not adding anything");
                    break;
            }
            
            Console.WriteLine(consoleOut);
            return view;
        }
    }
}
