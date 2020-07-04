using System;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
    [SerializeField]
    private GameObject cardBack;

    [SerializeField]
    private SceneController sceneController;

    private int _imageId;

    public int imageId {
        get { return this._imageId; }
    }

    public void SetCard(int imageId, Sprite image) {
        this._imageId = imageId;
        this.GetComponent<SpriteRenderer>().sprite = image;
    }

    private void Reveal() {
        this.cardBack.SetActive(false);
        this.sceneController.CardRevealed(this);
    }
    
    public void Unreveal() {
        this.cardBack.SetActive(true);
    }
    
    private void OnMouseDown() {
        if (this.cardBack.activeSelf && this.sceneController.CanReveal) {
            this.Reveal();
        }
    }
}
