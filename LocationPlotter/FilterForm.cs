using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocationPlotter
{
    public partial class FilterForm : Form
    {
        private DataTable table = new DataTable();
        public FilterForm(List<InterestingPlace> places, Form1 parent, InterestingPlaceOptions options)
        {
            InitializeComponent();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("UserID", typeof(string));
            table.Columns.Add("Latitude", typeof(double));
            table.Columns.Add("Longitude", typeof(double));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Created_At", typeof(DateTime));
            table.Columns.Add("Updated_At", typeof(DateTime));

            RefreshTable();


            dataGridView1.DataSource = table;
            Tag = parent;
            propertyGrid1.SelectedObject = options;
        }
        private void RefreshTable()
        {
            table.Rows.Clear();
            foreach (var p in (Tag as Form1).CustomPlaces)
            {
                table.Rows.Add(p.ID, p.UserID, p.Latitude, p.Longitude, p.Description, p.Created_At, p.Updated_At);
            }
        }
        private void FilterForm_Load(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (Tag is Form1 parent)
            {
                parent.RefreshMarkers();

            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }
    }
}
