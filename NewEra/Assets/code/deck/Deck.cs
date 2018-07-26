using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardsNS;
using EventsNS;

namespace DeckNS
{
    [System.Serializable]
    public class Deck : ScriptableObject
    {
        public List<CardData> cards = new List<CardData>(); // Use AddCard() and RemoveCard() to add/remove


        public void AddCards(List<CardBase> cardsBase)
        {
            foreach (CardBase cb in cardsBase)
            {
                AddCard(cb);
            }
        }


        public void AddCard(CardBase cardBase)
        {
            CardData nwCardData = CardBase.CreateCard(cardBase);
            AddCard(nwCardData);

            
        }


        public void AddCard(CardData cardData)
        {
            cards.Add(cardData);

            //addCard event
            List<DataHolder> dataHolders = new List<DataHolder>();
        }


        public void RemoveCard(CardData cardData)
        {
            this.cards.Remove(cardData);
        }
    }
}
