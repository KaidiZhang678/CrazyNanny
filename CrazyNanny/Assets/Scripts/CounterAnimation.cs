using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAnimation : MonoBehaviour
{   
    //to trigger the container animation
    private Animator animator;
    [SerializeField] private Container container;

    //add event in container.cs to make the animator play
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        container.GrabObj += (sender, e) =>
        {
            animator.SetTrigger("OpenClose");
        };
    }
}
