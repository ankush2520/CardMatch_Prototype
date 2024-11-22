using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CardMatch
{
    public static class CardGameEvents
    {
        public static Events<int, int> StartGame = new();
        public static Events<int> OnFlipAnimationDone = new();
        public static Events<CardItem> OnCardRevealed = new();
        public static Events<CardItem> OnCardClosed = new();

    }
}
