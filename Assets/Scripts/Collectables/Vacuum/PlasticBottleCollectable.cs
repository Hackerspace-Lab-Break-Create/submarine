namespace Assets.Scripts.Collectables.Vacuum
{
    public class PlasticBottleCollectable : CollectablesBase, IVacuumable
    {
        public int Points = 0;

        public override bool OnCollect(PlayerController player)
        {
            GameState.Points += Points;

            var result = base.OnCollect(player);

            return result;
        }
    }
}
