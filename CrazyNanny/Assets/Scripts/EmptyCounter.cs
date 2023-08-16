using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//extend this interface, for interface, can extend as many as we can
//inheritance basecounter
public class EmptyCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; //update tomatoPrefab with self-defined KitchenObjectSO class

    public override void Interact(Player player)
    {
        switch (HoldKitchenObj())
        {
            case false:
                if (player.HoldKitchenObj()) {
                    // Player is carrying something
                    player.SetFetchAnimation(true);
                    player.GetKitchenObj().SetKitchenObjParent(this);
                }
                break;

            case true:
                switch (player.HoldKitchenObj()) {
                    case true:
                        // Player holding the plate and stuff is on the counter to put on the plate
                        if ((player.GetKitchenObj().TryGetPlate(out PlateKitchenObject plateKitchenObject)) && 
                                (plateKitchenObject.AddMaterial(GetKitchenObj().GetKitchenObjSO()))) { 
                            player.SetFetchAnimation(true);
                            GetKitchenObj().DestroySelf();
                        }
                        // Plate on the counter and player is holding stuff to put on the plate
                        else if ( (GetKitchenObj().TryGetPlate(out plateKitchenObject)) && 
                                (plateKitchenObject.AddMaterial(player.GetKitchenObj().GetKitchenObjSO())) ) { 
                            player.SetFetchAnimation(true);
                            player.GetKitchenObj().DestroySelf();
                        }
                        break;
                    case false:
                        player.SetFetchAnimation(true);
                        GetKitchenObj().SetKitchenObjParent(player);
                        break;
                }
                break;
        }
    }

}
