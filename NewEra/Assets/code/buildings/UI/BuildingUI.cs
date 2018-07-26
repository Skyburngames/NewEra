using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuildingsNS
{
    public class BuildingUI : MonoBehaviour
    {
        private Building _building;
        public Vector3 worldOffset = new Vector3(1f, 0f, 1f);

        [Header("References")]
        public Text title;
        public Text citizenText;
        public Text workerText;
        public Transform buttonContainer;

        [Header("Prefabs")]
        public GameObject actionButtonPrefab;


        public Building Building
        {
            get
            {
                return _building;
            }

            set
            {
                _building = value;

                //create the buttons
                CreateActionButtons();


                Refresh();
            }
        }

        // ======================================== UNITY ===================================================
        private void Awake()
        {
            //attach to the buildingUIContainer (TODO: Make it more independent)
            this.transform.SetParent(GameObject.Find("UI/buildingUIContainer").transform, false);
        }
        

        private void Update()
        {
            Refresh();
        }

        

        // ======================================== PRIVATE ===================================================

        private void CreateActionButtons()
        {
            if(Building == null)
            {
                return;
            }

            foreach(BuildingAction ba in Building.BuildingData.buildingActions)
            {
                if (ba.showAsButton)
                {
                    //Instantiate new button
                    GameObject nwButton = GameObject.Instantiate(actionButtonPrefab);

                    //Set the data
                    BuildingActionButton buildingActionButton = nwButton.GetComponent<BuildingActionButton>();
                    buildingActionButton.BuildingAction = ba;

                    //set button position
                    nwButton.transform.SetParent(buttonContainer, false);
                }
            }
        }

        private void Refresh()
        {
            if(Building == null)
            {
                return;
            }

            title.text = Building.name;
            this.citizenText.text = "citizen: "+Building.Cit_neutral;
            this.workerText.text = "Wokers: " + Building.Cit_worker;

            RefreshScreenPosition();
        }

        private void RefreshScreenPosition()
        {
            if (Building == null)
            {
                return;
            }

            Vector2 screenPos = Camera.main.WorldToScreenPoint(Building.transform.position + worldOffset);
            //screenPos.y = Screen.height - screenPos.y;
            ((RectTransform)this.transform).position = screenPos;
        }

        
    }
}