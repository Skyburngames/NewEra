using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BuildingsNS;
using BuildingBlocksNS;
using EventsNS;

namespace CardsNS
{
    [System.Serializable]
    public class CardData: Object
    {
        public string cardName;
        public Sprite image;
        public string description;
        public bool goesIntoNextYear = true;

     

        [Tooltip("The card will solve these events in order.")]
        public BuildingBlockChain baseBuildingBlockChain;

        public CardData(CardBase cardBase)
        {
            //this.cardBase = cardBase;
            this.cardName = cardBase.baseName;
            this.image = cardBase.image;
            this.description = cardBase.description;
            this.baseBuildingBlockChain = cardBase.baseBuildingBlockChain;
            this.goesIntoNextYear = cardBase.goesIntoNextYear;
        }




        /*
        /// <summary>
        /// Call this to Use the card
        /// If the card wanne do something and requires additional info that is not provided, the card will choose random targets
        /// OR if nothing is possible the card will do nothing
        /// </summary>
        public void UseCard()
        {
            Debug.Log("Card: " + this.cardName + " is called without parameters, TODO: random targets");
        }


        public void UseCard(BuildingPlot buildingPlot)
        {
            if (buildingPlot == null || this.cardBase.buildingPrefab == null)
            {
                Debug.Log("Card: "+this.cardName+" is called with buildingPlot, but either the buildingplot or the the cardBase.buildingPrefab is null. Random target will be selected");
                UseCard(); // Use random targets
            }

            buildingPlot.BuildBuilding(this);
        }*/


        /// <summary>
        /// Solve the card, if not all data is set in a effect, that effect will do nothing
        /// </summary>
        public void SolveCard(List<DataObject> presetData)
        {
            this.baseBuildingBlockChain.StartChain(presetData);
        }
    }
}
