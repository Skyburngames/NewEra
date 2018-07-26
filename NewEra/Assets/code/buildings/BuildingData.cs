using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BuildingBlocksNS;

namespace BuildingsNS
{
    [System.Serializable]
    public class BuildingData {
        public string name;
        public GameObject prefab;

      
        public List<BuildingAction> buildingActions;

        public BuildingData(BuildingBase buildingBase)
        {
            this.prefab = buildingBase.prefab;
            this.name = buildingBase.name;
            this.buildingActions = buildingBase.buildingActions;
        }
    }
}
