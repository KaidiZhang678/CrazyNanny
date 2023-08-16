using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoorController : MonoBehaviour
{
    public Animator door;
    public bool open;
    [SerializeField] private int rayCount = 20; // Number of rays to cast
    [SerializeField] private float raySpacing = 0.2f; // Spacing between each ray
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private float interval = 6f;
    [SerializeField] private float rayShiftAmountZ = 0.0f;
    private bool playerDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        InvokeRepeating("CloseDoor", 0f, interval);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the starting direction for the first ray
        Vector3 startDirectionForward = transform.forward - (transform.right * (raySpacing * (rayCount - 1) / 2f));
        Vector3 startDirectionBackward = -transform.forward + (transform.right * (raySpacing * (rayCount - 1) / 2f));
        Vector3 currentPosition = transform.position;
        currentPosition.z -= rayShiftAmountZ;
        Vector3 rayPosition = currentPosition;

        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the direction for each ray
            Vector3 rayDirectionForward = startDirectionForward + (transform.right * (raySpacing * i));
            Vector3 rayDirectionBackward = startDirectionBackward - (transform.right * (raySpacing * i));

            RaycastHit[] hitsForward = Physics.RaycastAll(rayPosition, rayDirectionForward, raycastDistance);
            RaycastHit[] hitsBackward = Physics.RaycastAll(rayPosition, rayDirectionBackward, raycastDistance);

            if (PlayerDetected(hitsForward, hitsBackward))
            {
                playerDetected = true;
                OpenDoor();
            }
            else
            {
                playerDetected = false;
            }
        }
    }

    private void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            door.Play("Opening1", 0, 0.0f);
            open = true;
        }
    }

    private void CloseDoor()
    {
        if (!playerDetected)
        {
            if (open)
            {
                door.Play("Closing1", 0, 0.0f);
                open = false;
            }
        }


    }

    private bool PlayerDetected(RaycastHit[] hitsForward, RaycastHit[] hitsBackward)
    {
        // Loop through all the raycast hits
        for (int i = 0; i < hitsForward.Length; i++)
        {
            RaycastHit hit = hitsForward[i];
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        for (int i = 0; i < hitsBackward.Length; i++)
        {
            RaycastHit hit = hitsBackward[i];
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    void OnDestroy()
    {
        CancelInvoke("CloseDoor"); // Cancel the repeating invocation when the script is destroyed
    }
}

