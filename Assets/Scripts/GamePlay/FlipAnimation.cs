using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class FlipAnimation : MonoBehaviour
    {
        private bool playAnimation;
        private float rotationSpeed = 500, currentYRotation = 0f, targetYRotation = 180f;
        private int itemIndex = 0;

        public void PlayFlip(int index) {
            itemIndex = index;
            currentYRotation = 0;
            playAnimation = true;
        }

        private void FixedUpdate()
        {
            if (playAnimation)
            {
                if (currentYRotation < targetYRotation)
                {
                    float rotationStep = rotationSpeed * Time.fixedDeltaTime;

                    currentYRotation = Mathf.Min(currentYRotation + rotationStep, targetYRotation);
                    transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
                }
                else {
                    CardGameEvents.OnFlipAnimationDone.Dispatch(itemIndex);
                    playAnimation = false;
                }

            }            
        }
    }
}
