using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, InterfaceKitchenObj
{
    [SerializeField]public float moveSpeed = 8f;
    [SerializeField]private GameInput gameInput;
    private Vector3 lastDirection;
    private KitchenObject kitchenObject;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    public float interactionRange = 20f;
    Animator animator;

    //////////// NEW //////////////
    private GameObject nearbyTrash = null;
    //////////// NEW //////////////
    private static Player instance;

    public static Player GetInstance()
    {
        return instance;
    }

    private void Start() {
        instance = this;
        gameInput.Interaction += (sender, e) => PlayerInteract();
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking", false);
        animator.SetBool("isPickingUp", false); 
        animator.SetBool("isFetching", false); 
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    if (lastDirection != null)
    //    {
    //        Gizmos.DrawRay(transform.position, lastDirection * 20);
    //    }
    //}

    private void PlayerInteract() {
        
        // if (!GameWorker.Instance.IsGamePlaying()) {return;}

        Vector3 moveDir = GetNormalizedMove();

        Vector3 newMoveDir = transform.forward * moveDir.z;

        //check if the char is touching an object
        //lastDirection = newMoveDir != Vector3.zero ? newMoveDir : lastDirection;
        lastDirection = transform.forward;

        // Debug.Log("lastDirection" + lastDirection);
        // Debug.Log("moveDirection" + newMoveDir);
        // Debug.Log("starting position of raycast" + transform.position);
        // Debug.Log("direction of raycast" + lastDirection);
        

        // interactDistance 2f ;
        // Perform a raycast to check for a collision in the specified direction
        if (Physics.Raycast(transform.position, lastDirection, out RaycastHit raycastHit, 2f))
        {   
            // Debug.Log("Hit or not " + Physics.Raycast(transform.position, lastDirection, out raycastHit, 2f));
            // Check if the collided object has a component of type BaseCounter
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                // Debug.Log("BaseCount or not" + raycastHit.transform.TryGetComponent(out clearCounter));
                clearCounter.Interact(this);
            }
        }

        //////////// NEW //////////////
        if (nearbyTrash && Input.GetKeyDown(KeyCode.Space)) {
            Destroy(nearbyTrash);
            nearbyTrash = null;
        }
        //////////// NEW //////////////
    }


    //////////// NEW //////////////
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Trash")) {
            Debug.Log("Player entered trashed here");
            nearbyTrash = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject == nearbyTrash) {
            Debug.Log("Player entered trashed there");
            nearbyTrash = null;
        }
    }
    //////////// NEW //////////////



    public void SetPickupAnimation(bool value) {
        if (animator != null) {
            animator.SetBool("isPickingUp", value);
            // Reset the pickup animation after a short duration
            StartCoroutine(ResetPickupAnimation());
        }
    }

    private IEnumerator ResetPickupAnimation() {
        // Wait for a short duration (e.g., 1 second) to reset the pickup animation
        yield return new WaitForSeconds(1f);
        // Reset the pickup animation back to false after the duration
        animator.SetBool("isPickingUp", false);
    }


    public void SetFetchAnimation(bool value) {
        if (animator != null) {
            animator.SetBool("isFetching", value);
            // Reset the pickup animation after a short duration
            StartCoroutine(ResetFetchAnimation());
        }
    }

    private IEnumerator ResetFetchAnimation() {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isFetching", false);
    }

    public void stop()
    {
        animator.SetBool("isWalking", false);
    }

    public void walk()
    {
        animator.SetBool("isWalking", true);
    }

    private void Update() {
        /*1. for movement*/
        Vector3 moveDir = GetNormalizedMove();
        //set collision detection for movement
        // playerRadius 0.7, playerHeight 5, detect the distance: raycast or xxcast only return a boolean; moveDistance is the last parameter; 
        // bool canMove = !Physics.CapsuleCast (transform.position, transform.position+Vector3.up * 5f, 0.7f, moveDir, moveSpeed * Time.deltaTime);

        Vector3 newMoveDir = transform.forward * moveDir.z;
        RaycastHit hitStair;
        if (Physics.Raycast(transform.position + Vector3.up * 2f, -transform.up, out hitStair, 5f))
        {
            //Debug.Log(hitStair.collider.gameObject.name);
            if (hitStair.collider.CompareTag("Stairs"))
            {
                //Debug.Log(hitStair.collider.gameObject.name);
                bool canMoveOnStairs = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 5f, 0.5f, newMoveDir, moveSpeed * Time.deltaTime);
                // Handle the ramp movement here (e.g., modify the movement direction, apply force, etc.)
                if (canMoveOnStairs)
                {
                    newMoveDir = Vector3.ProjectOnPlane(newMoveDir, hitStair.normal);
                    transform.position += newMoveDir * moveSpeed * 0.6f * Time.deltaTime;
                }

                if (canMoveOnStairs && newMoveDir != Vector3.zero)
                {
                    animator.SetBool("isWalking", true);
                }
                else
                {
                    animator.SetBool("isWalking", false);
                }

                //animator.SetBool("onStairs", true);
            } else
            {
                //animator.SetBool("onStairs", false);
            }
        }
        else
        {
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 5f, 0.5f, newMoveDir, moveSpeed * Time.deltaTime);
            //if (!canMove )
            //{
            //    Debug.Log(hit.transform.gameObject.name);
            //}

            if (canMove)
            {
                if (transform.position.y != 0 || transform.position.y != 1.24)
                {
                    if (Mathf.Abs(transform.position.y) < Mathf.Abs(transform.position.y - 1.24f))
                    {
                        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x, 1.24f, transform.position.z);
                    }
                }
                transform.position += newMoveDir * moveSpeed * Time.deltaTime;
            }

            if (canMove && moveDir != Vector3.zero)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        //smooth rotation rotateSpeed 10;
        // transform.forward=Vector3.Slerp (transform.forward, moveDir, Time.deltaTime * 10f);

        /*0.5 for rotation */
        Vector3 rotateDir = GetNormalizedRotate();
        transform.Rotate(rotateDir * Time.deltaTime);

        /*2. for interaction*/
        //check if the char is touching an object; need to new an inputVector
        //lastDirection = newMoveDir != Vector3.zero ? newMoveDir : lastDirection;
        lastDirection = transform.forward;


        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        BoxCollider bc = hit.collider as BoxCollider;
        //        if (bc != null)
        //        {
        //            Destroy(bc.gameObject);
        //        }
        //    }
        //}
    }

    private Vector3 GetNormalizedMove() {
        Vector2 inputVector = gameInput.InputMove().normalized;
        return new Vector3(inputVector.x, 0f, inputVector.y);
    }

    private Vector3 GetNormalizedRotate()
    {
        Vector2 inputVector = gameInput.InputRotate();
        return new Vector3(0f, inputVector.x * 90, 0f);
    }

    // interface is implemented here
    // we also can implement a dog holding a toy by using interface
    public Transform MoveKitchenObj() {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObj (KitchenObject kitchenObject) {
        // we can set some pickup animation here. But need to align with GetKitchenObj
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObj() {
        return kitchenObject;
    }

    public void ClearKitchenObj() {
        kitchenObject = null;
    }

    public bool HoldKitchenObj() {
        return kitchenObject != null;
    }


}
