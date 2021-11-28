using System.Collections;
using AppKit;
using System.Collections.Generic;

namespace PasswordGenerator.CustomClasses
{
    public class TableDataSource : NSTableViewDataSource
    {

        // --- Variables
        public List<TableEntry> Entry = new List<TableEntry>();




        public TableDataSource() {}
    }
}
