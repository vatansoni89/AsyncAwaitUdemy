using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;
            Wait();
            loadingGIF.Visible = false;
            MessageBox.Show("From calling function");
        }

        private async Task Wait()
        {
            MessageBox.Show("Before 15 sec await");
            await Task.Delay(TimeSpan.FromSeconds(15));
            MessageBox.Show("After 15 sec await");
        }

        /* without await output:
        Before 15 sec await
        From calling function
        After 15 sec await
         */
    }
}
