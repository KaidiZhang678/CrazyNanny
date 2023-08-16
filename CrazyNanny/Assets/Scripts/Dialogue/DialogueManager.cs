using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Linq;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{   
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    private Story currentStory;

    private static DialogueManager  instance;
    
    public bool dialogueIsPlaying;

    public TextMeshProUGUI coinText;
    // public TextMeshProUGUI countText;

    public AudioSource coinSound;

    public GameObject babydoorwaypoint;

    private void Awake()
    {   
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        int coinCount = int.Parse(coinText.text.Substring(2));
        // int teenCount = int.Parse(countText.text.Substring(2));

        if (!dialogueIsPlaying)
        {
            return;
        }

        if (Input.GetKeyDown("d"))
        {
            ContinueStory();
        }

        if (Input.GetKeyDown("1"))
        {   
            BabyActions.GetInstance().Cry.Stop();
            BabyActions.GetInstance().wanderingTimer = 3000.0f;
            ExitDialogueMode();

            Teenager.GetInstance().TakeCareBabyFlag = true;
            /*
            Teenager.GetInstance().NMagent.isStopped = false;
            Teenager.GetInstance().teenanim.SetTrigger("MakeWalk");
            Teenager.GetInstance().NMagent.SetDestination(babydoorwaypoint.transform.position);
            Teenager.GetInstance().teenanim.SetTrigger("MakeSneeze");
            */
        }

        if (Input.GetKeyDown("2"))
        {

            coinCount += 1;
            // teenCount += 1;
            coinSound.Play(); 
            
            // countText.text = "x " + teenCount.ToString();
            coinText.text = "x " + coinCount.ToString();
            ExitDialogueMode();
            Teenager.GetInstance().resetTeenagerWalking();
        }

        if (Input.GetKeyDown("3"))
        {
            ExitDialogueMode();
            Teenager.GetInstance().resetTeenagerWalking();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        Player.GetInstance().stop();
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

    }

    private void  ContinueStory()
    {   

        if(currentStory.canContinue)
        {   

            dialogueText.text = currentStory.Continue();

        }
        else
        {
            ExitDialogueMode();
            Teenager.GetInstance().resetTeenagerWalking();
        }
    }
    
}