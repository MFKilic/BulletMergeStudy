using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TemplateFx.Managers;

namespace TemplateFx
{

    public class ViewPlay : MonoBehaviour
    {
        private const string strMoney = "Money";
        [SerializeField] private TextMeshProUGUI _textLevel;
        [SerializeField] private TextMeshProUGUI _textMoney;
        [SerializeField] private GameObject _buttonsObject;
        [SerializeField] private int _moneyIndex = 0;
        
        // Start is called before the first frame update

        public int GetMoneyIndex() { return _moneyIndex; }

        public void ViewPlayStart()
        {
            _buttonsObject.SetActive(true);
            if(PlayerPrefs.GetInt(strMoney) == 0)
            {
                OnMoneyChange(30);
            }
            else
            {
                OnMoneyChange(0);
            }
         
            _textLevel.text = "LEVEL " + (LevelManager.Instance.datas.level + 1);
        }

        private void OnEnable()
        {
            LevelManager.Instance.eventManager.OnMergeStageIsFinishAction += OnMergeStageFinish;
        }

        private void OnMergeStageFinish()
        {
            _buttonsObject.SetActive(false);
        }

       
        private void OnDisable()
        {
            LevelManager.Instance.eventManager.OnMergeStageIsFinishAction -= OnMergeStageFinish;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                OnMoneyChange(100);
            }
        }

        public void OnMoneyChange(int money)
        {
            _moneyIndex = PlayerPrefs.GetInt(strMoney);
            _moneyIndex += money;
            _textMoney.text = "$" + _moneyIndex.ToString();
            PlayerPrefs.SetInt(strMoney, _moneyIndex);
            ButtonManager.Instance.ButtonInteractive();
        }
       
      

       
    }

}

