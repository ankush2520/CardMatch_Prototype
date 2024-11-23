using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CardMatch.CoreGamePlay;

namespace CardMatch {

    public class CardItem : MonoBehaviour
    {
        public VisibleCardSide visible_cardSide; 
        public int Index { get; private set; }
        public int IndentityNumber { get; private set; }

        [SerializeField] private Button button;
        [SerializeField] private Image mainImage, revealImage;
        [SerializeField] private FlipAnimation animationGameObject;
        [SerializeField] private TextMeshProUGUI identityNumber;

        private float watchTime = 0.5f;
        private bool inFlip = false;
          

        private void OnEnable()
        {
            CardGameEvents.OnFlipAnimationDone.AddListener(OnFlipDone);
        }

        private void OnDisable()
        {
            CardGameEvents.OnFlipAnimationDone.RemoveListener(OnFlipDone);
        }

        public void OnClick()
        {
            if (!inFlip)
                DoFlip();
        }


        private void OnFlipDone(int index)
        {
            if (index == Index)
            {
                animationGameObject.gameObject.SetActive(false);

                if (visible_cardSide == VisibleCardSide.front)
                {
                    ShowBackSide();
                    
                }
                else {
                    ShowFrontSide();
                }

                inFlip = false;
            }
        }

        private void ShowFrontSide() {
            visible_cardSide = VisibleCardSide.front;

            CardGameEvents.OnCardClosed.Dispatch(this);

            mainImage.color = Color.green;
            revealImage.gameObject.SetActive(false);
        }

        private void ShowBackSide() {
            visible_cardSide = VisibleCardSide.back;
            CardGameEvents.OnCardRevealed.Dispatch(this);
            mainImage.color = new Color(1, 1, 1, 0);
            revealImage.gameObject.SetActive(true);
        }

        public void DoFlip()
        {
            animationGameObject.gameObject.SetActive(true);
            animationGameObject.PlayFlip(Index);

            SoundManager.instance.PlayFlipSound();

            mainImage.color = new Color(1, 1, 1, 0);
            revealImage.gameObject.SetActive(false);

            inFlip = true;
        }

        public void SetCoordinatValues(int indentityNumber, int index)
        {
            IndentityNumber = indentityNumber;
            Index = index;

            identityNumber.text = IndentityNumber.ToString();
            Invoke(nameof(DoFlip), watchTime);
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
    }
}