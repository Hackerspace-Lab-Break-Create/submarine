namespace Assets.Scripts.Collectables
{
    public class RepairKitCollectable : CollectablesBase
    {
        public override bool OnCollect(PlayerController player)
        {
            var canAdd = player.GetInventory()
                .AddKnife();

            if (!canAdd)
            {
                return false;
            }

            var result = base.OnCollect(player);

            if (result)
            {
                Destroy(this.gameObject);
            }

            return result;
        }
    }
}
