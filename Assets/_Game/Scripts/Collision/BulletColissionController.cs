using System.Collections;
using System.Collections.Generic;
using TemplateFx.Controller;
using TemplateFx.Managers;
using TemplateFx.Merge;
using UnityEngine;

namespace TemplateFx.Collision
{
    public class BulletColissionController : MonoBehaviour
    {
        private const string strBreak = "Break";
        private const string strBreakSound = "BreakSound";
        
        [SerializeField] private BulletController _bulletController;

        private void Start()
        {
            _bulletController = GetComponent<BulletController>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IBreakeable breakeable))
            {
                GameObject goBreak = PoolManager.Instance.GetPooledObject(strBreak);

                goBreak.transform.position = transform.position;

                SoundManager.Instance.SoundPlay(strBreakSound);
                
                if (goBreak.TryGetComponent(out ParticleSystemRenderer psRenderer))
                {
                    psRenderer.mesh = breakeable.BreakeableMesh();
                    psRenderer.material = breakeable.BreakeableMaterial();

                }
                if (goBreak.TryGetComponent(out ParticleSystem ps))
                {
                    ps.Play();
                }
                UIManager.Instance.viewPlay.OnMoneyChange(breakeable.OnPrice());
                _bulletController.BulletHealthChanged(breakeable.OnBreak());
            }

            if (other.TryGetComponent(out GunGridController gunGridController))
            {

                gunGridController.SetTheGun();
                _bulletController.BulletDestroyer();
            }
        }
    }

}

