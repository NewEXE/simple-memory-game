using UnityEngine;

namespace GameManagers
{
    public class BaseGameManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; protected set; }

        public BaseGameManager()
        {
            status = ManagerStatus.Shutdown;
        }

        public virtual void Startup()
        {
            status = ManagerStatus.Started;
        }
    }
}