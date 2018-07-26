using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EventsNS;
using BuildingsNS;

public class PlayerSelectBuilding : MonoBehaviour {

    public BaseEvent response;

    [HideInInspector] public bool waitingOnPlayer = false;

    public void LetPlayerSelectBuilding(GameEvent gameEvent)
    {
        waitingOnPlayer = true;
        HighlightAllBuildings(true);
    }


    public void OnClickBuilding(GameEvent gameEvent)
    {
        if (waitingOnPlayer)
        {
            Building buildingClicked = (Building)gameEvent.GetData("building").dataObject;

            waitingOnPlayer = false;
            HighlightAllBuildings(false);

            //raise the event
            List<DataHolder> dataHoldersList = new List<DataHolder>();
            dataHoldersList.Add(new DataHolder("building", buildingClicked));
            response.Raise(dataHoldersList, this);
        }
    }

    public void OnCancelSelectBuilding(GameEvent gameEvent)
    {
        waitingOnPlayer = false;
        HighlightAllBuildings(false);
    }



    // ==================================================== PRIVATE ==============================================================
    void HighlightAllBuildings(bool highlight)
    {
        foreach (Building b in Building.buildings)
        {
            b.Highlight(highlight);
        }
    }


}
