using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject //extend kitchenobject class
{   

    public event EventHandler<MaterialAddedArgs> MaterialAdded;
    public class MaterialAddedArgs: EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList= new List<KitchenObjectSO>(), 
                            kitchenObjectSOList= new List<KitchenObjectSO>(); //only slices on the plate

    public bool AddMaterial(KitchenObjectSO kitchenObjectSO) {
        // Not a valid kitchen object or already has this type
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO) || kitchenObjectSOList.Contains(kitchenObjectSO))
            return false;
        
        // Otherwise, add object on the plate
        kitchenObjectSOList.Add(kitchenObjectSO);
        if (MaterialAdded != null) {
            MaterialAdded.Invoke(this, new MaterialAddedArgs { kitchenObjectSO = kitchenObjectSO });
        }
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }
}
