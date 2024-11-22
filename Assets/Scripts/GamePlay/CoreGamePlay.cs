using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class CoreGamePlay : MonoBehaviour
    {
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
            CreateGrid(rowCount, colCount);
        }

        private void CreateGrid(int rowCount, int colCount)
        {
            
        }
    }
}