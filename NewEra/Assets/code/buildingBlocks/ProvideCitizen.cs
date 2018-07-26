using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BuildingsNS;

namespace BuildingBlocksNS {
    [CreateAssetMenu(fileName = "new ProvideCitizen", menuName = "BuildingBlocks/New ProvideCitizen")]
    public class ProvideCitizen : BuildingBlock
    {

        public int cit_neutral = 0;
        public int cit_worker = 0;
        public int cit_scolar = 0;
        public int cit_military = 0;
        public int cit_merchant = 0;


        public override List<DataObject> Solve(List<DataObject> dataArray)
        {
            DataObject dataObjectLastBuilding = BuildingBlockChain.GetLastObjectOfTypeFromDataArray(dataArray, DataObject.ObjectTypes.Building);
            Building building = (Building)dataObjectLastBuilding.objectRef;

            building.Cit_neutral += cit_neutral;
            building.Cit_worker += cit_worker;
            building.Cit_scolar += cit_scolar;
            building.Cit_military += cit_military;
            building.Cit_merchant += cit_merchant;


            return dataArray;
        }

    }
}
