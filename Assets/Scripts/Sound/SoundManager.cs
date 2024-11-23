using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;

        [SerializeField] AudioClip flipSound;
        [SerializeField] AudioClip wrongCombination;
        [SerializeField] AudioClip correctCombination;

        public static SoundManager instance = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void PlayFlipSound()
        {
            audioSource.PlayOneShot(flipSound);
        }

        public void PlayWrongSound()
        {
            audioSource.PlayOneShot(wrongCombination);
        }

        public void PlayCorrectSound()
        {
            audioSource.PlayOneShot(correctCombination);
        }

    }
}