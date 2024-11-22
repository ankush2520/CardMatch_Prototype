using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEditor;
using UnityEngine;

namespace CardMatch
{

    [System.Serializable]
    public class CardData
    {
        public int Index;
        public int IdentityNumber;
    }

    public class CardGridCreator : MonoBehaviour
    {
        

        [SerializeField] Transform boxParent;
        [SerializeField] CardItem itemPrefab;
        [SerializeField] Transform leftPoint, rightPoint, downPoint, upPoint;

        private CardData[]  cardData;

        public void CreateGrid(int row, int col, List<int> identityNumbers)
        {

            ClearData();
            cardData = new CardData[row * col];

            float horizontal_diff = rightPoint.localPosition.x - leftPoint.localPosition.x;
            float vertical_diff = upPoint.localPosition.y - downPoint.localPosition.y;

            float xgap = horizontal_diff / col;
            float ygap = vertical_diff / row;

            float width = xgap * 0.8f;
            float height = ygap * 0.8f;

            float xPos = leftPoint.localPosition.x + (width / 2) + (xgap * 0.1f);
            float yPos = downPoint.localPosition.y + (height / 2) + (ygap * 0.1f);

            int index = 0;

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    CardItem element = Instantiate(itemPrefab);
                    element.SetTransformValues(boxParent, height, width, new Vector3(xPos + (x * xgap), yPos));
                    element.gameObject.SetActive(true);
                    element.SetCoordinatValues(identityNumbers[index], index);

                    CardData obj = new()
                    {
                        IdentityNumber = identityNumbers[index],
                        Index = index
                    };

                    cardData[index] = obj;

                    index++;
                }
                yPos += ygap;
            }
        }

        private void ClearData()
        {
            
        }

        private void OnApplicationQuit()
        {
            // Save grid dimensions
            //   PlayerPrefs.SetInt("GridRows", rows);
            //   PlayerPrefs.SetInt("GridColumns", columns);

            // Save grid elements

            print(cardData.Length);
            string gridData = UnityEngine.JsonUtility.ToJson(cardData);

            print(gridData);

           // PlayerPrefs.SetString("GridData", gridData);

          //  PlayerPrefs.Save();
            Debug.Log("Grid data saved!");
        }

    }
}