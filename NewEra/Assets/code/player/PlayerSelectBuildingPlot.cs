using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventsNS;
using BuildingsNS;

namespace PlayerNS { 
    public class PlayerSelectBuildingPlot : MonoBehaviour {


        public BaseEvent response;

        [HideInInspector] public bool waitingOnPlayer = false;


	    public void LetPlayerSelectBuildingPlot(GameEvent gameEvent)
        {
            waitingOnPlayer = true;
            HighlightAllBuildingPlots(true);
        }


        public void OnClickBuildingPlot(GameEvent gameEvent)
        {
            if (waitingOnPlayer)
            {
                BuildingPlot buildingPlotClicked = (BuildingPlot)gameEvent.GetData("buildingPlot").dataObject;

                waitingOnPlayer = false;
                HighlightAllBuildingPlots(false);
                
                //raise the event
                List<DataHolder> dataHoldersList = new List<DataHolder>();
                dataHoldersList.Add(new DataHolder("buildingPlot", buildingPlotClicked));
                response.Raise(dataHoldersList, this);
            }
        }

        public void OnCancelSelectBuildingPlot(GameEvent gameEvent)
        {
            waitingOnPlayer = false;
            HighlightAllBuildingPlots(false);
        }




        // ==================================================== PRIVATE ==============================================================
        void HighlightAllBuildingPlots(bool highlight)
        {
            foreach (BuildingPlot bp in BuildingPlot.buildingPlots)
            {
                bp.Highlight(highlight);
            }
        }
    }
}
