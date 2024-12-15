public interface IGameStateFactory
{
    LoadMenuState CreateLoadMenuState();
    InitState CreateInitState();
    LoadLevelState CreateLoadLevelState();
}