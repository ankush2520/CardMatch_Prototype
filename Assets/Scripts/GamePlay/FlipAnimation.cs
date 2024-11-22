using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class FlipAnimation : MonoBehaviour
    {
        private bool playAnimation;

        public void PlayFlip() {
            playAnimation = true;
        }

        private void FixedUpdate()
        {
            if (playAnimation)
            {
                float rotationY = 50 * Time.deltaTime;
                transform.Rotate(0, rotationY, 0);
                playAnimation = false;
            }            
        }
    }
}
