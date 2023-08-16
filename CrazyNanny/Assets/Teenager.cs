using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;

public class Teenager : MonoBehaviour
{      
    private static Teenager instance;

    public Animator teenanim;
    public NavMeshAgent NMagent;
    public int currWaypoint = -1;
    public GameObject[] babywaypoints;

    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    private DateTime startTime;

    public TextMeshProUGUI coinText;
    // public TextMeshProUGUI countText;

    public AudioSource coinSound;    

    public GameObject babydoorwaypoint;
    public bool TakeCareBabyFlag = false;

    public AudioSource SneezeSound;
    
    private void Awake()
    {   
        instance = this;
        playerInRange = false;
        visualCue.SetActive(false);
    }

    public static Teenager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        teenanim = GetComponent<Animator>();
        

        if (teenanim == null)
            Debug.Log("Animator could not be found");

        NMagent = GetComponent<NavMeshAgent>();
        
        if (NMagent == null)
            Debug.Log("Nav Mesh Agent could not be found");
    }

    public void resetTeenagerWalking()
    {
        NMagent.isStopped = false;
        teenanim.SetTrigger("MakeWalk");
    }

    // Update is called once per frame
    void Update()
    {   

        int coinCount = int.Parse(coinText.text.Substring(2));
        // int teenCount = int.Parse(countText.text.Substring(2));

        

        //if (Input.GetKeyDown("d"))
        //{
        //    NMagent.isStopped = false;
        //    teenanim.SetTrigger("MakeWalk");
        //}


        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {   

            visualCue.SetActive(true);

            if (Input.GetKeyDown("space"))
            {   
                NMagent.isStopped = true;
                teenanim.SetTrigger("MakeIdle");

                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);

            }

        }
        else
        {   
            // NMagent.isStopped = false;
            visualCue.SetActive(false);
            
        }

        // Heading to the baby room
        if (TakeCareBabyFlag == true && this.transform.position.x != babydoorwaypoint.transform.position.x && this.transform.position.z != babydoorwaypoint.transform.position.z)
        {   
            NMagent.isStopped = false;
            teenanim.SetTrigger("MakeWalk");
            NMagent.SetDestination(babydoorwaypoint.transform.position);
            BabyActions.GetInstance().bonusTrigger = 2; 
            
        }

        if (TakeCareBabyFlag == true && this.transform.position.x == babydoorwaypoint.transform.position.x && this.transform.position.z == babydoorwaypoint.transform.position.z) 
        {   
            NMagent.isStopped = true;
            startTime = DateTime.Now;
            teenanim.SetTrigger("MakeSneeze");
            SneezeSound.Play();
            TakeCareBabyFlag = false;
            
        }

        if (TakeCareBabyFlag == false && ((float)(DateTime.Now-startTime).TotalSeconds) > 2f && this.transform.position.x == babydoorwaypoint.transform.position.x && this.transform.position.z == babydoorwaypoint.transform.position.z) 
        {   
            NMagent.isStopped = false;
            teenanim.SetTrigger("MakeWalk");
        }
        
        
        if (TakeCareBabyFlag == false && NMagent.isStopped == false)
        {
            if (!NMagent.pathPending) 
            {
                if (NMagent.remainingDistance == 0)
                {
                    setNextWaypoint();
                    
                }
            }
        }
    }

    private void setNextWaypoint()
    {   
        int arrLength = babywaypoints.Length;

        if (arrLength == 0)
        {
            Debug.Log("Waypoints array's length is equal to zero.");
        }

        else if (currWaypoint >= arrLength  - 1) 
        {   
           
            currWaypoint = 0;
            NMagent.SetDestination(babywaypoints[currWaypoint].transform.position);
        }

        else
        {
            currWaypoint += 1;
            NMagent.SetDestination(babywaypoints[currWaypoint].transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {   
        
        if (other.gameObject.tag == "Player")
        {   
            Debug.Log("Enter the teenagertrigger");
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