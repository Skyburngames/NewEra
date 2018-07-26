using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace BuildingsNS
{
    public class BuildingActionButton : MonoBehaviour
    {
        [Header("References")]
        public Text buttonText;


        private BuildingAction buildingAction;

        public BuildingAction BuildingAction
        {
            get
            {
                return buildingAction;
            }

            set
            {
                buildingAction = value;
                Refresh();
            }
        }



        public void ClickOnButton()
        {
            Debug.Log("Click on button!!!! @#@#!@#");
            buildingAction.baseBuildingBlockChain.StartChain();
        }


        private void Refresh()
        {
            //set the text
            string result = "";
            result += buildingAction.name; //TODO: add citizen cost
            buttonText.text = result;
        }


    }
}