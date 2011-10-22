// 
// LogFileReader.cs
// 
// Copyright (C) 2006-2011 Novell, Inc. (http://www.novell.com)
// Copyright (C) 2011 Xamarin Inc. (http://www.xamarin.com) 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Gtk;
using HeapShot.Gui;
using HeapShot.Gui.Widgets;
using HeapShot.Reader;
using System.Diagnostics;
using System.Threading;

public partial class MainWindow: Gtk.Window
{
	string lastFolder;
	string outfile;
	Process profProcess;
	ObjectMapReader mapReader;
	
	public MainWindow (string[] args): base ("")
	{
		Build ();
		viewer.Sensitive = false;
		if (args.Length > 0)
			OpenFile (args [0]);
		
		stopAction.Sensitive = false;
		executeAction.Sensitive = true;
		ForceHeapSnapshotAction.Sensitive = false;
	}
	
	protected override void OnDestroyed ()
	{
		ResetFile ();
		if (profProcess != null) {
			try {
				profProcess.Kill ();
			} catch {}
		}
		base.OnDestroyed ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	void ResetFile ()
	{
		if (mapReader != null) {
			mapReader.Dispose ();
			mapReader = null;
		}
		if (outfile != null) {
			try {
				System.IO.File.Delete (outfile);
			} catch {}
			outfile = null;
		}
		viewer.Clear ();
		viewer.Sensitive = false;
	}
	
	void OpenFile (string file)
	{
		
		mapReader = new ObjectMapReader (file);
		mapReader.HeapSnapshotAdded += delegate (object o, HeapShotEventArgs args) {
			Application.Invoke (delegate {
				viewer.AddSnapshot (args.HeapSnapshot);
			});
		};
		viewer.Sensitive = true;
		
		Reload (true);
	}
	
	void Reload (bool showProgress)
	{
		ProgressDialog dialog = null;
		
		if (showProgress) {
			dialog = new ProgressDialog (this, true);
			dialog.Show ();
		}
		
		ThreadPool.QueueUserWorkItem ((v) =>
		{
			ReloadSync (dialog);
		});
	}
	
	void ReloadSync (ProgressDialog dialog)
	{
		try {
			mapReader.Read (dialog);
			Application.Invoke ((sender, e) => 
			{
				ForceHeapSnapshotAction.Sensitive = mapReader.Port > 0;
				if (dialog != null)
					dialog.Destroy ();
			});
		} catch (Exception ex) {
			Console.WriteLine ("Exception while processing log file: {0}", ex);
		}
	}
	
	void ProfileApplication (string file)
	{
		string mono = typeof(int).Assembly.Location;
		for (int n=0; n<4; n++) mono = System.IO.Path.GetDirectoryName (mono);
		mono = System.IO.Path.Combine (mono, "bin","mono");
		ResetFile ();
		outfile = System.IO.Path.GetTempFileName ();
		profProcess = new Process ();
		profProcess.StartInfo.FileName = mono;
		profProcess.StartInfo.Arguments = "--gc=sgen --profile=log:heapshot=ondemand,nocalls,output=-" + outfile + " \"" + file + "\"";
		profProcess.StartInfo.UseShellExecute = false;
		profProcess.EnableRaisingEvents = true;
		profProcess.Exited += delegate {
			Application.Invoke (ProcessExited);
		};
		try {
			profProcess.Start ();
			stopAction.Sensitive = true;
			executeAction.Sensitive = false;
			ForceHeapSnapshotAction.Sensitive = true;
		} catch (Exception ex) {
			Console.WriteLine (ex);
			profProcess = null;
		}
		OpenFile (outfile);
	}
	
	void ProcessExited (object o, EventArgs a)
	{
		profProcess = null;
		stopAction.Sensitive = false;
		executeAction.Sensitive = true;
		ForceHeapSnapshotAction.Sensitive = false;
		Reload (false);
	}

	protected virtual void OnOpenActivated(object sender, System.EventArgs e)
	{
		FileChooserDialog dialog =
			new FileChooserDialog ("Open Object Map File", null, FileChooserAction.Open,
					       Gtk.Stock.Cancel, Gtk.ResponseType.Cancel,
					       Gtk.Stock.Open, Gtk.ResponseType.Ok);
					       
		if (lastFolder != null)
			dialog.SetCurrentFolder (lastFolder);
			
		int response = dialog.Run ();
		try {
			if (response == (int)Gtk.ResponseType.Ok) {
				lastFolder = dialog.CurrentFolder;
				ResetFile ();
				OpenFile (dialog.Filename);
			}
		} finally {
			dialog.Destroy ();
		}
	}

	protected virtual void OnQuitActivated(object sender, System.EventArgs e)
	{
		Application.Quit ();
	}
	
	bool checkingForHeapShot;
	void CheckForHeapShot (int initialCount)
	{
		try {
			Console.WriteLine ("CheckForHeapShot ({0}) checking: {1}", initialCount, checkingForHeapShot);
			ReloadSync (null);
			if (mapReader.HeapShots.Count == initialCount) {
				System.Threading.ThreadPool.QueueUserWorkItem (v =>
				{
					System.Threading.Thread.Sleep (500);
					Application.Invoke ((sender, e) => 
					{
						CheckForHeapShot (initialCount);
					});
				});
			} else {
				checkingForHeapShot = false;
			}
		} catch (Exception ex) {
			var dialog = new MessageDialog (this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, ex.Message);
			try {
				dialog.Run ();
			} finally {
				dialog.Destroy ();
			}
		}
	}
	
	protected virtual void OnMemorySnapshotActivated(object sender, System.EventArgs e)
	{
		try {
			mapReader.ForceSnapshot ();
			if (!checkingForHeapShot) {
				checkingForHeapShot = true;
				CheckForHeapShot (mapReader.HeapShots.Count);
			}
		} catch (Exception ex) {
			var dialog = new MessageDialog (this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, ex.Message);
			try {
				dialog.Run ();
			} finally {
				dialog.Destroy ();
			}
		}
	}

	protected virtual void OnExecuteActionActivated (object sender, System.EventArgs e)
	{
		FileChooserDialog dialog =
			new FileChooserDialog ("Profile Application", null, FileChooserAction.Open,
					       Gtk.Stock.Cancel, Gtk.ResponseType.Cancel,
					       Gtk.Stock.Open, Gtk.ResponseType.Ok);
					       
		if (lastFolder != null)
			dialog.SetCurrentFolder (lastFolder);
			
		int response = dialog.Run ();
		try {
			if (response == (int)Gtk.ResponseType.Ok) {
				lastFolder = dialog.CurrentFolder;
				ProfileApplication (dialog.Filename);
			}
		} finally {
			dialog.Destroy ();
		}
	}
	
	protected virtual void OnStopActionActivated (object sender, System.EventArgs e)
	{
		if (profProcess != null) {
			try {
				profProcess.Kill ();
			} catch {
			}
		}
	}
}

