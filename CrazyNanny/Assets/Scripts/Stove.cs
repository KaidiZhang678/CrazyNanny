using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class Stove: BaseCounter, InterfaceProgress {   //for state change

    public event EventHandler<InterfaceProgress.OnProgressEventArgs> OnProgress;
    
    public class OnProgressEventArgs: EventArgs {
        public float progressNormalized;
    }


    private enum State {
        Idle,
        Frying, 
        Fried, 
        Burned
    }
    private State state;

    // SO array
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    private float fryingTimer = 0f, burningTimer = 0f;


    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        // no need to switch idle and burned state; 
        switch (state) {
            case State.Idle: 
                break;
            case State.Frying: 
                fryingTimer += Time.deltaTime; //can increase or decrease time

                OnProgress?.Invoke (this, new InterfaceProgress.OnProgressEventArgs {
                    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                });

                if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                    //Fried
                    GetKitchenObj().DestroySelf();
                    KitchenObject.TransferKitchenObj(fryingRecipeSO.output, this);

                    burningRecipeSO = InputBurnedRecipeSO(GetKitchenObj().GetKitchenObjSO());
                    if (transform.gameObject.name == "Washer") { state = State.Burned; }
                    else { state = State.Fried; }
                    burningTimer = 0f;
                }
                break;
            case State.Fried: 
                burningTimer += Time.deltaTime; //can increase or decrease time

                OnProgress?.Invoke (this, new InterfaceProgress.OnProgressEventArgs {
                    progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                });

                if (burningTimer > burningRecipeSO.burningTimerMax) {
                    //Fried
                    GetKitchenObj().DestroySelf();
                    KitchenObject.TransferKitchenObj(burningRecipeSO.output, this);

                    state = State.Burned;

                    OnProgress?.Invoke (this, new InterfaceProgress.OnProgressEventArgs {
                        progressNormalized = 0f
                    });

                }
                break;
            case State.Burned: 
                break;
        }
    }

    public override void Interact(Player player) {
        if (!HoldKitchenObj()) {
            //Player is carrying something and player carring something that can be fried
            if (player.HoldKitchenObj() && (InputFriedRecipeSO(player.GetKitchenObj().GetKitchenObjSO()) != null)) {
                player.GetKitchenObj().SetKitchenObjParent(this); //player drop it on the countner
                fryingRecipeSO = InputFriedRecipeSO(GetKitchenObj().GetKitchenObjSO()); //pan gets the object
                state = State.Frying;
                fryingTimer = 0f;

                OnProgress?.Invoke (this, new InterfaceProgress.OnProgressEventArgs {
                    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                });
                // Trigger the pickup animation on the player
                player.SetFetchAnimation(true);
            }
        } else if (player.HoldKitchenObj()) { //there is a Kitchenobject and player is carrying anything
            if (player.GetKitchenObj().TryGetPlate(out PlateKitchenObject plateKitchenObject) 
                && plateKitchenObject.AddMaterial(GetKitchenObj(). GetKitchenObjSO())) { //if player carrying a plate object
                GetKitchenObj().DestroySelf();
                state = State.Idle;
            }
        } else {
            GetKitchenObj().SetKitchenObjParent(player); //if not carrying, assign the object to the player pickup
            state = State.Idle; //stove back to Idle;
        }
    }

    private FryingRecipeSO InputFriedRecipeSO(KitchenObjectSO inputKitchenObjectSO) {
        return fryingRecipeSOArray.FirstOrDefault
        (fryingRecipeSO => fryingRecipeSO.input == inputKitchenObjectSO);
    }

    private BurningRecipeSO InputBurnedRecipeSO(KitchenObjectSO inputKitchenObjectSO) {
        return burningRecipeSOArray.FirstOrDefault
        (burningRecipeSO => burningRecipeSO.input == inputKitchenObjectSO);
    }
}
