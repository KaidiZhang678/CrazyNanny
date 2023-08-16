using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface InterfaceProgress {

    public event EventHandler<OnProgressEventArgs> OnProgress;
    
    public class OnProgressEventArgs: EventArgs {
        public float progressNormalized;
    }
}
