using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch {
    public class CardItem : MonoBehaviour
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Index { get; private set; }

        [SerializeField] private Button button;
        [SerializeField] private Image mainImage, revealImage;
        [SerializeField] private FlipAnimation animationGameObject;

        
        public void SetCoordinatValues(int x, int y, int index)
        {
            X = x;
            Y = y;
            Index = index;
        }

        public void SetTransformValues(Transform boxParent, float height, float width, Vector3 position)
        {
            transform.SetParent(boxParent);
            Vector3 newSize = GetComponent<RectTransform>().sizeDelta;
            newSize.x = width;
            newSize.y = height;
            GetComponent<RectTransform>().sizeDelta = newSize;
            transform.localPosition = position;
        }

        public void OnClick()
        {
            animationGameObject.gameObject.SetActive(true);
            animationGameObject.PlayFlip();
        }

        public void SetMainButtonSprite(Sprite sprite)
        {
            mainImage.sprite = sprite;
        }

        public void ToggleInteractable(bool state)
        {
            button.interactable = state;
        }

        public void SetRevealImageColor(Color color)
        {
            revealImage.color = color;
        }

        public void SetRevealImage(Sprite revealImageSprite)
        {
            if (revealImageSprite == null)
            {
                revealImage.gameObject.SetActive(false);
            }
            else
            {
                revealImage.gameObject.SetActive(true);
                this.revealImage.sprite = revealImageSprite;
            }
        }
    }
}