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
        
        private int _score;
        private int _movesLeft = 5;
        private bool _isGameOver;

        public int GetMovesLeft()
        {
            return _movesLeft;
        }
        
        public int GetScore()
        {
            return _score;
        }
        
        public bool IsGameOver()
        {
            return _isGameOver;
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
                // Score was increased
                _score++;
            } else {
                // Moves left decreased
                yield return new WaitForSeconds(.5f);

                _firstRevealed.Unreveal();
                _secondRevealed.Unreveal();
            
                _movesLeft--;
            
                if (_movesLeft == 0)
                {
                    // Game was failed, restart.
                    _isGameOver = true;
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