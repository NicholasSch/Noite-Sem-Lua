using System.Collections;
using UnityEngine;

public class Act5ChestInteractable : MonoBehaviour, IInteractable
{   
    [Header("Dependencies")]

    [SerializeField] private HouseNight2Manager houseNight2Manager;



    

    public void Interact()
    {
        if (ProgressionManager.Instance.act5TobaccoFound)
            return;

        houseNight2Manager.PlayChestOpeningSequence();
    }

}