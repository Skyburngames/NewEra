using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// Add events to 'Events'. In the next update the events will recieve the listener. In activeEvents is collected which events are already added
/// </summary>

namespace EventsNS
{
    public class BaseEventListener : MonoBehaviour
    {
        public List<BaseEvent> Events = new List<BaseEvent>();
        List<BaseEvent> activeEvents = new List<BaseEvent>();

        [Tooltip("This method will be called on Monobehaviours attached to the same GameObject as this GameEventListener is attached to. (you CAN access the GameEvent.data this way)")]
        public string callMethod;

        public UnityEvent Response;


        // ======================================================= UNITY ====================================================================

        private void OnEnable()
        {
            RefreshActiveEvents();
        }


        private void Update()
        {
            RefreshActiveEvents();
        }
        

        private void OnDisable()
        {
            foreach (BaseEvent be in activeEvents)
            {
                be.UnregisterListener(this);
            }
        }


        // ======================================================= PUBLIC ====================================================================
        public void OnEventRaised(GameEvent gameEvent)
        {
            if(Response != null)
            {
                Response.Invoke();
            }
            SendMessage(callMethod, gameEvent, SendMessageOptions.DontRequireReceiver);
        }


        // ======================================================= PRIVATE ====================================================================
        /// <summary>
        /// Adds the events that are added to 'Events' to the activeEvents which are actually used
        /// </summary>
        private void RefreshActiveEvents()
        {
            // pickup and add all new events
            foreach (BaseEvent be in Events)
            {
                if (!activeEvents.Contains(be))
                {

                    if (be == null)
                    {
                        Debug.LogError("baseEvent in listener: " + this + " is null!");
                        return;
                    }

                    be.RegisterListener(this);
                    activeEvents.Add(be);
                }
            }
        }
    }
}