using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CardMatch
{
    public static class CardGameEvents
    {
        public static Events<int, int> StartGame = new();
        public static Events<int> OnFlipDone = new();
             
    }
}
