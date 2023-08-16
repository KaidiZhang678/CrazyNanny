using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothCleanedWard : BaseCounter {

    public Animator closetAnimator;
    public override void Interact (Player player) {
        if (player.HoldKitchenObj()) {
            if (player.GetKitchenObj().CompareTag("CleanCloth")) {
                ClothManager.Instance.DeliveryCloth();
                player.GetKitchenObj().DestroySelf();
                // Trigger the "CleanDressClosetOpenClose" animation by setting the "openclose" trigger
                closetAnimator.SetTrigger("OpenClose");
            }
        }
    }

}
