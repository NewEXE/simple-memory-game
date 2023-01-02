namespace GameManagers
{
    public interface IGameManager
    {
        ManagerStatus status { get; }
        void Startup();
    }
}