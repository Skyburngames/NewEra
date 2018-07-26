using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BuildingsNS;

namespace BuildingBlocksNS
{
    [CreateAssetMenu(fileName = "new ProvideBuildingPlot", menuName = "BuildingBlocks/New ProvideBuildingPlot")]
    public class ProvideRandomBuildingPlot : BuildingBlock
    {
        

        public override List<DataObject> Solve(List<DataObject> dataArray)
        {
            BuildingPlot nwBuildingPlot = null;
            int randomIndex = Random.Range(0, BuildingPlot.buildingPlots.Count);
            nwBuildingPlot = BuildingPlot.buildingPlots[randomIndex];

            if (nwBuildingPlot == null)
            {
                Debug.Log("ProvideBuildingPlot could not be resolved, it could not find BuildingPlots");
                return dataArray;
            }

            // only do something and alter the dataArray if it is certain this entire action can be resolved!
            DataObject nwDataObject = new DataObject(DataObject.ObjectTypes.BuildingPlot, nwBuildingPlot.GetComponent<BuildingPlot>());//new DataObject().Setup(DataObject.ObjectTypes.BuildingPlot, nwBuildingPlot.GetComponent<BuildingPlot>());
            dataArray.Add(nwDataObject);
            return base.Solve(dataArray);
        }
    }
}
