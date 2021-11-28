// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PasswordGenerator
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTableColumn AppNameColumn { get; set; }

		[Outlet]
		AppKit.NSTextView CurrPassword_TextBox { get; set; }

		[Outlet]
		AppKit.NSTableColumn PasswordColumn { get; set; }

		[Outlet]
		AppKit.NSScroller PasswordScroll { get; set; }

		[Outlet]
		AppKit.NSScrollView Table { get; set; }

		[Outlet]
		AppKit.NSScrollView UsernameColumn { get; set; }

		[Action ("AddEntry_button:")]
		partial void AddEntry_button (AppKit.NSButton sender);

		[Action ("App_Name:")]
		partial void App_Name (AppKit.NSTextField sender);

		[Action ("Generate_Password:")]
		partial void Generate_Password (AppKit.NSButton sender);

		[Action ("PasswordLen_Input:")]
		partial void PasswordLen_Input (AppKit.NSTextField sender);

		[Action ("Press:")]
		partial void Press (AppKit.NSButton sender);

		[Action ("Username_Input:")]
		partial void Username_Input (AppKit.NSTextField sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CurrPassword_TextBox != null) {
				CurrPassword_TextBox.Dispose ();
				CurrPassword_TextBox = null;
			}

			if (PasswordScroll != null) {
				PasswordScroll.Dispose ();
				PasswordScroll = null;
			}

			if (Table != null) {
				Table.Dispose ();
				Table = null;
			}

			if (AppNameColumn != null) {
				AppNameColumn.Dispose ();
				AppNameColumn = null;
			}

			if (UsernameColumn != null) {
				UsernameColumn.Dispose ();
				UsernameColumn = null;
			}

			if (PasswordColumn != null) {
				PasswordColumn.Dispose ();
				PasswordColumn = null;
			}
		}
	}
}
