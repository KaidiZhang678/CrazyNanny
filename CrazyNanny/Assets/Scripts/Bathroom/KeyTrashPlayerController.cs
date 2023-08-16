using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyTrashPlayerController : MonoBehaviour
{
    public float pickUpRange = 1f; // Adjust according to your needs
    private Animator animator;
    public bool hasBlueKey = false;
    public bool hasBlackKey = false;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI keyCountText;
    public TextMeshProUGUI trashCountText;
    // public GameObject winTextObject;
    public AudioSource coinSound;
    private int keysRemaining = 2;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsPlayerIdle())
        {
            PickUp();
        }
        else {
            animator.SetBool("isPickingUp", false);
        }
    }

    private void PickUp()
    {
        // Find all Colliders that the player can pick up within range
        Collider[] itemsToPickUp = Physics.OverlapSphere(transform.position, pickUpRange);
        foreach (Collider item in itemsToPickUp)
        {
            // Check if the Collider is a "Pick Up Item"
            if (item.gameObject.CompareTag("Key") || item.gameObject.CompareTag("Trash"))
            {
                animator.SetBool("isPickingUp", true);
                string objectLayerName = LayerMask.LayerToName(item.gameObject.layer);
                if (objectLayerName == "BlueKey") {
                    hasBlueKey = true;
                    keysRemaining--;
                    keyCountText.text = "x "+keysRemaining.ToString();
                }
                else if (objectLayerName == "BlackKey") {
                    hasBlackKey = true;
                    keysRemaining--;
                    keyCountText.text = "x " + keysRemaining.ToString();
                } else if (item.gameObject.CompareTag("Trash"))
                {
                    // update coin
                    string currentText = coinText.text;
                    int count = int.Parse(currentText.Substring(2));

                    coinText.text = "x " + (count+1).ToString();
                    coinSound.Play();


                    GameObject trashesGenerator = GameObject.Find("TrashesGenerator");
                    TrashesGenerator tg = trashesGenerator.GetComponent<TrashesGenerator>();
                    tg.currentTrashCount--;
                    tg.trashCleanedCount++;
                    trashCountText.text = "x " + (tg.currentTrashCount).ToString();
                    
                    Debug.Log("You cleaned " + tg.trashCleanedCount + " trashes! There's still " 
                    + tg.currentTrashCount + " trashes. ");
                }
                Destroy(item.gameObject);                
            }
        }
    }

    private bool IsPlayerIdle() {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }

    // private void DecrementTrashCount(GameObject gameObject) 
    // {
    //     Debug.Log("You just destroyed a trashes! --1 triggered~~");
    //     currentTrashCount--;
    //     trashCleanedCount++;
    //     existingTrashCount = maxTrashCount - currentTrashCount;
    //     trashCountText.text = "x " + (existingTrashCount).ToString();
        
    // }
}
