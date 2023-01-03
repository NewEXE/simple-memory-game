using System;
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

    private void Awake()
    {
        Messenger.AddListener(GameEvent.REVEAL_MATCHED_CARDS, OnRevealMatchedCard);
        Messenger.AddListener(GameEvent.REVEAL_NOT_MATCHED_CARDS, OnRevealNotMatchedCard);
        Messenger.AddListener(GameEvent.MOVES_OVER, OnMovesOver);
    }

    private void Start()
    {
        SetScore(Managers.GameProcess.GetScore());
        SetMovesLeft(Managers.GameProcess.GetMovesLeft());
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.REVEAL_MATCHED_CARDS, OnRevealMatchedCard);
        Messenger.RemoveListener(GameEvent.REVEAL_NOT_MATCHED_CARDS, OnRevealNotMatchedCard);
        Messenger.RemoveListener(GameEvent.MOVES_OVER, OnMovesOver);
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

    private void OnRevealMatchedCard()
    {
        SetScore(Managers.GameProcess.GetScore());
    }
    
    private void OnRevealNotMatchedCard()
    {
        SetMovesLeft(Managers.GameProcess.GetMovesLeft());
    }
    
    private void OnMovesOver()
    {
        ShowGameOverLabel();
    }
}
