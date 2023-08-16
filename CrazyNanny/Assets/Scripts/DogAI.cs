using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DogAI : MonoBehaviour
{
    public float speed;
    public float hunger = 1;
    public Animator animator;
    public AudioClip hungryBark;
    public AudioClip feedingSound;
    public Slider hungerBar;
    public float interactionDistance = 2f;
    public Transform player;
    public float hungerDecreaseRate = 0.1f;
    public AudioSource audioSource;
    public TextMeshProUGUI coinText;
    public AudioSource coinSound;
    public TextMeshProUGUI dogCountText;

    private Vector3 nextDestination;
    private bool isWalking = false;
    private float idleTimer = 5f;
    private float walkTimer = 2f;

    void Start()
    {
        nextDestination = transform.position;
        StartCoroutine(ManageHunger());
        animator.SetBool("DogWalk", false);

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        hungerBar.value = hunger / 100;
        dogCountText.text = "x " + ((int)System.Math.Round((98 - hunger) / 20)).ToString();

        if (hunger < 30 && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(hungryBark);
        }


        if (isWalking)
        {
            walkTimer -= Time.deltaTime;
            if (walkTimer <= 0f)
            {
                isWalking = false;
                animator.SetBool("DogWalk", false);
                idleTimer = 5f;
            }
            else
            {
                LookAtTarget(nextDestination);
                transform.position = Vector3.MoveTowards(transform.position, nextDestination, speed * Time.deltaTime);
            }
        }
        else
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
            {
                isWalking = true;
                animator.SetBool("DogWalk", true);
                nextDestination = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-5, 5));
                walkTimer = 2f;
                idleTimer = 0f; 
            }
        }

        if (Vector3.Distance(player.position, transform.position) <= interactionDistance && Input.GetKeyDown(KeyCode.D))
        {
            if (hunger < 90f)
            {
                // update coin
                string currentText = coinText.text;
                int count = int.Parse(currentText.Substring(2));
                count += 1;
                coinText.text = "x " + count.ToString();
                coinSound.Play();
            }
            hunger = Mathf.Min(hunger + 20f, 100f);
            audioSource.PlayOneShot(feedingSound);
        }
    }

    void LookAtTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
    }

    IEnumerator ManageHunger()
    {
        while (true)
        {
            hunger -= hungerDecreaseRate * Time.deltaTime;
            hunger = Mathf.Clamp(hunger, 0, 100);
            // Debug.Log("Hunger: " + hunger.ToString());
            yield return null;
        }
    }
}