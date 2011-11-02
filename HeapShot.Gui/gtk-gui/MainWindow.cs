
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action open;
	private global::Gtk.Action quit;
	private global::Gtk.Action ForceHeapSnapshotAction;
	private global::Gtk.Action executeAction;
	private global::Gtk.Action stopAction;
	private global::Gtk.Action FileAction;
	private global::Gtk.Action SnapshotAction;
	private global::Gtk.Action LoadHeapSnapshotsAction;
	private global::Gtk.Action ProfileAction;
	private global::Gtk.Action FileAction1;
	private global::Gtk.VBox vbox1;
	private global::Gtk.MenuBar menubar1;
	private global::Gtk.Toolbar toolbar1;
	private global::HeapShot.Gui.Widgets.ObjectMapViewer viewer;
	private global::Gtk.Statusbar statusbar1;
	private global::Gtk.Label statusBarFileName;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.open = new global::Gtk.Action ("open", null, null, "gtk-open");
		this.open.IsImportant = true;
		w1.Add (this.open, null);
		this.quit = new global::Gtk.Action ("quit", null, null, "gtk-quit");
		this.quit.IsImportant = true;
		w1.Add (this.quit, null);
		this.ForceHeapSnapshotAction = new global::Gtk.Action ("ForceHeapSnapshotAction", global::Mono.Unix.Catalog.GetString ("Force Heap Snapshot"), null, null);
		this.ForceHeapSnapshotAction.IsImportant = true;
		this.ForceHeapSnapshotAction.ShortLabel = "Memory snapshot";
		w1.Add (this.ForceHeapSnapshotAction, null);
		this.executeAction = new global::Gtk.Action ("executeAction", global::Mono.Unix.Catalog.GetString ("Run"), null, "gtk-execute");
		this.executeAction.IsImportant = true;
		this.executeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Run");
		w1.Add (this.executeAction, null);
		this.stopAction = new global::Gtk.Action ("stopAction", null, null, "gtk-stop");
		w1.Add (this.stopAction, null);
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.SnapshotAction = new global::Gtk.Action ("SnapshotAction", global::Mono.Unix.Catalog.GetString ("Snapshot"), null, null);
		this.SnapshotAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Snapshot");
		w1.Add (this.SnapshotAction, null);
		this.LoadHeapSnapshotsAction = new global::Gtk.Action ("LoadHeapSnapshotsAction", global::Mono.Unix.Catalog.GetString ("Load Heap Snapshots"), null, null);
		this.LoadHeapSnapshotsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Load Heap Snapshots");
		w1.Add (this.LoadHeapSnapshotsAction, null);
		this.ProfileAction = new global::Gtk.Action ("ProfileAction", global::Mono.Unix.Catalog.GetString ("Profile"), null, null);
		this.ProfileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Profile");
		w1.Add (this.ProfileAction, null);
		this.FileAction1 = new global::Gtk.Action ("FileAction1", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction1, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("Heap Shot");
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='FileAction1' action='FileAction1'><menuitem name='open' action='open'/><separator/><menuitem name='quit' action='quit'/></menu><menu name='ProfileAction' action='ProfileAction'><menuitem name='executeAction' action='executeAction'/><menuitem name='stopAction' action='stopAction'/><separator/><menuitem name='ForceHeapSnapshotAction' action='ForceHeapSnapshotAction'/><menuitem name='LoadHeapSnapshotsAction' action='LoadHeapSnapshotsAction'/></menu><menu/></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.vbox1.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name='toolbar1'><toolitem name='open' action='open'/><separator/><toolitem name='executeAction' action='executeAction'/><toolitem name='stopAction' action='stopAction'/><toolitem name='ForceHeapSnapshotAction' action='ForceHeapSnapshotAction'/></toolbar></ui>");
		this.toolbar1 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbar1")));
		this.toolbar1.Name = "toolbar1";
		this.toolbar1.ShowArrow = false;
		this.toolbar1.ToolbarStyle = ((global::Gtk.ToolbarStyle)(3));
		this.toolbar1.IconSize = ((global::Gtk.IconSize)(3));
		this.vbox1.Add (this.toolbar1);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.toolbar1]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.viewer = new global::HeapShot.Gui.Widgets.ObjectMapViewer ();
		this.viewer.Events = ((global::Gdk.EventMask)(256));
		this.viewer.Name = "viewer";
		this.vbox1.Add (this.viewer);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.viewer]));
		w4.Position = 2;
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbar1 = new global::Gtk.Statusbar ();
		this.statusbar1.Name = "statusbar1";
		this.statusbar1.Spacing = 2;
		// Container child statusbar1.Gtk.Box+BoxChild
		this.statusBarFileName = new global::Gtk.Label ();
		this.statusBarFileName.Name = "statusBarFileName";
		this.statusBarFileName.LabelProp = global::Mono.Unix.Catalog.GetString ("   ");
		this.statusbar1.Add (this.statusBarFileName);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.statusbar1 [this.statusBarFileName]));
		w5.Position = 1;
		w5.Expand = false;
		w5.Fill = false;
		this.vbox1.Add (this.statusbar1);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.statusbar1]));
		w6.Position = 3;
		w6.Expand = false;
		w6.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 711;
		this.DefaultHeight = 466;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.open.Activated += new global::System.EventHandler (this.OnOpenActivated);
		this.quit.Activated += new global::System.EventHandler (this.OnQuitActivated);
		this.ForceHeapSnapshotAction.Activated += new global::System.EventHandler (this.OnMemorySnapshotActivated);
		this.executeAction.Activated += new global::System.EventHandler (this.OnExecuteActionActivated);
		this.stopAction.Activated += new global::System.EventHandler (this.OnStopActionActivated);
	}
}
