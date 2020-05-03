using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardWebRequest.Model
{
    /// <summary>
    /// Represents a play card.
    /// </summary>
    class Card
    {
        public int Value { get; private set; }
        public ColorEnum Color { get; private set; }
        public CardTypeEnum CardType { get; private set; }

        public bool CardsInFrench { get; private set; }

        public Card(int avalue, ColorEnum aColor)
        {
            this.Value = avalue;
            this.Color = aColor;
            this.CardType = CardTypeEnum.Classic;
            ReadLanguageConfiguration();
        }

        public Card(int avalue, string aColorString)
        {
            this.Value = avalue;
            this.Color = (ColorEnum)Enum.Parse(typeof(ColorEnum), aColorString);
            this.CardType = CardTypeEnum.Classic;
            ReadLanguageConfiguration();
        }

        public Card(CardTypeEnum aCardType)
        {
            this.CardType = aCardType;
            ReadLanguageConfiguration();
        }

        public Card(string aCardTypeString)
        {
            this.CardType = (CardTypeEnum)Enum.Parse(typeof(CardTypeEnum), aCardTypeString);
            ReadLanguageConfiguration();
        }

        private void ReadLanguageConfiguration()
        {
            string language = System.Configuration.ConfigurationManager.AppSettings.Get("CardsInFrench");
            if (language == "0") CardsInFrench = false;
            else CardsInFrench = true;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            switch (CardType)
            {
                case CardTypeEnum.Classic:
                    switch (Value)
                    {
                        case 13:
                            s.Append(CardsInFrench ? "Roi" : "King");
                            break;
                        case 12:
                            s.Append(CardsInFrench ? "Reine" : "Queen");
                            break;
                        case 11:
                            s.Append(CardsInFrench ? "Valet" : "Jack");
                            break;
                        case 1:
                            s.Append(CardsInFrench ? "As" : "Ace");
                            break;
                        default:
                            s.Append(Value);
                            break;
                    }
                    if (CardsInFrench)
                    {
                        switch (Color)
                        {
                            case ColorEnum.Clubs:
                                s.Append(" de Trèfle");
                                break;
                            case ColorEnum.Spades:
                                s.Append(" de Pique");
                                break;
                            case ColorEnum.Diamonds:
                                s.Append(" de Carreau");
                                break;
                            case ColorEnum.Hearts:
                                s.Append(" de Coeur");
                                break;
                        }
                    }
                    else s.Append(" of ").Append(Color);
                    break;
                case CardTypeEnum.Joker:
                    s.Append("Joker");
                    break;
                case CardTypeEnum.Cdm:
                    s.Append("Cdm");
                    break;
            }
            return s.ToString();
        }

    }
}
