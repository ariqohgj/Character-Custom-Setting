using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyBuildSystem.Features.Runtime.Buildings.Manager;
//using EasyBuildSystem.Features.Runtime.Buildings.Part;

public class ModeChanger : MonoBehaviour
{
    [SerializeField] BuildingManager buildingManager;
    [SerializeField] GameObject[] ConstructionModeObj;
    [SerializeField] GameObject[] FurnitureModeObj;
    private bool isFurnitureMode;

    [ContextMenu("Change Mode")]
    public void ChangeMode()
    {
        BuildingManager.Instance.BuildingPartReferences.Clear();
        if (isFurnitureMode)
        {
            for (int i = 0; i < ConstructionModeObj.Length; i++)
            {
                ConstructionModeObj[i].SetActive(false);
            }
            for (int i = 0; i < FurnitureModeObj.Length; i++)
            {
                FurnitureModeObj[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < FurnitureModeObj.Length; i++)
            {
                FurnitureModeObj[i].SetActive(false);
            }
            for (int i = 0; i < ConstructionModeObj.Length; i++)
            {
                ConstructionModeObj[i].SetActive(true);
            }
        }
    }
}
