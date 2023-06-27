using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace TemplateFx
{
    public class StatueController : MonoBehaviour , IHittable
    {
        [SerializeField] private int _healthCount = 100;
        [SerializeField] private TextMeshPro _healthText;
        private Tween _shakeScaleTween;
        private Vector3 _startScale;
        public void Hit(int damage)
        {
            _healthCount -= damage;
            _healthText.text = _healthCount.ToString();

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

