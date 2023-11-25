using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordingSystem_CarMaintenance
{
    public partial class StartingForm : Form
    {
        public StartingForm()=>InitializeComponent();

        private void Request_Click(object sender, EventArgs e)
        { Hide(); FormRequest.Request_Authorized = false; new FormRequest().ShowDialog(); Show(); }

        private void Authorization_Click(object sender, EventArgs e)
        { Hide(); new Authorization().ShowDialog(); Show(); }
    }
}
