using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;



public class BurgerManager : MonoBehaviour
{
    public static BurgerManager Instance {get; private set;} // for referenced by takeout bag
    [SerializeField] private List<KitchenObjectSO> kitchenObjectSOList;
    [SerializeField] public int requestedBurgerNo;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI countText;
    // public GameObject winTextObject;
    public AudioSource coinSound;
    private int burgerMade = 0;


    private void Awake() {
        SetCountText();
        // winTextObject.SetActive(false);
        Instance = this; // for referenced by takeout bag
    }
    

    void SetCountText() {
        // update coin
        string currentText = coinText.text;
        Debug.Log(currentText);
        Debug.Log(currentText.Substring(2));
        int count = int.Parse(currentText.Substring(2));
        
        // don't increase coin when game starts
        if (!(burgerMade == 0))
        {
            count += 50;
        }
        
        coinText.text = "x " + count.ToString();

        if (burgerMade != 0){ coinSound.Play(); }
        countText.text = "x " + requestedBurgerNo.ToString();
        if (requestedBurgerNo == 0 ) {
            //winTextObject.SetActive(true);
            Debug.Log ("All requested burgers are done!");
        }
    }


    //check if the recipe meets the requested. 
    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject) {

        Debug.Log(kitchenObjectSOList.Count);
        //first check if the elements numbers meet
        if (kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
            bool plateRecipeMatch = kitchenObjectSOList.All (recipeKitchenObjectSO =>
                plateKitchenObject.GetKitchenObjectSOList().Any(plateKitchenObjectSO =>
                    plateKitchenObjectSO == recipeKitchenObjectSO
                )
            );

            if (plateRecipeMatch) {
                Debug.Log("correct recipe");
                requestedBurgerNo -= 1;
                burgerMade += 1;
                SetCountText();
                return;
            }
        }
        // No match found, player did not deliver the correct recipe
        Debug.Log ("wrong recipe");
    }
}
