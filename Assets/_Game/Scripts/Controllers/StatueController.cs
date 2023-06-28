using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using TMPro;
using UnityEngine;


namespace TemplateFx
{
    public class StatueController : MonoBehaviour , IHittable
    {
        [SerializeField] private int _healthCount = 100;
        [SerializeField] private TextMeshPro _healthText;
        [SerializeField] private GameObject _moneyObject;
        private Tween _shakeScaleTween;
        private Vector3 _startScale;
        public void Hit(int damage)
        {
            _healthCount -= damage;
            _healthText.text = _healthCount.ToString();

            if( _healthCount <= 0 )
            {
                _healthCount = 0;
                GameObject moneyPrefab = Instantiate(_moneyObject,transform.position + Vector3.up, Quaternion.identity,LevelManager.Instance.enviromentHolderTransform);
                moneyPrefab.transform.DOMoveY(0.2f, 0.5f);
                moneyPrefab.transform.DORotate(Vector3.up * 90, 0.5f);
                gameObject.SetActive(false);
            }

            if(_shakeScaleTween != null)
            {
                _shakeScaleTween.Kill();
            }
            transform.localScale = _startScale;
            _shakeScaleTween = transform.DOShakeScale(0.1f,0.1f,1,90);
        }

        // Start is called before the first frame update
        void Start()
        {
            _startScale = transform.localScale;
            _healthText.text = _healthCount.ToString();
        }

       
    }

}

