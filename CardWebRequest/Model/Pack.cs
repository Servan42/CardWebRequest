using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardWebRequest.Model
{
    /// <summary>
    /// Represents a pack of play cards.
    /// </summary>
    class Pack
    {
        public List<Card> PackList { get; private set; }
        public int TotalCards { get; private set; }

        private int _nbOfJokers;
        private int _nbOfCdm;
        private static Random _rng = new Random();

        public Pack()
        {
            _nbOfCdm = int.Parse(ConfigurationManager.AppSettings.Get("NumberOfCdm"));
            _nbOfJokers = int.Parse(ConfigurationManager.AppSettings.Get("NumberOfJokers"));
            Init();
        }

        public void Init()
        {
            PackList = new List<Card>();
            foreach (ColorEnum color in Enum.GetValues(typeof(ColorEnum)))
            {
                for (int i = 1; i < 14; i++) PackList.Add(new Card(i, color));
            }

            for (int i = 0; i < _nbOfJokers; i++) { PackList.Add(new Card(CardTypeEnum.Joker)); }
            for (int i = 0; i < _nbOfCdm; i++) { PackList.Add(new Card(CardTypeEnum.Cdm)); }
            TotalCards = PackList.Count;
        }

        public void Shuffle()
        {
            int n = PackList.Count;
            while (n > 1)
            {
                n--;
                int k = _rng.Next(n + 1);
                Card value = PackList[k];
                PackList[k] = PackList[n];
                PackList[n] = value;
            }
        }

        public List<Card> Draw(int aAmount)
        {
            if (aAmount > PackList.Count) throw new Exception("Not enough cards remaining in the deck.");
            List<Card> returnList = new List<Card>();
            for (int i = 0; i < aAmount; i++)
            {
                returnList.Add(PackList[0]);
                PackList.RemoveAt(0);
            }
            return returnList;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach (Card card in PackList) s.Append(card).Append("\n");
            return s.ToString();
        }
    }
}
