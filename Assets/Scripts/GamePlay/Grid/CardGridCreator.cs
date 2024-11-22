using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch
{
    public class CardGridCreator : MonoBehaviour
    {
        [SerializeField] Transform boxParent;
        [SerializeField] CardItem itemPrefab;
        [SerializeField] Transform leftPoint, rightPoint, downPoint, upPoint;

        private CardItem[,] cardItems = new CardItem[2, 2];


        public void CreateGrid(int row,int col) {

            ClearData();
            cardItems = new CardItem[row, col];

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
                    element.SetCoordinatValues(x, y, index);
                    cardItems[y, x] = element;
                    index++;
                }
                yPos += ygap;
            }
        }

        private void ClearData()
        {

            foreach (var item in cardItems)
            {
                if (item)
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }
}