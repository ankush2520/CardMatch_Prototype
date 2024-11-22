using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] TMP_InputField colInput, rowInput;
        [SerializeField] TextMeshProUGUI warningText;
        [SerializeField] Button playButton;
        [SerializeField] GameObject inputSelectionPanel;

        private int colCount, rowCount;

        private const int maxSize = 6;
        private const string waringTextForEvenCards = "Note : For the game to begin, either the number of rows or the number of columns must be even. If both are odd, the game will not start.";

        private const string waringTextForMaxLimit = "Note : For the game to begin, both dimensions must be greater than 0 and less than or equal to 6.";

        private void Start()
        {
           // playButton.interactable = false;
        }

        public void StartGame() {
            GetInput();

            if (colCount > 6 || colCount <= 0 || rowCount > 6 || rowCount <= 0)
            {
                warningText.gameObject.SetActive(true);
                warningText.text = waringTextForMaxLimit;
                return;
            }
            if (colCount % 2 != 0 && rowCount % 2 != 0)
            {
                warningText.gameObject.SetActive(true);
                warningText.text = waringTextForEvenCards;
                return;
            }

            CardGameEvents.StartGame.Dispatch(rowCount, colCount);

            inputSelectionPanel.SetActive(false);

        }

        private void GetInput()
        {
            if (string.IsNullOrEmpty(colInput.text))
                colCount = 0;
            else
                colCount = int.Parse(colInput.text);

            if (string.IsNullOrEmpty(rowInput.text))
                rowCount = 0;
            else
                rowCount = int.Parse(rowInput.text);
        }

    }
}
