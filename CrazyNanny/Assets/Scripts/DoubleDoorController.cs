using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenCloseDoorSpaceKey : MonoBehaviour
{
    public Animator leftDoor;
    public Animator rightDoor;
    public bool open;
    [SerializeField] private int rayCount = 20; // Number of rays to cast
    [SerializeField] private float raySpacing = 0.2f; // Spacing between each ray
    [SerializeField] private float raycastDistance = 3f;
    [SerializeField] private float interval = 6f;
    [SerializeField] private float rayShiftAmountZ = 0.0f;
    private bool playerDetected = false;

    // get the player object
    public GameObject player;
    // get the KeyTrashPlayerController class
    public KeyTrashPlayerController keyTrashPlayerController;


    // Start is called before the first frame update
    void Start()
    {
        open = false;
        InvokeRepeating("CloseDoor", 0f, interval);

        // get the keyTrashPlayerController when game starts:
        keyTrashPlayerController = player.GetComponent<KeyTrashPlayerController>();
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

            // Cast a ray in the specified direction
            RaycastHit hitForward;
            RaycastHit hitBackward;

            bool forward = Physics.Raycast(rayPosition, rayDirectionForward, out hitForward, raycastDistance);
            bool backward = Physics.Raycast(rayPosition, rayDirectionBackward, out hitBackward, raycastDistance);
            //Debug.DrawRay(rayPosition, rayDirectionForward * raycastDistance, Color.red);
            //Debug.DrawRay(rayPosition, rayDirectionBackward * raycastDistance, Color.blue);


            if (PlayerDetected(forward, backward, hitForward, hitBackward))
            {
                // Debug.Log("Raycast Detected Player!");
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
            // Debug.Log("Space key pressed, opening door");
            leftDoor.Play("Opening", 0, 0.0f);
            rightDoor.Play("Opening1", 0, 0.0f);
            open = true;
        }
    }

    private void CloseDoor()
    {
        if (!playerDetected)
        {
            if (open)
            {
                leftDoor.Play("Closing", 0, 0.0f);
                rightDoor.Play("Closing1", 0, 0.0f);
                open = false;
            }
        }


    }

    private bool PlayerDetected(bool forward, bool backward, RaycastHit hitForward, RaycastHit hitBackward)
    {
        //Debug.Log("Player Detected method called");
        if (forward)
        {
            // Debug.Log("detected in forward direction");
            // Debug.Log(hitForward.collider.gameObject.name);
            if (hitForward.collider.CompareTag("Player"))
            {
                // get the layer name of the collided object (glass door)
                string currDoorLayerName = LayerMask.LayerToName(gameObject.layer);
                // see if the player has the correct key to open this door:
                if (currDoorLayerName == "BlackDoor" && keyTrashPlayerController.hasBlackKey) {
                    Debug.Log("Forward collider is player! Open the black door!!!");
                    return true;
                }
                else if (currDoorLayerName == "BlueDoor" && keyTrashPlayerController.hasBlueKey) {
                    Debug.Log("Forward collider is player! Open the blue door!!!");
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        if (backward)
        {
            // Debug.Log("detected in backward direction");
            if (hitBackward.collider.CompareTag("Player"))
            {
                // Debug.Log("Backward collider is player");
                // return true;

                // get the layer name of the collided object (glass door)
                string currDoorLayerName = LayerMask.LayerToName(gameObject.layer);
                // see if the player has the correct key to open this door:
                if (currDoorLayerName == "BlackDoor" && keyTrashPlayerController.hasBlackKey) {
                    Debug.Log("Forward collider is player! Open the black door!!!");
                    return true;
                }
                else if (currDoorLayerName == "BlueDoor" && keyTrashPlayerController.hasBlueKey) {
                    Debug.Log("Forward collider is player! Open the blue door!!!");
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        return false;
    }

    void OnDestroy()
    {
        CancelInvoke("CloseDoor"); // Cancel the repeating invocation when the script is destroyed
    }
}
