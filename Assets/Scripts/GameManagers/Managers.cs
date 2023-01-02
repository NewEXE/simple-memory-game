using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagers
{
    [RequireComponent(typeof(GameProcessManager))]
    [RequireComponent(typeof(InterfaceManager))]
    public class Managers : MonoBehaviour
    {
        public static GameProcessManager GameProcess { get; private set; }
        public static InterfaceManager Interface { get; private set; }
    
        private List<IGameManager> _startSequence;
    
        void Awake()
        {
            GameProcess = GetComponent<GameProcessManager>();
            Interface = GetComponent<InterfaceManager>();
    
            _startSequence = new List<IGameManager>
            {
                GameProcess,
                Interface
            };

            StartCoroutine(StartupManagers());
        }
    
        private IEnumerator StartupManagers() {
            foreach (IGameManager manager in _startSequence) {
                manager.Startup();
            }

            yield return null;

            int numModules = _startSequence.Count;
            int numReady = 0;

            while (numReady < numModules) {
                int lastReady = numReady;
                numReady = 0;

                foreach (IGameManager manager in _startSequence) {
                    if (manager.status == ManagerStatus.Started) {
                        numReady++;
                    }
                }

                if (numReady > lastReady)
                    Debug.Log($"Progress: {numReady}/{numModules}");
			
                yield return null;
            }
		
            Debug.Log("All managers started up");
        }
    
    }
}


