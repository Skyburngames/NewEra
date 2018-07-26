using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MainNS;
using CardsNS;
using EventsNS;
using DeckNS;

namespace UINS {
    public class Hand : MonoBehaviour
    {
        [Tooltip("If a card moves on its position it is lerped to its new position with this speed")]
        public float cardLerpSpeed = 10f;

        

        //public Vector2 padding = Vector2.zero;
        public Vector2 offset = Vector2.zero;

        public GameObject container;
        List<Card> cards = new List<Card>();


        // Use this for initialization
        void Start()
        {
            //RefreshCards();

            //move from the middle
            foreach (Card c in cards)
            {
                c.transform.position = new Vector2(this.container.transform.position.x, 0f);
            }
        }


        public void HideHandExceptActiveCard(GameEvent gameEvent)
        {
            Card activeCard = (Card)gameEvent.GetData("card").dataObject;
            if(activeCard == null) {
                return;
            }

            foreach(Card c in cards)
            {
                c.gameObject.SetActive(c == activeCard);
            }
        }


        public void ShowAllCards()
        {
            foreach (Card c in cards)
            {
                c.gameObject.SetActive(true);
            }
        }


        private void Update()
        {
            RefreshCardPositions();
        }


        public void CardPlayed(GameEvent gameEvent)
        {
            Card cardPlayed = (Card)gameEvent.GetData("card").dataObject;
            if (cards.Contains(cardPlayed))
            {
                cards.Remove(cardPlayed);
            }
            RefreshCardPositions();
        }

        
        //TODO: verwijder deze functie en regel het via HandCardsChange
        public void RefreshCards()
        {
            ResetHand();

            //build new cards
            foreach (CardData cd in MainControl.instance.humanPlayer.cardsInHand.cards)
            {
                Card nwCard = (CardBase.CreateCard(cd)).GetComponent<Card>();
                cards.Add(nwCard);
                nwCard.transform.SetParent(this.container.transform);
            }

            RefreshCardPositions();
        }


        public void HandCardsChange(GameEvent gammeEvent)
        {
            Deck handDeck = (Deck)gammeEvent.GetData("deck").dataObject;
            
            ResetHand();

            //build new cards
            foreach (CardData cd in handDeck.cards)
            {
                Card nwCard = (CardBase.CreateCard(cd)).GetComponent<Card>();
                cards.Add(nwCard);
                nwCard.transform.SetParent(this.container.transform);
            }

            RefreshCardPositions();
        }


        public void ResetHand()
        {
            // remove all child objects
            foreach (Transform child in container.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            // empty card list
            cards = new List<Card>();
        }


        /*
        public void RemoveAllCards()
        {
            // remove all child objects
            foreach (Transform child in container.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            // empty card list
            cards = new List<Card>();
        }


        public void GainCard(CardData cardData)
        {
            Card nwCard = (CardBase.CreateCard(cardData)).GetComponent<Card>();
            cards.Add(nwCard);
            nwCard.transform.SetParent(this.container.transform);

            RefreshCardPositions();
        }*/


        private void RefreshCardPositions()
        {
            if (cards.Count <= 0)
            {
                return;
            }
            /*
            float previousElementsX = padding.x;
            for(int i=0; i < cards.Count; i++)
            {
                Card currentCard = cards[i];
                Vector2 nwPosition = new Vector2(previousElementsX, padding.y);
                float currentCardScale = currentCard.transform.localScale.x;
                float currentCardSize = ((RectTransform)currentCard.transform).sizeDelta.x;
                previousElementsX += offset.x + (currentCardScale * currentCardSize);
                currentCard.transform.localPosition = nwPosition;
            }*/
            float totalWidth = 0;
            Card currentCard;

            //calc the total width
            for (int i = 0; i < cards.Count; i++)
            {
                currentCard = cards[i];
                float cardScale = currentCard.transform.localScale.x;
                float cardBaseSize = ((RectTransform)currentCard.transform).sizeDelta.x;
                float cardSize = cardBaseSize * cardScale;
                totalWidth += cardSize;
            }
            //add all the offsets
            totalWidth += (cards.Count - 1) * offset.x; // all cards have a offset to the next card, except the last card

            float positionX = this.container.transform.position.x - (totalWidth / 2f);


            // set the positions
            for (int i = 0; i < cards.Count; i++)
            {
                currentCard = cards[i];
                Vector2 cardSize = currentCard.GetCardSize();
                positionX += cardSize.x / 2f;

                //set the position
                Vector2 nwPosition = new Vector2(positionX, this.container.transform.position.y);
                //currentCard.transform.position = nwPosition;

                currentCard.transform.position = Vector2.Lerp(currentCard.transform.position, nwPosition, cardLerpSpeed * Time.deltaTime);

                positionX += (cardSize.x / 2f) + offset.x;

                /*
                float cardScale = currentCard.transform.localScale.x;
                float cardBaseSize = ((RectTransform)currentCard.transform).sizeDelta.x;
                float cardSize = cardBaseSize * cardScale;
                positionX += (cardSize + offset.x) / 2f;
                */
                //Vector2 nwPosition = new Vector2(positionX, this.container.transform.position.y); //TODO Y

                //currentCard.transform.position = nwPosition;
            }
        }

        
    }
}
