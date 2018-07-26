using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardsNS;
using BuildingsNS;
using EventsNS;

/// <summary>
/// NOTE: becarefully with gameObjects that are prefabs and gameObjects that are buildings in game (and could be destroyed at any time!)
/// </summary>

namespace BuildingBlocksNS
{
    [System.Serializable]
    public class DataObject
    {
        public enum ObjectTypes {Card, BuildingPlot, Building, BuildingBase, CardPrefab, Event};

        /// <summary>
        /// Prefab is always a gameObject, others are ingame Scripts
        /// </summary>
        public ObjectTypes objectType;
        public Object objectRef; //you should only set it if this is a prefab


        public DataObject GetClone()
        {
            DataObject nwDataObject = new DataObject(this.objectType, this.objectRef);
            return nwDataObject;
        }


        /// <summary>
        /// Use this function to Get the type that belongs to the enum
        /// NOTE: if new ObjectTypes emerge, ADD THEM ONLY HERE!
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static System.Type GetObjectType(ObjectTypes _type)
        {
            switch (_type)
            {
                case ObjectTypes.Building:          return typeof(Building);
                case ObjectTypes.BuildingPlot:      return typeof(BuildingPlot);
                case ObjectTypes.BuildingBase:      return typeof(BuildingBase);
                case ObjectTypes.Card:              return typeof(Card);
                case ObjectTypes.CardPrefab:        return typeof(GameObject);
                case ObjectTypes.Event:             return typeof(BaseEvent);
                default:                            return null;
            }
        }


        public DataObject(ObjectTypes objectType, Object objectRef)
        {
            this.objectType = objectType;
            this.objectRef = objectRef;

            //IsDataCorrectlyFormatted();
        } 
        

        public DataObject CreateClone()
        {
            DataObject nwDataObject = new DataObject(this.objectType, this.objectRef);
            return nwDataObject;
        }
        
        /*
        public bool IsDataCorrectlyFormatted()
        {
            if(this.objectRef == null)
            {
                return false;
            }

            System.Type requiredType = DataObject.GetObjectType(this.objectType);
            return this.objectRef.GetType() == requiredType;
        }*/

    }
}
