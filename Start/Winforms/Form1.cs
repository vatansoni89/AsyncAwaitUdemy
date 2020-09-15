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
using System.Threading;
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
                //We do Task.Run to solve it.
                var cards = await GetCards(1500);

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
            using var semaphore = new SemaphoreSlim(100); // To ensure how many Task can run at same time. 
                                                        //Throttle the amount of http req. in our case.
            var tasks = new List<Task<HttpResponseMessage>>();

            tasks = cards.Select(async card =>
            {
                var json = JsonConvert.SerializeObject(card);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await semaphore.WaitAsync();
                try
                {
                    return await HttpClient.PostAsync($"{apiUrl}/cards", content);
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToList();

            var responses = await Task.WhenAll(tasks);
            var rejectedcards = new List<string>();

            foreach (var response in responses)
            {
                var content = await response.Content.ReadAsStringAsync();
                var card = JsonConvert.DeserializeObject<CardResponse>(content);
                if (!card.Approved)
                {
                    rejectedcards.Add(card.Card);
                }
            }

            foreach (var card in rejectedcards)
            {
                Console.WriteLine($"Card {card} was rejected.");
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
