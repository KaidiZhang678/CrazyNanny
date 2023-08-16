using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreNumber;
    private BurgerManager burgerManager; 

    private void Start() {
        GameWorker.Instance.StateChange += GameWorker_StateChange;
        gameObject.SetActive(false);
        burgerManager = FindObjectOfType<BurgerManager>();
    }

    private void GameWorker_StateChange(object sender, System.EventArgs e) {
        if (GameWorker.Instance.IsGameOver()) {
            gameObject.SetActive(true);
            // load the total number from public var
            totalScoreNumber.text = burgerManager.coinText.text.Substring(2);
        }
        else {
            gameObject.SetActive(false);
        }
    }

}
