using GameManagers;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI scoreLabel;
    
    [SerializeField]
    private TextMeshProUGUI movesLeftLabel;
    
    [SerializeField]
    private GameObject background;
    
    [SerializeField]
    private GameObject gameOverCanvas;

    private void Update()
    {
        SetScore(Managers.GameProcess.GetScore());
        SetMovesLeft(Managers.GameProcess.GetMovesLeft());

        if (Managers.GameProcess.IsGameOver())
        {
            ShowGameOverLabel();
        }
    }

    public void OnStartClick() {
        Managers.GameProcess.Restart();
    }
    
    private void SetScore(int value) {
        scoreLabel.text = "Score: " + value;
    }
    
    private void SetMovesLeft(int value) {
        movesLeftLabel.text = "Moves Left: " + value;
    }

    private void ShowGameOverLabel() {
        Vector3 backgroundPos = background.transform.position;
        background.transform.position = new Vector3(backgroundPos.x, backgroundPos.y, -99f);

        gameOverCanvas.SetActive(true);
    }
}
