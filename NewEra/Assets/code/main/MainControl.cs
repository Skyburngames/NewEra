using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayerNS;

namespace MainNS { 
    public class MainControl : MonoBehaviour {

        public static MainControl instance; // singleton

        public Player humanPlayer;


        private void Awake()
        {
            MainControl.instance = this;
        }
        /*
        public List<CardBase> startingCards = new List<CardBase>();

        public Deck deckMain = new Deck();
        public Deck deckNextYear = new Deck();


        private void Awake()
        {
            MainControl.instance = this;
            deckMain.AddCardsToDeck(startingCards);
        }

        private void Start()
        {
            
        }
        */

    }
}
