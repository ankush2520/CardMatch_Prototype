using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CardMatch.CoreGamePlay;

namespace CardMatch {
    public class CardItem : MonoBehaviour
    {
        VisibleCardSide cardSide;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Index { get; private set; }

        [SerializeField] private Button button;
        [SerializeField] private Image mainImage, revealImage;
        [SerializeField] private FlipAnimation animationGameObject;

        private Color initialFrontColor;

        private void OnEnable()
        {
            cardSide = VisibleCardSide.front;
            initialFrontColor = mainImage.color;

            CardGameEvents.OnFlipDone.AddListener(OnFlipDone);
        }

        private void OnDisable()
        {
            CardGameEvents.OnFlipDone.RemoveListener(OnFlipDone);
        }


        public void OnClick()
        {
            animationGameObject.gameObject.SetActive(true);
            animationGameObject.PlayFlip(Index);

            mainImage.color = new Color(1, 1, 1, 0);
            revealImage.gameObject.SetActive(false);
        }

        private void OnFlipDone(int index)
        {
            if (index == Index)
            {
                animationGameObject.gameObject.SetActive(false);

                if (cardSide == VisibleCardSide.front)
                {
                    cardSide = VisibleCardSide.back;
                    mainImage.color = new Color(1, 1, 1, 0);
                    revealImage.gameObject.SetActive(true);
                }
                else {
                    cardSide = VisibleCardSide.front;
                    mainImage.color = initialFrontColor;
                    revealImage.gameObject.SetActive(false);
                }
                
                
            }
        }


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


        public void SetMainButtonSprite(Sprite sprite)
        {
            mainImage.sprite = sprite;
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