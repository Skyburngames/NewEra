using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardsNS;

/// <summary>
/// This class will handle a card. it will send a event for each type of required Data (in preset) is required and wait for a response.
/// Eventually it will solve the card
/// 
/// - It will create a BaseEventListener for each type of data it listens to, set in playerDataProviders
/// </summary>

using BuildingBlocksNS;
using EventsNS;

namespace PlayerNS
{
    public class PlayerSolveCard : MonoBehaviour {
        public List<PlayerDataProvider> playerDataProviders = new List<PlayerDataProvider>();

        [HideInInspector] public Card card;
        [HideInInspector] public int presetIndex = 0;
        [HideInInspector] public PlayerDataProvider playerDataProviderActive = null;


        List<DataObject> presetData = new List<DataObject>();

        public BaseEvent cardPlayed;


        private void Start()
        {
            //create BaseEventListeners
            foreach(PlayerDataProvider pdp in playerDataProviders)
            {
                BaseEventListener eventListenerResponse = this.gameObject.AddComponent<BaseEventListener>();
                eventListenerResponse.Events.Add(pdp.waitForResponse);
                eventListenerResponse.callMethod = "ResponseEvent";

                /*
                BaseEventListener eventListenerResponseCancel = this.gameObject.AddComponent<BaseEventListener>();
                eventListenerResponseCancel.Events.Add(pdp.cancelResponse);
                eventListenerResponseCancel.callMethod = "ResponseEvent";
                */
            }
        }


        public void StartSolvingPresetDataCard(Card nwCard)
        {
            //reset old data
            ResetCard(); 

            //start with new card
            this.card = nwCard;
            if(this.card == null)
            {
                Debug.Log("Cannot solve the card, because the card provided is null");
                return;
            }

            //create a clone of the presetDataRequired. this cloned data can be filled with actual game data
            /*
            foreach (DataObject dataObj in card.CardData.baseBuildingBlockChain.presetDataRequired)
            {
                presetData.Add(dataObj.CreateClone());
            }*/
            this.presetData = card.CardData.baseBuildingBlockChain.GetClonePresetData();
            
            SolveNextPresetData();
        }


        public void SolveNextPresetData()
        {
            if(this.card == null)
            {
                Debug.Log("Cannot solve the card, because the card is null. Is StartSolvingPresetDataCard called first?");
                return;
            }
            this.presetIndex++;
            //if(this.presetIndex >= card.CardData.cardBase.baseBuildingBlockChain.p)

            if(this.presetIndex >= presetData.Count)
            {
                // TODO: preset is done!
                SolveCard();
                return;
            }

            DataObject currentDataObject = presetData[this.presetIndex];
            if(currentDataObject.objectRef != null)
            {
                //data is already set, continue
                SolveNextPresetData();
                return;
            }

            playerDataProviderActive = GetPlayerDataProvider(currentDataObject.objectType);

            // No additional info is required (event itself will tell what is requires, (todo: perhaps later send more info if more specific conditions are required)
            List<DataHolder> dh = new List<DataHolder>();
            dh.Add(new DataHolder("card", this.card));
            playerDataProviderActive.request.Raise(dh, this); 
        }


        /// <summary>
        /// This function is called if a event is fired on which this object listen and maby waits
        /// </summary>
        /// <param name="gameEvent"></param>
        public void ResponseEvent(GameEvent gameEvent)
        {
            if(playerDataProviderActive.waitForResponse == gameEvent.baseEvent)
            {
                //continue!
                DataHolder dh = gameEvent.GetData(0); //the first object on this type of event is always the object requested for
                Debug.Log("Data recieved--: " + dh.dataObject);
                this.presetData[presetIndex].objectRef = dh.dataObject; //store provided data
                SolveNextPresetData();
            }/*else if(playerDataProviderActive.cancelResponse == gameEvent.baseEvent)
            {
                //cancel
                ResetCard();
                return;
            }*/
        }


        public void CancelResponseEvent(GameEvent gameEvent)
        {
            ResetCard();
        }


        /// <summary>
        /// This function is called if all presetData is set
        /// </summary>
        private void SolveCard()
        {
            this.card.CardData.SolveCard(this.presetData);
            Destroy(this.card.gameObject); //remove the cards gameObject. The event that is fired will remove the event from the hand

            List<DataHolder> list = new List<DataHolder>();
            list.Add(new DataHolder("card", this.card));
            cardPlayed.Raise(list, this);
            
            ResetCard();
        }


        private void ResetCard()
        {
            this.card = null;
            this.presetIndex = -1; //will be set to 0 in SolveNext();
            presetData = new List<DataObject>();
            playerDataProviderActive = null;
        }

        
        private PlayerDataProvider GetPlayerDataProvider(DataObject.ObjectTypes objectType)
        {
            foreach(PlayerDataProvider wfe in playerDataProviders)
            {
                if(wfe.objectType == objectType)
                {
                    return wfe;
                }
            }
            return null;
        }


    }

    [System.Serializable]
    public class PlayerDataProvider
    {
        public DataObject.ObjectTypes objectType;
        public BaseEvent request;
        public BaseEvent waitForResponse;
        //public BaseEvent cancelResponse;
    }
}
