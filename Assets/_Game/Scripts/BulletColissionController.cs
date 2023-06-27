using System.Collections;
using System.Collections.Generic;
using TemplateFx.Merge;
using UnityEngine;

namespace TemplateFx
{
    public class BulletColissionController : MonoBehaviour
    {
        [SerializeField] private BulletController _bulletController;

        private void Start()
        {
            _bulletController = GetComponent<BulletController>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IBreakeable breakeable))
            {
                _bulletController.BulletHealthChanged(breakeable.OnBreak());
            }

            if(other.TryGetComponent(out GunGridController gunGridController))
            {
                
                gunGridController.SetTheGun();
                _bulletController.BulletDestroyer();
            }
        }
    }

}

