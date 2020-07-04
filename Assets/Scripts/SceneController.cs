using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    private const int gridRows = 2;
    private const int gridCols = 4;
    private const float offsetX = 2f;
    private const float offsetY = 2.5f;
    
    [SerializeField]
    private UIController uiController;
    
    [SerializeField]
    private MemoryCard originalCard;
    
    [SerializeField]
    private Sprite[] images;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    
    private int _score;
    
    private int _movesLeft = 5;

    public bool CanReveal {
        get { return this._secondRevealed == null; }
    }
    
    public void CardRevealed(MemoryCard card) {
        if (this._firstRevealed == null) {
            this._firstRevealed = card;
        } else {
            this._secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    public void Restart() {
        SceneManager.LoadScene("Game");
    }
    
    void Start() {
        this.uiController.SetMovesLeft(this._movesLeft);
        
        Vector3 startPos = this.originalCard.transform.position;

        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++) {
            for (int j = 0; j < gridRows; j++) {
                MemoryCard card;
                if (i == 0 && j == 0) {
                    card = this.originalCard;
                } else {
                    card = Instantiate(this.originalCard);
                }

                int index = j * gridCols + i;
                int imageId = numbers[index];
                card.SetCard(imageId, this.images[imageId]);

                float posX = offsetX * i + startPos.x;
                float posY = -(offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers) {
        int[] newArray = numbers.Clone() as int[];
        
        for (int i = 0; i < newArray.Length; i++) {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    private IEnumerator CheckMatch() {
        if (this._firstRevealed.imageId == this._secondRevealed.imageId) {
            this._score++;
            this.uiController.SetScore(this._score);
        } else {
            yield return new WaitForSeconds(.5f);

            this._firstRevealed.Unreveal();
            this._secondRevealed.Unreveal();
            
            this._movesLeft--;
            this.uiController.SetMovesLeft(this._movesLeft);
            
            if (this._movesLeft == 0) {
                StartCoroutine(RunGameOver());
            }
        }

        this._firstRevealed = null;
        this._secondRevealed = null;
    }

    private IEnumerator RunGameOver() {
        this.uiController.ShowGameOverLabel();
        yield return new WaitForSeconds(1.5f);
        this.Restart();
    }
}
