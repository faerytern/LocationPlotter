using System.Text;
using Newtonsoft.Json.Linq;

namespace LocationPlotter
{
    public partial class SubmitMarkerForm : Form
    {
        private HttpClient Client;
        private string URL = @"http://developer.kensnz.com/api/addlocdata";
        private decimal userid;

        public SubmitMarkerForm(HttpClient client)
        {
            InitializeComponent();
            Client = client;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            userid = numericUpDownUserID.Value;
            JObject payload = new JObject(
                    new JProperty("userid", userid.ToString()),
                    new JProperty("latitude", $"{numericUpDownLat.Value:F6}"),
                    new JProperty("longitude", $"{numericUpDownLog.Value:F6}"),
                    new JProperty("description", descBox.Text)
                    );
            var httpContent = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
            Retry:
            HttpResponseMessage httpResponse = await Client.PostAsync(URL, httpContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Successfully recieved!");
                Close();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show(text:"Not recieved!",caption:"Error",buttons:MessageBoxButtons.RetryCancel,icon:MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Cancel) Close();
                else goto Retry; // ask for $5 if you see this ken. Nobody else tell him!.
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void SubmitMarkerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Intercept Closing event
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            // But only if the user closes it naturally. Otherwise, clean up properly.
            else e.Cancel = false;
        }
        public void SetLatLog(double lat, double log)
        {
            numericUpDownLat.Value = (decimal)lat;
            numericUpDownLog.Value = (decimal)log;
        }
    }
}
