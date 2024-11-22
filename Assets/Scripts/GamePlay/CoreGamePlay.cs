using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardMatch
{
    public class CoreGamePlay : MonoBehaviour
    {
        public enum VisibleCardSide
        {
            none, front, back
        }

        [SerializeField] CardGridCreator gridCreator;

        private int lastRevealIndex;

        private void OnEnable()
        {
            CardGameEvents.StartGame.AddListener(StartGame);
            CardGameEvents.OnCardRevealed.AddListener(OnCardRevealed);
        }

        private void OnDisable()
        {
            CardGameEvents.StartGame.RemoveListener(StartGame);
            CardGameEvents.OnCardRevealed.RemoveListener(OnCardRevealed);

        }

        private void OnCardRevealed(CardItem item)
        {
            lastRevealIndex = item.IndentityNumber;
        }

        private void StartGame(int rowCount, int colCount)
        {
            List<int> identityNumbers = GetListOfRandomNumbers(rowCount, colCount);

            gridCreator.CreateGrid(rowCount, colCount, identityNumbers);
        }

        private List<int> GetListOfRandomNumbers(int rowCount, int colCount)
        {
            List<int> randomList = new List<int>();

            int max = rowCount * colCount;

            for (int i = 1; i <= max / 2; i++)
            {
                randomList.Add(i);
                randomList.Add(i);
            }

            Shuffle(randomList);

            return randomList;
        }

        public void Shuffle(List<int> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public void RestartGame() {
            SceneManager.LoadScene(0);
        }


    }
}