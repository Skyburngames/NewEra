using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using CardsNS;
using EventsNS;
using GenericNS;

namespace BuildingsNS
{
    public class Building : MonoBehaviour {
        //static
        public static List<Building> buildings = new List<Building>();

        [Header("Events")]
        public BaseEvent mouseUp_Building;

        

        [Header("Citizen attached")] // see getters/setters
        private int cit_neutral = 0;
        private int cit_worker = 0;
        private int cit_scolar = 0;
        private int cit_military = 0;
        private int cit_merchant = 0;


        [Header("References")]
        public Transform buildingModelContainer;

        //private
        private bool _isHighlighted = false;
        private BuildingData _buildingData;


        private void OnEnable()
        {
            buildings.Add(this);
        }

        private void OnDisable()
        {
            buildings.Remove(this);
        }
        

        public void OnMouseUpAsButton()
        {
            Debug.Log("click on building: " + this.gameObject.name);

            // Raise click event with the building data
            List<DataHolder> dataList = new List<DataHolder>() { };
            dataList.Add(new DataHolder("building", this));
            mouseUp_Building.Raise(dataList, this);
        }

        public void OnMouseEnter()
        {
            ShowBuildingUI(true);
        }

        public void OnMouseExit()
        {
            
            ShowBuildingUI(false);
        }

        public void  ShowBuildingUI(bool show)
        {
            //create the buildingUI
            if (buildingUI == null && show) //first time create the buildingUI
            {
                //create the buildidngUI
                GameObject buildingUIGO = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BuildingUI"));
                buildingUI = buildingUIGO.GetComponent<BuildingUI>();
                buildingUI.Building = this;
            }
            
            buildingUI.gameObject.SetActive(show && !_isHighlighted); //dont show if building is being highlighted
        }


        public virtual bool CanCardBeAttached(Card card)
        {
            return true; // perhaps later there are buildings that cannot contain citizen
        }



        public BuildingData BuildingData
        {
            get
            {
                return _buildingData;
            }

            set
            {
                if(_buildingData  != null)
                {
                    Debug.LogError("You should not set BuildingData more then once on a building. See building: "+this.gameObject);
                }

                _buildingData = value;
                Setup();
            }
        }


        void Setup()
        {
            //TODO: Clear old data (although there should be old data)

            GameObject model = GameObject.Instantiate(BuildingData.prefab);
            model.transform.SetParent(buildingModelContainer, false);
            model.transform.localPosition = Vector3.zero;
        }


        
        public void Highlight(bool highlight)
        {
            this._isHighlighted = highlight;
            if (_isHighlighted)
            {
                this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
                this.transform.localScale = Vector3.one;
            }
        }


        public int Cit_neutral
        {
            get
            {
                return cit_neutral;
            }

            set
            {
                cit_neutral = value;
                Refresh();
            }
        }

        public int Cit_worker
        {
            get
            {
                return cit_worker;
            }

            set
            {
                cit_worker = value;
                Refresh();
            }
        }

        public int Cit_scolar
        {
            get
            {
                return cit_scolar;
            }

            set
            {
                cit_scolar = value;
                Refresh();
            }
        }

        public int Cit_military
        {
            get
            {
                return cit_military;
            }

            set
            {
                cit_military = value;
                Refresh();
            }
        }

        public int Cit_merchant
        {
            get
            {
                return cit_merchant;
            }

            set
            {
                cit_merchant = value;
                Refresh();
            }
        }


        private BuildingUI buildingUI;



        private void Refresh()
        {
            Debug.Log("Building with name: "+this.BuildingData.name + " now has citizen: "+this.cit_neutral);

           
        }


        public void Test()
        {
            Debug.Log("TEST IS SUCCESFULLY, event is caught!");
        }
    }
}
