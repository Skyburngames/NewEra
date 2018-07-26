// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace EventsNS
{
    [CustomEditor(typeof(BaseEvent))]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            BaseEvent e = target as BaseEvent;
            if (GUILayout.Button("Raise"))
                e.Raise(null, this);
        }
    }
}