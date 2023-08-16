using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject progressGameObject;
    [SerializeField] private Image barImage;

    private void Start() {
        // call the interface by using getcomponent with GameObject
        progressGameObject.GetComponent<InterfaceProgress>().OnProgress += CookingProgressChange;
        barImage.fillAmount = 0f;
        gameObject.SetActive(false);
    }

    private void CookingProgressChange(object sender, InterfaceProgress.OnProgressEventArgs e) {
        barImage.fillAmount = e.progressNormalized;
        gameObject.SetActive(e.progressNormalized == 0f || e.progressNormalized == 1f 
            ? false : true);
    }

}
