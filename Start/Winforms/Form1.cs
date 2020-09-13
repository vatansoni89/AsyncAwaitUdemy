using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winforms
{
    public partial class Form1 : Form
    {
        private readonly string apiUrl;
        private readonly HttpClient HttpClient;

        public Form1()
        {
            InitializeComponent();
            apiUrl = "https://localhost:44337/api/greetings";
            HttpClient= new HttpClient();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;
            await Wait();
            var name = txtName.Text;
            //MessageBox.Show("From calling function after 0 sec");
            try
            {
                 var greeting = await GetGreetings(name);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            //MessageBox.Show(greeting);
            loadingGIF.Visible = false;
        }

        private async Task Wait()
        {
            await Task.Delay(TimeSpan.FromSeconds(0));
        }

        private async Task<string> GetGreetings(string name)
        {
            using (var response = await HttpClient.GetAsync($"{apiUrl}1/{name}"))
            {
                response.EnsureSuccessStatusCode();
                var greeting = await response.Content.ReadAsStringAsync();
                return greeting;
            }
        }

        /* without await output:
        Before 15 sec await
        From calling function
        After 15 sec await
         */

        /* with await output:
        Before 15 sec await
        After 15 sec await
        From calling function
         */

        /*
          WaitingForActivation means await is not there.
          If exception happen in non awaited task function then exception wil be wrappen in a Task and wont be catched, so for exception handling await is must.
        */
    }
}
