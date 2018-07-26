using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DO NOT SET OR ALTER data on a buildingBlock or BuildingBlockChain! (scriptable objects)
/// </summary>


namespace BuildingBlocksNS
{
    [System.Serializable]
    public class BuildingBlockChain
    {

        public List<DataObject> presetDataRequired = new List<DataObject>(); // only acts as a list of objects that are required. dont store data in here!
        public List<BuildingBlock> buildingBlocks = new List<BuildingBlock>();

        /*
        public BuildingBlockChain GetClone()
        {
            BuildingBlockChain clonedBuildingBlockChain = new BuildingBlockChain();
            
            foreach(DataObject dataObj in this.presetDataRequired)
            {
                clonedBuildingBlockChain.presetDataRequired.Add(dataObj.GetClone());
            }
            clonedBuildingBlockChain.buildingBlocks = this.buildingBlocks;

            return clonedBuildingBlockChain;
        }*/

        public List<DataObject> GetClonePresetData()
        {
            List<DataObject> presetDataResult = new List<DataObject>();
            foreach (DataObject dataObj in this.presetDataRequired)
            {
                presetDataResult.Add(dataObj.CreateClone());
            }
            return presetDataResult;
        }

   
        /// <summary>
        /// this will create a clone of the preset data and start the chain
        /// </summary>
        public void StartChain()
        {
            List<DataObject> presetDataTmp = GetClonePresetData();
            StartChain(presetDataTmp);
        }



        /// <summary>
        /// Use this function if you first want to alter the presetData. Never direclty alter presetDataRequired. but instead use GetClonePresetData and alter that and then provide it to this function
        /// </summary>
        /// <param name="presetData"></param>
        public void StartChain(List<DataObject> presetData)
        {
            //IsPresetDataCorrectlyFormatted();

            List<DataObject> chain = presetData; //start with the presetData (NOTE: this is NOT presetDataRequired, presetDataRequired only shows the types that are required for this chain)
            for (int i=0; i < buildingBlocks.Count; i++)
            {
                chain = buildingBlocks[i].Solve(chain);
            }
        }


        // ============================================================ STATIC ================================================================

        public static DataObject GetLastObjectOfTypeFromDataArray(List<DataObject> dataArray, DataObject.ObjectTypes objectType)
        {
            for (int i = dataArray.Count - 1; i >= 0; i--)
            {
                if (dataArray[i].objectType == objectType)
                {
                    return dataArray[i];
                }
            }
            return null; //nothing found
        }


        // ============================================================ PRIVATE ================================================================
        /*
        private bool IsPresetDataCorrectlyFormatted()
        {
            foreach(DataObject dataObj in this.presetData)
            {
                if (!dataObj.IsDataCorrectlyFormatted())
                {
                    Debug.Log("Preset data of "+this+" is not correctly formatted");
                    return false;
                }
            }
            return true;
        }*/
    }
}
