using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//inheritance basecounter
public class Container: BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; //update with self-defined KitchenObjectSO class
    public event EventHandler GrabObj;

    public override void Interact(Player player) {
        //let counter know what is on it
        if (!player.HoldKitchenObj()) {//Player is not carrying anything
            KitchenObject.TransferKitchenObj(kitchenObjectSO, player);
            //trigger the animator in CounterAnimation.cs
            GrabObj?.Invoke(this, EventArgs.Empty);

            // Trigger the character's pickup animation here
            player.SetFetchAnimation(true);
        }
    }
}
