using UnityEngine;
using Pixelplacement;
using UnityEngine.UI;
using TMPro;
using TemplateFx.Managers;

public enum ButtonEnum
{
    Add
}

[System.Serializable]
public class ButtonClass
{
    public Button button;
    public float startPrice;
    public TextMeshProUGUI priceText;
    public float priceCross;
    public float price;
    public int buttonLevel;
    public string saveName;
    public string saveLevelName;

}


namespace TemplateFx.Managers
{
    public class ButtonManager : Singleton<ButtonManager>
    {


        public ButtonClass[] incrementalButtons;

        [HideInInspector] public ButtonEnum buttonName;


        private void Start()
        {
            ResetPrefs();
        }

        private void ResetPrefs()
        {
            foreach (ButtonClass bc in incrementalButtons)
            {
                if (PlayerPrefs.GetFloat(bc.saveName) == 0)
                {
                    PlayerPrefs.SetFloat(bc.saveName, bc.startPrice);
                    bc.priceText.text = TextWrite(bc.startPrice);
                    bc.button.interactable = PriceCheck(bc.startPrice);
                }
                else
                {
                    bc.price = PlayerPrefs.GetFloat(bc.saveName);
                    bc.buttonLevel = PlayerPrefs.GetInt(bc.saveLevelName);
                    bc.priceText.text = TextWrite(bc.price);
                    bc.button.interactable = PriceCheck(bc.price);
                }

            }
        }

        private string TextWrite(float price)
        {

            return "$" + price.ToString("F0");
        }

        public void ButtonInteractive()
        {
            foreach (ButtonClass bc in incrementalButtons)
            {
                bc.button.interactable = PriceCheck(PlayerPrefs.GetFloat(bc.saveName));
            }
        }

        private bool PriceCheck(float price)
        {
            bool isActive = !GridManager.Instance.GridIsFull();

            if (!isActive)
            {
                return isActive;
            }



            if (price < UIManager.Instance.viewPlay.GetMoneyIndex())
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }

            return isActive;
        }


        public void OnBuyingButtonChanged(int number)
        {

            ButtonClass bc = incrementalButtons[number];

            float tempPrice = PlayerPrefs.GetFloat(bc.saveName);

            UIManager.Instance.viewPlay.OnMoneyChange(-(int)tempPrice);

            bc.buttonLevel = PlayerPrefs.GetInt(bc.saveLevelName);

            bc.buttonLevel++;

            PlayerPrefs.SetInt(bc.saveLevelName, bc.buttonLevel);

            tempPrice *= bc.priceCross;

            bc.priceText.text = TextWrite(tempPrice);

            PlayerPrefs.SetFloat(bc.saveName, tempPrice);

            bc.button.interactable = PriceCheck(tempPrice);


        }


    }
}

