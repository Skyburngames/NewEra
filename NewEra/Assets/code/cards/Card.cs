using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using EventsNS;
using BuildingBlocksNS;

namespace CardsNS
{
    public class Card : MonoBehaviour
    {

        public Text title;
        public Text description;
        public Image image;
        public GameObject selected;

        private CardData _cardData;

        [Header("Events")]
        public BaseEvent select_Card;
        public BaseEvent deselect_Card;


        public CardData CardData
        {
            get
            {
                return _cardData;
            }

            set
            {
                _cardData = value;
                Refresh();
            }
        }

        public Vector2 GetCardSize()
        {
            Vector2 cardScale = this.transform.localScale;
            Vector2 cardSize = ((RectTransform)this.transform).sizeDelta;
            cardSize.x *= cardScale.x;
            cardSize.y *= cardScale.y;
            return cardSize;
        }

        private void Refresh()
        {
            this.title.text = CardData.cardName;
            this.description.text = CardData.description;
            this.image.sprite = CardData.image;
        }


        public void SelectCard()
        {
            selected.SetActive(true);

            /*
            Dictionary<string, object> dict = new Dictionary<string, object>() {
                {"card", this}
            };*/

            List<DataHolder> dataList = new List<DataHolder>() { };
            dataList.Add(new DataHolder("card", this));

            select_Card.Raise(dataList, this);
        }


        public void DeSelectCard()
        {
            StartCoroutine(DeselectOnNextFrame()); //release on next frame, some object require the releaseMouseEvent
        }


        IEnumerator DeselectOnNextFrame()
        {
            yield return new WaitForSeconds(0f);
            selected.SetActive(false);

            /*
            Dictionary<string, object> dict = new Dictionary<string, object>() {
                {"card", this}
            };*/
            List<DataHolder> dataList = new List<DataHolder>() { };
            dataList.Add(new DataHolder("card", this));

            deselect_Card.Raise(dataList, this);
        }

    }
}
