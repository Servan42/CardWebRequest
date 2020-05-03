using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CardWebRequest.Model;

namespace CardWebRequest.Network
{
    class WebRequest
    {
        private List<string> URLList;
        private int _currentPlayerNumber;
        private readonly HttpClient client = new HttpClient();

        public int NumberOfPlayers { get; private set; }

        public WebRequest()
        {
            _currentPlayerNumber = 0;
            URLList = new List<string>();
            URLList.AddRange(ConfigurationManager.AppSettings.Get("URLPlayers").Split(';'));
            NumberOfPlayers = URLList.Count;
        }

        public void SendToNextPlayer(List<Card> CardList)
        {
            string URL = URLList[_currentPlayerNumber];
            _currentPlayerNumber = (_currentPlayerNumber + 1) % URLList.Count();

            StringBuilder s = new StringBuilder();
            s.Append("------------------\n");
            foreach (Card card in CardList) s.Append(card).Append("\n");

            var values = new Dictionary<string, string>
            {
               { "content", s.ToString() }
            };

            Task task = call(values, URL);
            task.Wait();
        }

        private async Task call(Dictionary<string, string> aValues, string aURL)
        {
            var content = new FormUrlEncodedContent(aValues);
            var response = await client.PostAsync(aURL, content);
            var responseString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseString);
        }
    }
}
