namespace Assets.Scripts.Collectables
{
    public class KnifeCollectable : CollectablesBase
    {
        public override bool OnCollect(PlayerController player)
        {
            player.GetInventory()
                .AddKnife();

            var result = base.OnCollect(player);

            if (result)
            {
                Destroy(this.gameObject);
            }

            return result;
        }
    }
}
