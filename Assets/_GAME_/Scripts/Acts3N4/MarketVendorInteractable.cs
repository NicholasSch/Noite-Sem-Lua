using UnityEngine;

public class MarketVendorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private MarketDay2Manager marketDay2Manager;
    [SerializeField] private MarketDay2Manager.VendorType vendorType;

    public void Interact()
    {
        marketDay2Manager.InteractWithVendor(vendorType);
    }
}