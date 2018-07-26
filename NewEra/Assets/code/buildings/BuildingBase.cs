using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BuildingBlocksNS;

namespace BuildingsNS
{
    [CreateAssetMenu(fileName = "new baseBuilding", menuName = "Buildings/New Building")]
    public class BuildingBase : ScriptableObject
    {
        public new string name = "new building";
        public GameObject prefab;

        [Tooltip("Each action is a option shown when the user mouseOver")]
        public List<BuildingAction> buildingActions;

        public static BuildingData CreateBuilding(BuildingBase buildingBase)
        {
            BuildingData nwCardData = new BuildingData(buildingBase);
            return nwCardData;
        }

        public static Building CreateBuilding(BuildingData buildingData)
        {
            GameObject buildingGO = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Building"));
            Building building = buildingGO.GetComponent<Building>();
            building.BuildingData = buildingData;

            return building;
        }
    }
}

[System.Serializable]
public class BuildingAction
{
    public string name;

    public bool showAsButton = true;

    [Tooltip("The card will solve these events in order.")]
    public BuildingBlockChain baseBuildingBlockChain;
}