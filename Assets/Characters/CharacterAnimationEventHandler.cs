using UnityEngine;

public class CharacterAnimationEventHandler : MonoBehaviour
{
    public AudioClip footSound;
    public AudioClip hitSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void FootDown()
    {
        audioSource.PlayOneShot(footSound);
    }

    void Hit()
    {
        audioSource.PlayOneShot(hitSound);
    }
}
