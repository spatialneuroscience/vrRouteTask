using UnityEngine;

namespace Landmarks.Scripts
{
    public class LM_LoopSound : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private AudioSource audioSource;
        void Start()
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
