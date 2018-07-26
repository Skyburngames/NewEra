/* FIRE EVENT
 * - In Editor create a (scriptable object), give it a proper name. (this is a instance of GameEvent)
 * - Add a 'public GameEvent eventName' property to the monobehavior where you would like to call this event.
 * - In the code call eventName.Raise(data) to fire the event
 *      - data = List<NameObjectEntry> with keyValue pairs. The event will NOT be fired if the sender doesn't fill in the required data!
 *      - You can also call 'Raise()' but this will only work it there isnt any requiredData to be send (dataTypesRequired.count == 0)
 * - The BaseEvent is never transmitted, instead a GameEvent is send with the required data. (This prevents data mutation while running on a SO)
 * 
 * LISTEN TO EVENT
 * - Add a GameEventListener to the GameObject that should listen to the GameEvent
 * - Use the UnityEvent (response) to handle the event (this way you can NOT use parameters)
 * - or/and fill in the 'callMethod' string. All monobehaviours on this object that have a function with this name will be called.
 * - Then in the method being called make sure it has 1 parameter of type 'GameEvent'.
 * - You can now use the DataList (or the GetData() ) on GameEvent, cast the results to the desired types 
 * - NOTE: The keynames on the GameEvent matches those of dataTypesRequired.
 * 
 * Naming:
 *  - nameEvent_ObjectType -> if the events passes a object, attach this type
 *  
 *  Do NOT keep a reference to this object, since it will change if a new object calls the same type of baseEvent with different data (SO behaviour)
 */

using System.Collections.Generic;
using UnityEngine;

namespace EventsNS
{
    [CreateAssetMenu(fileName = "new BaseEvent", menuName = "Events/New BaseEvent")]
    public class BaseEvent : ScriptableObject
    {
        [Header("Data Types Required in Event")]
        [Tooltip("When some code calls Raise(list) it most provide a List<Object> with exaclty corresponds with the types in this array, " +
            "if everyhing goes well the listener can expect to recieve a GameEvent with a DataList that corresponds with the names in dataTypesRequired")]
        public List<TypeHolder> dataTypesRequired;

        

        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<BaseEventListener> eventListeners =
            new List<BaseEventListener>();

        /// <summary>
        /// Call this method to Raise(fire) this event. Provide a dataList with keyValue pairs that match the dataTypesRequired.
        /// NOTE: dataTypesRequired.name == dataList.name, but: dataTypesRequired.objectType = TYPE of object, dataList.dataObject = data OBJECT (runtime) self!
        /// </summary>
        /// <param name="dataList"></param>
        public void Raise(List<DataHolder> dataList, Object caller)
        {
            Debug.Log("Event "+ this +" called on object: " + caller );
            if (!IsDataCorrectlyProvided(dataList)) {
                Debug.LogError("The event: "+this+" has a required type that was not set in the dataList when calling Raise(..)");
                return;
            }

            // Create GameEvent that is actually send, baseEvent should never be altered, because it is referenced on multiple places
            GameEvent nwGameEvent = new GameEvent(this, dataList);

            //Raise the event
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(nwGameEvent);
        }


        bool IsDataCorrectlyProvided(List<DataHolder> dataList) //TODO
        {
            if(dataList == null)
            {
                return false;
            }


            // if length is not the same, the data is not correctly provided
            if(dataList.Count != this.dataTypesRequired.Count)
            {
                return false;
            }

            //loop through the entire dataList and for each element check if 
            for(int i= 0; i < dataTypesRequired.Count; i++)
            {
                DataHolder foundDataHolder = FindDataHolderInList(dataList, dataTypesRequired[i].name);
                if(foundDataHolder == null)
                {
                    return false;   // a required type is not found
                }
            }
            return true;
        }


        public DataHolder FindDataHolderInList(List<DataHolder> list, string name)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].name == name)
                {
                    return list[i];
                }
            }
            return null;
        }

        

        public void RegisterListener(BaseEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }


        public void UnregisterListener(BaseEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }



    // =================================================================== CLASSES ==========================================================================================
    public class DataHolder
    {
        public DataHolder(string name, Object dataObject)
        {
            this.name = name;
            this.dataObject = dataObject;
        }

        public string name = "";
        public Object dataObject;
    }


    [System.Serializable]
    public class TypeHolder
    {
        public TypeHolder(string name, Object objectType)
        {
            this.name = name;
            this.objectType = objectType;
        }

        public string name = "";
        public Object objectType;

    }
    


    // GameEvents are the in game variants on a baseEvent that are actually send in game (and possible altered)
    public class GameEvent{
        //Vars
        //public string eventName = "";
        //public EventType eventType = null;
        public BaseEvent baseEvent;
        private List<DataHolder> _dataList;

        //GETTERS and SETTERS
        public List<DataHolder> DataList
        {
            get
            {
                return _dataList;
            }

            set
            {
                _dataList = value;
            }
        }

        //ctor
        public GameEvent(BaseEvent baseEvent, List<DataHolder> nwDataList)
        {
            this.baseEvent = baseEvent;
            this.DataList = nwDataList;
        }


        public DataHolder GetData(string key)
        {
            foreach(DataHolder dh in DataList)
            {
                if(dh.name == key)
                {
                    return dh;
                }
            }
            return null; 
        }

        public DataHolder GetData(int index)
        {
            if(index < 0 || index >= DataList.Count)
            {
                Debug.LogWarning("Cannot return index: " + index + " from DataList in BaseEvent: " + this);
                return null;
            }
            return DataList[index];
        }
        
    }
}