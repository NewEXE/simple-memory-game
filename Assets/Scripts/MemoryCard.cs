using GameManagers;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
    [SerializeField]
    private GameObject cardBack;

    private int _imageId;

    public int imageId {
        get { return _imageId; }
    }

    public void SetCard(int imageId, Sprite image) {
        _imageId = imageId;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    private void Reveal() {
        cardBack.SetActive(false);
        Managers.GameProcess.CardRevealed(this);
    }
    
    public void Unreveal() {
        cardBack.SetActive(true);
    }
    
    private void OnMouseDown() {
        if (cardBack.activeSelf && Managers.GameProcess.CanReveal) {
            Reveal();
        }
    }
}
