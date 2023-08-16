using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeoutBag : BaseCounter
{
    public override void Interact (Player player) {
        if ( (player.HoldKitchenObj()) && (player.GetKitchenObj().TryGetPlate(out PlateKitchenObject plateKitchenObject)) ) {

            BurgerManager.Instance.DeliveryRecipe (plateKitchenObject); // referenced from instance of burgermanager before destroy
            player.GetKitchenObj().DestroySelf();
        }
    }
}
