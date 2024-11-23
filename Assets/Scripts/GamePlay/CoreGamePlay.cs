using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

namespace CardMatch
{
    [System.Serializable]
    public class CardData
    {
        public int IdentityNumber;
        public int Index;
        public bool Destoryed;
    }

    public class CoreGamePlay : MonoBehaviour
    {
        public enum VisibleCardSide
        {
            none, front, back
        }

        [SerializeField] CardGridCreator gridCreator;
        [SerializeField] TextMeshProUGUI scoreText,notetext,gameDonetext;
        [SerializeField] GameObject choicePanel, inputSelectionPanel;

        private List<CardItem> currentMatch = new List<CardItem>();
        private List<CardData> current_cardData = new List<CardData>();

        private int ScoreCount, currentRowCount, currentColCount, destroyedCount;

   
        private void OnEnable()
        {
            CardGameEvents.OnClickPlayGame.AddListener(OnClickPlayGame);
            CardGameEvents.OnCardRevealed.AddListener(OnCardRevealed);
            CardGameEvents.OnCardClosed.AddListener(OnCardClosed);
        }

        private void OnDisable()
        {
            CardGameEvents.OnClickPlayGame.RemoveListener(OnClickPlayGame);
            CardGameEvents.OnCardRevealed.RemoveListener(OnCardRevealed);
            CardGameEvents.OnCardClosed.RemoveListener(OnCardClosed);
        }

        private void UpdateScore(int newScore) {
            ScoreCount += newScore;
            ScoreCount = Math.Max(0, ScoreCount);
            scoreText.text = "Score: " +  ScoreCount; 
        }

        private void OnCardRevealed(CardItem item)
        {
            if (!currentMatch.Contains(item))
            {
                currentMatch.Add(item);
            }

            if (currentMatch.Count == 2)
            {
                Invoke(nameof(CheckStatus), 0.1f);
            }
        }

        private void OnCardClosed(CardItem item)
        {
            if (currentMatch != null && currentMatch.Count > 0)
            {
                if (currentMatch[0].Index == item.Index)
                {
                    currentMatch = new List<CardItem>();
                }
            }            
        }

        private void CheckforWinStatus()
        {
            destroyedCount += 2;
            if (destroyedCount >= currentRowCount * currentColCount)
            {
                gameDonetext.gameObject.SetActive(true);
                gameDonetext.text = "Game Done Your Score: " + ScoreCount;
                current_cardData = null;
                ScoreCount = 0;
            }
        }

        private void CheckStatus()
        {
            int expectedScore = currentRowCount * currentColCount;

            if (currentMatch[0].IndentityNumber == currentMatch[1].IndentityNumber)
            {
                UpdateScore(expectedScore);
                gridCreator.DeleteDataFromList(currentMatch[0].Index);
                gridCreator.DeleteDataFromList(currentMatch[1].Index);

                for (int i = 0; i < current_cardData.Count; i++)
                {
                    if (currentMatch[0].IndentityNumber == current_cardData[i].IdentityNumber)
                        current_cardData[i].Destoryed = true;
                    if (currentMatch[1].IndentityNumber == current_cardData[i].IdentityNumber)
                        current_cardData[i].Destoryed = true;
                }

                SoundManager.instance.PlayCorrectSound();
                CheckforWinStatus();

            }
            else
            {
                SoundManager.instance.PlayWrongSound();

                UpdateScore(-((expectedScore) / 2));
                currentMatch[0].DoFlip();
                currentMatch[1].DoFlip();
            }

            currentMatch = new List<CardItem>();
        }

        private void OnClickPlayGame(int rowCount, int colCount)
        {
            inputSelectionPanel.SetActive(false);

            currentRowCount = rowCount;
            currentColCount = colCount;
            destroyedCount = 0;
            ScoreCount = 0;

            List<int> randomList = GetListOfRandomNumbers(rowCount, colCount);

            gridCreator.CreateGrid(rowCount, colCount, randomList);
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

            FillCardDataList(randomList);

            return randomList;
        }

        private List<CardData> GetIdentityNumbersFromPrefs() {
            string data = PlayerPrefs.GetString("GridData");
            List<CardData> cardData = JsonConvert.DeserializeObject<List<CardData>>(data);
            return cardData;
        }

        private void FillCardDataList(List<int> list)
        {
            current_cardData = new List<CardData>();
            for (int i = 0; i < list.Count; i++)
            {
                CardData obj = new CardData();

                obj.IdentityNumber = list[i];
                obj.Index = i;

                current_cardData.Add(obj);
            }
        }

        private void Shuffle(List<int> list)
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

        public void ResetToDefault() {
            SaveDataToPrefs();
            SceneManager.LoadScene(0);
        }

        public void NewGame() {
            inputSelectionPanel.SetActive(true);
            choicePanel.SetActive(false);
        }

        public void LoadLastGame() {
            currentRowCount = PlayerPrefs.GetInt("GridRows");
            currentColCount = PlayerPrefs.GetInt("GridColumns");
            destroyedCount = PlayerPrefs.GetInt("destroyedCount");

            UpdateScore(PlayerPrefs.GetInt("Score"));

            List<CardData> cardData = GetIdentityNumbersFromPrefs();
            current_cardData = cardData;
            if (cardData == null || cardData.Count == 0)
            {
                notetext.gameObject.SetActive(true);
                notetext.text = "No Saved Game";
                return;
            }

            List<int> indentityNumberList = new List<int>();

            for (int i = 0; i < cardData.Count; i++)
                indentityNumberList.Add(cardData[i].IdentityNumber);

            gridCreator.CreateGrid(currentRowCount, currentColCount, indentityNumberList);

            for (int i = 0; i < cardData.Count; i++)
            {
                if (cardData[i].Destoryed)
                    gridCreator.DeleteDataFromList(cardData[i].Index);
            }

            choicePanel.SetActive(false);
        }

        private void SaveDataToPrefs() {
            PlayerPrefs.SetInt("GridRows", currentRowCount);
            PlayerPrefs.SetInt("GridColumns", currentColCount);
            PlayerPrefs.SetInt("Score", ScoreCount);
            PlayerPrefs.SetInt("destroyedCount", destroyedCount);
            string gridData = JsonConvert.SerializeObject(current_cardData);
            PlayerPrefs.SetString("GridData", gridData);

            PlayerPrefs.Save();
            Debug.Log("Grid data saved!");
        }

        private void OnApplicationQuit()
        {
            SaveDataToPrefs();
        }
    }
}