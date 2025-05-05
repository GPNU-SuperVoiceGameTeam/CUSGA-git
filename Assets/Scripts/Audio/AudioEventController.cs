using UnityEngine;
using UnityEngine.Events;
public class AudioEventController : MonoBehaviour
{
    public static UnityAction<AudioType> OnPlayAudio;
 
    public static void RaiseOnPlayAudio(AudioType type)
    {
        OnPlayAudio?.Invoke(type);
    }
}
