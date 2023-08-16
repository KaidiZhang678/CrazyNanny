using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, InterfaceKitchenObj 
{
    [SerializeField] private Transform CounterTop;

    private KitchenObject kitchenObject;
    public virtual void Interact (Player player) {
    }

    // interface is implemented here
    public Transform MoveKitchenObj() {
        return CounterTop;
    }

    public void SetKitchenObj (KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObj() {
        return kitchenObject;
    }

    public void ClearKitchenObj() {
        kitchenObject = null;
    }

    public bool HoldKitchenObj() {
        return kitchenObject != null;
    }
    
}
