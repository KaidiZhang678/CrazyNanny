using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{   
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    public Text coinText;
    public TextMeshProUGUI countText;

    public AudioSource coinSound;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        int coinCount = int.Parse(coinText.text.Substring(2));
        int teenCount = int.Parse(countText.text.Substring(2));

        if (playerInRange)
        {   
            visualCue.SetActive(true);
            if (Input.GetKeyDown("space"))
            {   
                //Debug.Log(inkJSON.text);
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                
                coinCount += 1;
                teenCount += 1;
                coinSound.Play(); 
                
                countText.text = "x " + teenCount.ToString();
                coinText.text = "x " + coinCount.ToString();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {   
        Debug.Log("Enter the teenagertrigger");
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
