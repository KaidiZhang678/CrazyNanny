using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start() {
        GameWorker.Instance.StateChange += GameWorker_StateChange;
        gameObject.SetActive(false);
    }

    private void GameWorker_StateChange(object sender, System.EventArgs e) {
        gameObject.SetActive(GameWorker.Instance.CountdownActive());
    }

    private void Update() {
        countdownText.text = GameWorker.Instance.GetCountdownTimer().ToString("#");
    }

}
