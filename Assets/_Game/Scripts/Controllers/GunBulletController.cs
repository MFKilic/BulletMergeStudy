using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateFx.Controller
{
    public class GunBulletController : MonoBehaviour
    {
        [SerializeField] float _speed = 20;
        [HideInInspector] public int damage = 1;
        [HideInInspector] private float _lifetimeIndex = 1;
        private WaitForSeconds _lifeTime;

        public void ManuelStart(float lifetimeIndex, float bulletScaleIndex)
        {
            transform.localScale = Vector3.one * bulletScaleIndex;
            _lifetimeIndex = lifetimeIndex;
            _lifeTime = new WaitForSeconds(_lifetimeIndex);
            StartCoroutine(BulletLifeTimeTimer());
        }

        private IEnumerator BulletLifeTimeTimer()
        {
            yield return _lifeTime;
            gameObject.SetActive(false);
        }

        void Update()
        {
            transform.position += transform.forward * Time.deltaTime * _speed;
        }
    }

}

