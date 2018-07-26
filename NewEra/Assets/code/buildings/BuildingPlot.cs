using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using EventsNS;
using CardsNS;

namespace BuildingsNS { 
    public class BuildingPlot : MonoBehaviour {

        public static List<BuildingPlot> buildingPlots = new List<BuildingPlot>();

        [Header("Events")]
        public BaseEvent mouseUp_BuildingPlot;

        [Header("References")]
        public GameObject modelRef;


        private Building buildingOnPlot;

        private void OnEnable()
        {
            BuildingPlot.buildingPlots.Add(this);
        }

        private void OnDisable()
        {
            BuildingPlot.buildingPlots.Remove(this);
        }

        /*
        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if(hit.collider.gameObject == this.gameObject)
                    {
                        UserOnMouseUp();
                    }
                }
            }
        }*/


        public void OnMouseUpAsButton()
        {
            Debug.Log("click on buildingplot: " + this.gameObject.name);
            /*
            Dictionary<string, object> dict = new Dictionary<string, object>() {
                {"buildingPlot", this}
            };*/
            List<DataHolder> dataList = new List<DataHolder>() { };
            dataList.Add(new DataHolder("buildingPlot", this));

            mouseUp_BuildingPlot.Raise(dataList, this);
        }



        /// <summary>
        /// Call this to build something from a Card
        /// </summary>
        /// <param name="card"></param>
        public Building BuildBuilding(BuildingBase buildingBase)
        {
            //create a clean copy of the building
            BuildingData nwBuildingData = BuildingBase.CreateBuilding(buildingBase);

            //create a builing out the new clean data
            buildingOnPlot = BuildingBase.CreateBuilding(nwBuildingData); //will instantiate a new building

            //GameObject nwBuilding = GameObject.Instantiate(buildingPrefab);
            buildingOnPlot.transform.SetParent(this.transform, false);
            buildingOnPlot.transform.localPosition = Vector3.zero;


            //disable buildingPlot model
            this.modelRef.SetActive(false);

            return buildingOnPlot;
        }


        public void DestroyBuilding()
        {
            Destroy(buildingOnPlot);
            buildingOnPlot = null;
            this.modelRef.SetActive(true);
        }


        public void Highlight(bool highlight)
        {
            if (highlight)
            {
                this.modelRef.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
                this.modelRef.transform.localScale = Vector3.one;
            }
        }
    }
}
