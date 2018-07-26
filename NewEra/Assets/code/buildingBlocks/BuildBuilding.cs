using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BuildingsNS;

namespace BuildingBlocksNS {
    [CreateAssetMenu(fileName = "new BuildBuilding", menuName = "BuildingBlocks/New Building")]
    public class BuildBuilding : BuildingBlock {
        // GameObject buildingPrefab;
        //public BuildingPlot buildingPlot;


        public override List<DataObject> Solve(List<DataObject> dataArray)
        {
            // Load a Building
            DataObject dataObjectBuildingBase = BuildingBlockChain.GetLastObjectOfTypeFromDataArray(dataArray, DataObject.ObjectTypes.BuildingBase);//(GetLastObjectOfTypeFromDataArray<GameObject>(dataArray));
            if (dataObjectBuildingBase == null)
            {
                Debug.LogError("BuildBuilding could not be resolved, because it couldnt load a dataObject with BuildingBase as its type");
                return dataArray;
            }

            // Load a BuildingPlot
            DataObject dataObjectBuildingPlot = BuildingBlockChain.GetLastObjectOfTypeFromDataArray(dataArray, DataObject.ObjectTypes.BuildingPlot);
            if(dataObjectBuildingPlot == null)
            {
                Debug.LogError("BuildBuilding could not be resolved, because it couldnt load a dataObject with BuildingPlot as its type");
                return dataArray;
            }

            // only do something and alter the dataArray if it is certain this entire action can be resolved!
            BuildingBase buildingBase = (BuildingBase)dataObjectBuildingBase.objectRef;
            
            Building nwBuilding = ((BuildingPlot)dataObjectBuildingPlot.objectRef).BuildBuilding(buildingBase);
            DataObject nwDataObject = new DataObject(DataObject.ObjectTypes.Building, nwBuilding.gameObject.GetComponent<Building>());//new DataObject().Setup(DataObject.ObjectTypes.Building, nwBuilding.GetComponent<Building>());
     
            // TODO: check if data is correctly formatted!
            // TODO2: Do this in a editor
            Debug.Log("TODO: check if data is correctly formatted!");
            
            dataArray.Add(nwDataObject); //build the building and add it to the array

            return base.Solve(dataArray);
        }
    }
}
