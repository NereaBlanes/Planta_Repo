using UnityEngine;

public class AnimationSfxTriggers : MonoBehaviour
{
 public void PlayAnimationSFX(int sfxToPlay)
    {
        AudioManager.Instance.PlaySFX(sfxToPlay);
    }

}
