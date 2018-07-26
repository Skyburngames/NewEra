using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventsNS;

/// <summary>
/// Will send a event, note: it will send the event WITHOUT parameters
/// </summary>

namespace BuildingBlocksNS
{
    [CreateAssetMenu(fileName = "new FireEvent", menuName = "BuildingBlocks/New FireEvent")]
    public class FireEvent : BuildingBlock
    {
        public override List<DataObject> Solve(List<DataObject> dataArray)
        {
            DataObject dataObjectBaseEvent = BuildingBlockChain.GetLastObjectOfTypeFromDataArray(dataArray, DataObject.ObjectTypes.Event);
            if (dataObjectBaseEvent == null)
            {
                Debug.LogError("FireEvent could not be resolved, because it couldnt load a GameEvent with Event as its type");
                return dataArray;
            }

            BaseEvent baseEvent = (BaseEvent)dataObjectBaseEvent.objectRef;
            baseEvent.Raise(new List<DataHolder>(), this); //will not send any parameters

            // Add the new event to the list
            DataObject nwDataObject = new DataObject(DataObject.ObjectTypes.Event, baseEvent);
            dataArray.Add(nwDataObject);

            return base.Solve(dataArray);
        }
    }
}
