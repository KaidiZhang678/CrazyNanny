using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject: MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private InterfaceKitchenObj interfaceKitchenObj;

    public KitchenObjectSO GetKitchenObjSO(){
        return kitchenObjectSO;
    }

    // to let the object know where it is
    public void SetKitchenObjParent(InterfaceKitchenObj KitchenObjectParent) {
        if (interfaceKitchenObj != null) { //this is first counter, clear it
            interfaceKitchenObj.ClearKitchenObj();
        }

        interfaceKitchenObj = KitchenObjectParent; // this is the new one

        interfaceKitchenObj.SetKitchenObj(this);

        transform.SetParent(interfaceKitchenObj.MoveKitchenObj(), false);
        transform.localPosition = Vector3.zero;
    }
    
    //for dispose
    public void DestroySelf() {
        interfaceKitchenObj.ClearKitchenObj();
        Destroy (gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        plateKitchenObject = this as PlateKitchenObject;
        return plateKitchenObject != null;
    }

    public static KitchenObject TransferKitchenObj(KitchenObjectSO kitchenObjectSO, InterfaceKitchenObj interfaceKitchenObj) {
        KitchenObject kitchenObject = Instantiate(kitchenObjectSO.prefab).GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjParent(interfaceKitchenObj);
        return kitchenObject;
    }
}
