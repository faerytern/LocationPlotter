namespace LocationPlotter
{
    partial class MapForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
            this.myMap = new GMap.NET.WindowsForms.GMapControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyLocationToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterMarkersOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetToDefaultViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToStandardZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshMarkersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // myMap
            // 
            this.myMap.Bearing = 0F;
            this.myMap.CanDragMap = true;
            this.myMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.myMap.GrayScaleMode = false;
            this.myMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.myMap.LevelsKeepInMemory = 5;
            this.myMap.Location = new System.Drawing.Point(0, 0);
            this.myMap.MarkersEnabled = true;
            this.myMap.MaxZoom = 22;
            this.myMap.MinZoom = 0;
            this.myMap.MouseWheelZoomEnabled = true;
            this.myMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.myMap.Name = "myMap";
            this.myMap.NegativeMode = false;
            this.myMap.PolygonsEnabled = true;
            this.myMap.RetryLoadTile = 0;
            this.myMap.RoutesEnabled = true;
            this.myMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.myMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.myMap.ShowTileGridLines = false;
            this.myMap.Size = new System.Drawing.Size(784, 761);
            this.myMap.TabIndex = 0;
            this.myMap.Zoom = 0D;
            this.myMap.Load += new System.EventHandler(this.myMap_Load);
            this.myMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.myMap_MouseDoubleClick);
            this.myMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MyMap_MouseMove);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyLocationToClipboardToolStripMenuItem,
            this.filterMarkersOnToolStripMenuItem,
            this.quickCommandsToolStripMenuItem,
            this.refreshMarkersToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(221, 136);
            // 
            // copyLocationToClipboardToolStripMenuItem
            // 
            this.copyLocationToClipboardToolStripMenuItem.Name = "copyLocationToClipboardToolStripMenuItem";
            this.copyLocationToClipboardToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.copyLocationToClipboardToolStripMenuItem.Text = "Copy Location to Clipboard";
            this.copyLocationToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyLocationToClipboardToolStripMenuItem_Click);
            // 
            // filterMarkersOnToolStripMenuItem
            // 
            this.filterMarkersOnToolStripMenuItem.Name = "filterMarkersOnToolStripMenuItem";
            this.filterMarkersOnToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.filterMarkersOnToolStripMenuItem.Text = "Filter Markers";
            this.filterMarkersOnToolStripMenuItem.Click += new System.EventHandler(this.filterMarkersOnToolStripMenuItem_Click);
            // 
            // quickCommandsToolStripMenuItem
            // 
            this.quickCommandsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetSettingsToolStripMenuItem,
            this.ResetToDefaultViewToolStripMenuItem,
            this.resetToStandardZoomToolStripMenuItem});
            this.quickCommandsToolStripMenuItem.Name = "quickCommandsToolStripMenuItem";
            this.quickCommandsToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.quickCommandsToolStripMenuItem.Text = "Quick Commands";
            // 
            // resetSettingsToolStripMenuItem
            // 
            this.resetSettingsToolStripMenuItem.Name = "resetSettingsToolStripMenuItem";
            this.resetSettingsToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.resetSettingsToolStripMenuItem.Text = "Reset Settings";
            this.resetSettingsToolStripMenuItem.Click += new System.EventHandler(this.resetSettingsToolStripMenuItem_Click);
            // 
            // ResetToDefaultViewToolStripMenuItem
            // 
            this.ResetToDefaultViewToolStripMenuItem.Name = "ResetToDefaultViewToolStripMenuItem";
            this.ResetToDefaultViewToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.ResetToDefaultViewToolStripMenuItem.Text = "Reset to Default View";
            this.ResetToDefaultViewToolStripMenuItem.Click += new System.EventHandler(this.ResetToDefaultViewToolStripMenuItem_Click);
            // 
            // resetToStandardZoomToolStripMenuItem
            // 
            this.resetToStandardZoomToolStripMenuItem.Name = "resetToStandardZoomToolStripMenuItem";
            this.resetToStandardZoomToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.resetToStandardZoomToolStripMenuItem.Text = "Reset to standard zoom";
            this.resetToStandardZoomToolStripMenuItem.Click += new System.EventHandler(this.resetToStandardZoomToolStripMenuItem_Click);
            // 
            // refreshMarkersToolStripMenuItem
            // 
            this.refreshMarkersToolStripMenuItem.Name = "refreshMarkersToolStripMenuItem";
            this.refreshMarkersToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.refreshMarkersToolStripMenuItem.Text = "Refresh Markers";
            this.refreshMarkersToolStripMenuItem.Click += new System.EventHandler(this.refreshMarkersToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.myMap);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapForm";
            this.Text = "Map Plotter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl myMap;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem refreshMarkersToolStripMenuItem;
        private ToolStripMenuItem filterMarkersOnToolStripMenuItem;
        private ToolStripMenuItem quickCommandsToolStripMenuItem;
        private ToolStripMenuItem resetSettingsToolStripMenuItem;
        private ToolStripMenuItem ResetToDefaultViewToolStripMenuItem;
        private ToolStripMenuItem resetToStandardZoomToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem copyLocationToClipboardToolStripMenuItem;
    }
}