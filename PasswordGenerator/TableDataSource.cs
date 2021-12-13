using System.Collections;
using AppKit;
using System.Collections.Generic;
using Foundation;
using System;
using CoreGraphics;

namespace PasswordGenerator
{
    public class TableDataSource : NSTableViewDataSource
    {

        // --- Variables
        public List<TableEntry> Entry = new List<TableEntry>();


        // --- Constructors ---
        public TableDataSource() {}

        public override nint GetRowCount(NSTableView tableView)
        {
            return Entry.Count; //Returns the amount of entry rows
        }

    }
}
