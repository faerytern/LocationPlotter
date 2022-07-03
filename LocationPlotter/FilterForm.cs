namespace LocationPlotter
{
    public partial class FilterForm : Form
    {
        public InterestingPlaceOptions options;
        public FilterForm(MapForm parent)
        {
            InitializeComponent();
            Tag = parent;

            dataGridView1.DataSource = parent.table;
            this.options = parent.options;
            propertyGrid1.SelectedObject = options;
        }
        public void RefreshPropertyGrid()
        {
            // How I reset my options is hacky so I have to refresh/reset the propertygrid's selected object to keep references in sync. I really should just reset values to default instead of making a new obj but im lazy
            propertyGrid1.SelectedObject = options;
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (Tag is MapForm parent) parent.RefreshMarkers();
        }
        private void FilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Intercept Closing event
            // Keep it open incase it helps ease loading the datagridview control
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            // But only if the user closes it naturally. Otherwise, clean up properly.
            else e.Cancel = false;
        }
    }
}