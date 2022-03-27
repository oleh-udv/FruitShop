namespace Game
{
    public interface ITriggerable
    {
        void TriggerEnterAction(Player player);
        void TriggerExitAction(Player player);
    }
}
