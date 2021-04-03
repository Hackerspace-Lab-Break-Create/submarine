namespace Assets.Scripts.Collectables.Vacuum
{
    public class PlasticBottleCollectable : CollectablesBase, IVacuumable
    {
        public override bool OnCollect(PlayerController player)
        {
            var result = base.OnCollect(player);

            return result;
        }
    }
}
