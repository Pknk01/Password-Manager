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
		AppKit.NSTextView PasswordDisplay { get; set; }

		[Outlet]
		AppKit.NSTableView TableDisplay { get; set; }

		[Action ("AddEntryButton:")]
		partial void AddEntryButton (AppKit.NSButton sender);

		[Action ("AppNameInput:")]
		partial void AppNameInput (AppKit.NSTextField sender);

		[Action ("GeneratePasswordButton:")]
		partial void GeneratePasswordButton (AppKit.NSButton sender);

		[Action ("PasswordLengthInput:")]
		partial void PasswordLengthInput (AppKit.NSTextField sender);

		[Action ("UsernameInput:")]
		partial void UsernameInput (AppKit.NSTextField sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PasswordDisplay != null) {
				PasswordDisplay.Dispose ();
				PasswordDisplay = null;
			}

			if (TableDisplay != null) {
				TableDisplay.Dispose ();
				TableDisplay = null;
			}
		}
	}
}
