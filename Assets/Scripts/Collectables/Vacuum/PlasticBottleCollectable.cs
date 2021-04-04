using UnityEngine;

namespace Assets.Scripts.Collectables.Vacuum
{
    public class PlasticBottleCollectable : CollectablesBase, IVacuumable
    {
        public int Points = 0;

        public override bool OnCollect(PlayerController player)
        {
            GameState.Points += Points;

            var result = base.OnCollect(player);

            if (result)
            {
                var Clips = player.GetSoundClips();
                var _audioSource = player.GetAudioSource();

                _audioSource.PlayOneShot(Clips[4]);
            }

            return result;
        }
    }
}
