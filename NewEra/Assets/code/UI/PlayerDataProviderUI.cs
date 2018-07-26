using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventsNS;

namespace UINS { 
    public class PlayerDataProviderUI : MonoBehaviour {
        
        public BaseEvent cancelResponse;
        
        [Header("References")]
        public GameObject cancelButton;


        private void Awake()
        {
            cancelButton.SetActive(false);
        }


        public void ShowCancelButton()
        {
            cancelButton.SetActive(true);
        }


        public void HideCancelButton()
        {
            cancelButton.SetActive(false);
        }


        public void ClickOnCancelButton()
        {
            //List<DataHolder> dataHolderList = new List<DataHolder>();
            //dataHolderList.Add(new DataHolder("cancel", null));
            cancelResponse.Raise(new List<DataHolder>(), this);
        }
    }
}