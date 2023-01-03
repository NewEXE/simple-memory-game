using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagers
{
    public class GameProcessManager : BaseGameManager
    {
        private MemoryCard _firstRevealed;
        private MemoryCard _secondRevealed;
        
        private const string SceneName = "Game";
        
        private int _score = 0;
        private int _movesLeft = 5;

        public int GetMovesLeft()
        {
            return _movesLeft;
        }
        
        public int GetScore()
        {
            return _score;
        }

        public void Restart() {
            SceneManager.LoadScene(SceneName);
        }
        
        public bool CanReveal {
            get { return _secondRevealed == null; }
        }
    
        public void CardRevealed(MemoryCard card) {
            if (_firstRevealed == null) {
                _firstRevealed = card;
            } else {
                _secondRevealed = card;
                StartCoroutine(CheckMatch());
            }
        }

        private IEnumerator CheckMatch() {
            if (_firstRevealed.imageId == _secondRevealed.imageId) {
                _score++;
                Messenger.Broadcast(GameEvent.REVEAL_MATCHED_CARDS);
            } else {
                yield return new WaitForSeconds(.5f);

                _firstRevealed.Unreveal();
                _secondRevealed.Unreveal();
            
                _movesLeft--;
                Messenger.Broadcast(GameEvent.REVEAL_NOT_MATCHED_CARDS);
            
                if (_movesLeft == 0)
                {
                    Messenger.Broadcast(GameEvent.MOVES_OVER);
                    StartCoroutine(RunGameOver());
                }
            }

            _firstRevealed = null;
            _secondRevealed = null;
        }

        private IEnumerator RunGameOver() {
            yield return new WaitForSeconds(1.5f);
            Restart();
        }
    }
}