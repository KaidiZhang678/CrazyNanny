using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HungerSystem : MonoBehaviour
{
    public event EventHandler OnHungerIncreased;  // Declare the event

    public int CurrentHunger { get; private set; }

    private void Awake()
    {
        CurrentHunger = 100;
    }

    private void Update()
    {
        CurrentHunger -= 1;

        // Keep hunger value within 0-100 range
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, 100);

        // Call the event
        if (OnHungerIncreased != null)
            OnHungerIncreased(this, EventArgs.Empty);
    }

    public void Feed(int amount)
    {
        CurrentHunger += amount;

        // Keep hunger value within 0-100 range
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, 100);
    }
}