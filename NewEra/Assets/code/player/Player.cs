using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardsNS;
using DeckNS;
using EventsNS;
using BuildingsNS;

namespace PlayerNS { 
    public class Player : MonoBehaviour {

        private Card cardSelected;

        public PlayerSolveCard playerSolveCard;
        public List<CardBase> deckStart = new List<CardBase>();

        public int cardsPerRound = 5;

        [Header("Events")]
        public BaseEvent cardDrawn;
        public BaseEvent handChange;
        public BaseEvent deckChange;
        public BaseEvent nextYearChange;
        public BaseEvent newRound;

        [Header("In Game Data")]
        [HideInInspector] public Deck deckMain;
        [HideInInspector] public Deck deckNextYear;
        [HideInInspector] public Deck cardsInHand;


        private void Awake()
        {
            deckMain = new Deck();
            deckNextYear = new Deck();
            cardsInHand = new Deck();

            deckMain.AddCards(deckStart);
            
        }

        private void Start()
        {
            NextRound(); //start game
        }

        // ======================================================= PLAYER ACTIONS ====================================================================
        public void NextRound()
        {
            //TODO: If still cards in hand, play/remove them

            for(int i=0; i < cardsPerRound; i++)
            {
                DrawCard();
            }

            List<DataHolder> dataHolders = new List<DataHolder>();
            dataHolders.Add(new DataHolder("deck", this.cardsInHand));
            deckChange.Raise(dataHolders, this);
        }


        public void DrawCard(bool random = true)
        {
            if(deckMain.cards.Count <= 0)
            {
                Debug.Log("Cannot draw a card, deckmain is empty");
                return;
            }
            int index = 0;
            if (random)
            {
                index = Random.Range(0, this.deckMain.cards.Count);
            }
            CardData drawnCard = this.deckMain.cards[index];

            this.cardsInHand.AddCard(drawnCard); //add to hand
            this.deckMain.RemoveCard(drawnCard); //remove from deck


            //draw card event
            List<DataHolder> dataHolders = new List<DataHolder>();
            dataHolders.Add(new DataHolder("cardData", drawnCard));
            dataHolders.Add(new DataHolder("player", this));
            cardDrawn.Raise(dataHolders, this);

            OnHandChange();
            OnDeckChange();
        }


        public void OnHandChange()
        {
            List<DataHolder> dataHolders = new List<DataHolder>();
            dataHolders.Add(new DataHolder("deck", this.cardsInHand));
            handChange.Raise(dataHolders, this);
        }


        public void OnDeckChange()
        {
            List<DataHolder> dataHolders = new List<DataHolder>();
            dataHolders.Add(new DataHolder("deck", this.deckMain));
            deckChange.Raise(dataHolders, this);
        }


        public void OnNextYearChange()
        {
            List<DataHolder> dataHolders = new List<DataHolder>();
            dataHolders.Add(new DataHolder("deck", this.deckNextYear));
            nextYearChange.Raise(dataHolders, this);
        }




        // ======================================================= LISTEN TO EVENTS ====================================================================
        /// <summary>
        /// Called if a card is played
        /// </summary>
        /// <param name="gameEvent"></param>
        public void CardIsPlayed(GameEvent gameEvent)
        {
            Card cardPlayed = (Card)gameEvent.GetData("card").dataObject;
            if (cardPlayed.CardData.goesIntoNextYear)
            {
                deckNextYear.cards.Add(cardPlayed.CardData);
                OnNextYearChange();
            }
        }


        /// <summary>
        /// Called if a card is selected
        /// </summary>
        /// <param name="gameEvent"></param>
        public void CardSelected(GameEvent gameEvent)
        {
            this.cardSelected = (Card)gameEvent.GetData("card").dataObject;
            Debug.Log("Card selected in playCardsFromHand: " + this.cardSelected.CardData.cardName);

            playerSolveCard.StartSolvingPresetDataCard(this.cardSelected);
        }


        /// <summary>
        /// Called if a card is deselected
        /// </summary>
        /// <param name="gameEvent"></param>
        public void CardDeselected(GameEvent gameEvent)
        {
            Debug.Log("Card deselected in playCardsFromHand: " + (Card)gameEvent.GetData("card").dataObject);
            this.cardSelected = null;
        }

       
    }
}