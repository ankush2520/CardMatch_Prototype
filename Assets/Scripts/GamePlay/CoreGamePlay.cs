using System;
using System.Collections;
using System.Collections.Generic;
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

        private void OnEnable()
        {
            CardGameEvents.StartGame.AddListener(StartGame);
        }

        private void OnDisable()
        {
            CardGameEvents.StartGame.RemoveListener(StartGame);

        }

        private void StartGame(int rowCount, int colCount)
        {
            gridCreator.CreateGrid(rowCount, colCount);
        }

        public void RestartGame() {
            SceneManager.LoadScene(0);
        }


    }
}