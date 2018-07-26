using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BuildingBlocksNS;

namespace CardsNS
{
    [CreateAssetMenu(fileName = "new baseCard", menuName = "Cards/New Card")]
    public class CardBase : ScriptableObject
    {
        public string baseName = "new card";
        public Sprite image;
        public string description;
        public bool goesIntoNextYear = true;


        /// <summary>
        /// The card will solve these events in order.
        ///     Fill in all needed data before calling 'UseCard()'
        /// </summary>
        [Tooltip("The card will solve these events in order.")]
        public BuildingBlockChain baseBuildingBlockChain;



        public static CardData CreateCard(CardBase cardBase)
        {
            CardData nwCardData = new CardData(cardBase);
            return nwCardData;
        }


        public static GameObject CreateCard(CardData cardData)
        {
            GameObject nwCard = GameObject.Instantiate(Resources.Load("prefabs/card", typeof(GameObject))) as GameObject;
            Card card = nwCard.GetComponent<Card>();
            card.CardData = cardData;
            return nwCard;
        }

    }
}