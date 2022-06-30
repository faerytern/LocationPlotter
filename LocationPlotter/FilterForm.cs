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
        public FilterForm(MapForm parent, InterestingPlaceOptions options, DataTable table)
        {
            InitializeComponent();
            Tag = parent;

            dataGridView1.DataSource = table;
            propertyGrid1.SelectedObject = options;
        }
        
        private void FilterForm_Load(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (Tag is MapForm parent) parent.RefreshMarkers();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }
    }
}
