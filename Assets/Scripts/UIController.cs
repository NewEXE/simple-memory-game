using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField]
    private SceneController sceneController;
    
    [SerializeField]
    private TextMeshProUGUI scoreLabel;
    
    [SerializeField]
    private TextMeshProUGUI movesLeftLabel;
    
    [SerializeField]
    private GameObject backgound;
    
    [SerializeField]
    private GameObject gameOverCanvas;

    private void Start() {
        this.SetScore(0);
    }

    public void OnStartClick() {
        this.sceneController.Restart();
    }
    
    public void SetScore(int value) {
        this.scoreLabel.text = "Score: " + value;
    }
    
    public void SetMovesLeft(int value) {
        this.movesLeftLabel.text = "Moves Left: " + value;
    }

    public void ShowGameOverLabel() {
        Vector3 backgroundPos = this.backgound.transform.position;
        this.backgound.transform.position = new Vector3(backgroundPos.x, backgroundPos.y, -99f);

        this.gameOverCanvas.SetActive(true);
    }
}
