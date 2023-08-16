using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfaceKitchenObj
{//not include any implementation in the interface 
    public Transform MoveKitchenObj();

    public void SetKitchenObj (KitchenObject kitchenObject);

    public KitchenObject GetKitchenObj();

    public void ClearKitchenObj();

    public bool HoldKitchenObj();

}
