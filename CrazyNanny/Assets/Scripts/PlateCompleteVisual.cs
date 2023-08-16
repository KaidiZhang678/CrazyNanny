using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{   
    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;
    
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private void Start() {
        plateKitchenObject.MaterialAdded += (sender, e) =>
        {   //add each element
            KitchenObjectSO targetKitchenObjSO = e.kitchenObjectSO;
            var kitchenObjectElement = kitchenObjectSOGameObjectList.FirstOrDefault(kitchenObj => kitchenObj.kitchenObjectSO == targetKitchenObjSO);
            if (kitchenObjectElement.kitchenObjectSO != null) {
                kitchenObjectElement.gameObject.SetActive(true);
            }
        }; 
        //plate all set!
        kitchenObjectSOGameObjectList.ForEach(kitchenObjectElement => kitchenObjectElement.gameObject.SetActive(false));
    }
}
