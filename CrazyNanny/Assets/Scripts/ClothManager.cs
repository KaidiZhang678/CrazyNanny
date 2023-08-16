using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;



public class ClothManager : MonoBehaviour
{
    public static ClothManager Instance {get; private set;} // for referenced by takeout bag
    [SerializeField] public int requestedClothNo;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI countText;
    // public GameObject winTextObject;
    public AudioSource coinSound;
    private int clothCleaned = 0;


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
        if (!(clothCleaned == 0))
        {
            count += 15;
        }
        
        coinText.text = "x " + count.ToString();

        if (clothCleaned != 0){ coinSound.Play(); }
        countText.text = "x " + requestedClothNo.ToString();
        if (requestedClothNo == 0 ) {
            //winTextObject.SetActive(true);
            Debug.Log ("All requested Clothes are cleaned!");
        }
    }


    //check if the recipe meets the requested. 
    public void DeliveryCloth () {
        Debug.Log("Cleaned Cloth");
        requestedClothNo -= 1;
        clothCleaned += 1;
        SetCountText();
        return;
    }
}
