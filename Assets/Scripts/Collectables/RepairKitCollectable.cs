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
                var Clips = player.GetSoundClips();
                var _audioSource = player.GetAudioSource();

                _audioSource.PlayOneShot(Clips[5]);

                Destroy(this.gameObject);
            }

            return result;
        }
    }
}
