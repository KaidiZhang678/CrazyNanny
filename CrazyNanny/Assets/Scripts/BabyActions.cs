using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Linq;
using UnityEngine.UI;



public class BabyActions : MonoBehaviour
{   
    private static BabyActions instance;

    private Animator babyanim;
    public float speed = 1f;

    private Transform playerPickUpHand;
    private Transform player;
    private Transform bed;


    public float placebedDistance;
    public float pickUpDistance;

    public bool itemIsPicked = false;

    // Baby Way Points
    public int currWaypoint = -1;
    public GameObject[] babywaypoints;
    //private Rigidbody rb;
    
    private float OnBedTime = 7000.0f;
    private float OnBedTimer = 0.0f;
    private bool OnBed = false;

    // Audio Sound Effects
    public AudioSource Cry;
    public AudioSource Run;
    public AudioSource coinSound;
    private float wanderingTime = 6000.0f;
    public float wanderingTimer = 0.0f;

    // Variables for randomly walking
    private NavMeshAgent NMagent;
    public float timeForNewPath;

    // Scoring Calculations for placing baby on bed
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI countText;
    private int babyPlacedTimes = 0;

    // Bonus Trigger 
    public int bonusTrigger = 1;

    private void Awake()
    {   
        instance = this;
    }

    public static BabyActions GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {   
        babyanim = GetComponent<Animator>();

        player = GameObject.Find("Player").transform;
        playerPickUpHand = GameObject.Find("PickUpHand").transform;
        bed = GameObject.Find("Bed").transform;

        if (babyanim == null)
            Debug.Log("Animator could not be found");

        NMagent = GetComponent<NavMeshAgent>();
        
        if (NMagent == null)
            Debug.Log("Nav Mesh Agent could not be found");
    }

    
    // Update is called once per frame
    void Update()
    {
        pickUpDistance = Vector3.Distance(player.position, transform.position);
        placebedDistance = Vector3.Distance(player.position, bed.position);
        //NMagent = GetComponent<NavMeshAgent>();
        int coinCount = int.Parse(coinText.text.Substring(2));
        int babyCount = int.Parse(countText.text.Substring(2));
        

        if (pickUpDistance <= 2) 
        {
            if (Input.GetKeyDown("space") && itemIsPicked == false)
            {   
                Run.Stop();
               // GameObject.Find("BabyPlaceholder").GetComponent<Rigidbody>().useGravity = false;
               // GameObject.Find("BabyPlaceholder").GetComponent<BoxCollider>().enabled =false;
                babyanim.SetFloat("Speed", 0f);
                NMagent.isStopped = true;
                NMagent.velocity = Vector3.zero;
                
                GameObject.Find("BabyObject").transform.position = playerPickUpHand.position;
                GameObject.Find("BabyObject").transform.rotation = Quaternion.Euler(0, 90, 0);
                GameObject.Find("BabyObject").transform.parent = GameObject.Find("PickUpHand").transform;

                itemIsPicked = true;
 
                
                
            }
        }
        
        if (Input.GetKeyDown("d") && itemIsPicked == true && placebedDistance <= 4) 
        {
            Cry.Stop();
            babyanim.SetFloat("Speed", 0f);

            NMagent.isStopped = true;
            NMagent.velocity = Vector3.zero;

            GameObject.Find("BabyObject").transform.parent = GameObject.Find("Baby").transform;
            //GameObject.Find("BabyPlaceholder").GetComponent<Rigidbody>().useGravity = true;
            //GameObject.Find("BabyPlaceholder").GetComponent<BoxCollider>().enabled = true;
            
            //NMAgent.Warp(new Vector3(11.23f, 0.55f, 16.55f));
            this.transform.position = new Vector3(0f, 0f, 0f);
            GameObject.Find("BabyObject").transform.position =  new Vector3(11.53f, 0.25f, 10.544f);
            GameObject.Find("BabyObject").transform.rotation = Quaternion.Euler(270, 0, 90);
            //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime);

            itemIsPicked = false;

            OnBed = true;
            wanderingTimer = 0f;

            // Calculate baby score
            babyPlacedTimes += 1;

            
            coinCount += 5 * bonusTrigger;
            babyCount = 0;
            coinSound.Play(); 
            
            countText.text = "x " + babyCount.ToString();
            coinText.text = "x " + coinCount.ToString();
            
            

        }

        if (OnBed == true) {
            OnBedTimer += 1f;
        }

        if (OnBedTimer == OnBedTime) {
            
            babyanim.SetFloat("Speed", 1.5f);

            GameObject.Find("BabyObject").transform.position =  new Vector3(0f, 0f, 0f);
            GameObject.Find("BabyObject").transform.rotation = Quaternion.Euler(0, 0, 0);
            this.transform.position = new Vector3(14.73f, 0.5f, 9.71f);

            OnBed = false;
            OnBedTimer = 0f;

            NMagent.isStopped = false;
            
        }

        
        if (OnBed == false) {
            wanderingTimer += 1f;
        }

        if (wanderingTimer == wanderingTime) {
            Cry.Play();
        }
        
        
        
        if (OnBed == false && itemIsPicked == false && NMagent.isStopped == false) 
        {   
            babyCount = 1;
            countText.text = "x " + babyCount.ToString();

            if (!NMagent.pathPending) 
            {   
                
                if (NMagent.remainingDistance == 0)
                {   
                    
                    // Run.Play();
                    //setNextWaypoint();
                    babyanim.SetFloat("Speed", 1.5f);
                    NMagent.SetDestination(getRandPos());
                }
            }
        }
        
    }

    // Get random position
    private Vector3 getRandPos()
    {
        float x = Random.Range(13.27f, 19.11f);
        float z = Random.Range(6.89f, 13.69f);

        Vector3 pos = new Vector3(x, 0.5f, z);
        return pos;
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
    
    
}
