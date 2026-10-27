using UnityEngine;
using CryFine;
namespace Cryfine
{ 
    public class AudioCF
    {
        public static void PlayRandom(AudioSource source, AudioClip[] clips)
        {
            if (source == null || clips == null || clips.Length == 0)
            {
                Debug.LogWarning("[Cry-Fine Audio] PlayRandom not working.");
                return;
            }

            int random = Random.Range(0, clips.Length);
            source.PlayOneShot(clips[random]);
        }
        public static void PlayWithRandomPitch(AudioSource source, AudioClip clip, float minPitch, float maxPitch)
        {
            if (source == null || clip == null)
            {
                Debug.LogWarning("[Cry-Fine Audio] PlayWithRandomPitch not working.");
                return;
            }

            float randomPitch = Random.Range(minPitch,maxPitch);
            source.PlayOneShot(clip,randomPitch);
        }
    }
}
