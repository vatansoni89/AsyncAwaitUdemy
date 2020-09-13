using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            apiUrl = "https://localhost:44337/api";
            HttpClient = new HttpClient();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                //Loading gif don't appear quickly with 25000 req. 
                var cards = await GetCards(25000);

                await ProcessCards(cards);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"Operation took {sw.ElapsedMilliseconds / 1000.0} seconds.");
            loadingGIF.Visible = false;
        }

        private async Task<List<string>> GetCards(int amountOfCardsToGenerate)
        {
            return await Task.Run(() =>
            {
                var cards = new List<string>();
                for (int i = 0; i < amountOfCardsToGenerate; i++)
                {
                    cards.Add(i.ToString().PadLeft(16, '0'));
                }
                return cards;
            });

        }

        private async Task ProcessCards(List<string> cards)
        {
            var tasks = new List<Task<HttpResponseMessage>>();

            await Task.Run(() =>
            {
                foreach (var card in cards)
                {
                    var json = JsonConvert.SerializeObject(card);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var responseTask = HttpClient.PostAsync($"{apiUrl}/cards", content);
                    tasks.Add(responseTask);
                }
            });

            await Task.WhenAll(tasks);
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
