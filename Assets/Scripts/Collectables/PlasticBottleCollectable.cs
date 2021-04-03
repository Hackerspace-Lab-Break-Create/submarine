namespace Assets.Scripts.Collectables
{
    public class PlasticBottleCollectable : CollectablesBase
    {
        public override bool OnCollect(PlayerController player)
        {
            var result = base.OnCollect(player);

            if (result)
            {
                
            }

            return result;
        }
    }
}
