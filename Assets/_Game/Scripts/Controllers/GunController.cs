using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using UnityEngine;
using DG.Tweening;
using TemplateFx.Merge;
using UnityEditor;
using TemplateFx.Collision;

namespace TemplateFx.Controller
{
    public class GunController : Gun
    {
        private const string strBullet = "Bullet";
        private const string strGun = "Gun";

        [SerializeField] private Transform _recoilTransform;
        [SerializeField] private ParticleSystem _shieldParticle;
        [SerializeField] private GunCollisionController _gunCollisionController;
        [SerializeField] private ParticleSystem _muzzleParticle;
        private WaitForSeconds _shieldTime = new WaitForSeconds(4);
        private Coroutine _coroutineShield;
        [HideInInspector] public bool isActive;

        private void FireTween()
        {
            if (!GameState.Instance.IsPlaying())
            {
                return;
            }
            SoundManager.Instance.SoundPlay(strGun);
            _muzzleParticle.Clear();
            _muzzleParticle.Play();
            _recoilTransform.DORotate(new Vector3(-20, 0, 0), 0.2f).OnComplete(RecoilTween);
        }

        private void RecoilTween()
        {
            _recoilTransform.DORotate(new Vector3(0, 0, 0), firePerSecond).OnComplete(Fire);
        }

        private void OnEnable()
        {
            LevelManager.Instance.eventManager.OnBreakStageIsFinishAction += OnBreakStageIsFinish;
            LevelManager.Instance.eventManager.OnGateIsCollectedAction += OnGateIsCollected;
            LevelManager.Instance.eventManager.OnBoostCollectedAction += OnBoostCollected;
        }
        private void OnDisable()
        {
            LevelManager.Instance.eventManager.OnBreakStageIsFinishAction -= OnBreakStageIsFinish;
            LevelManager.Instance.eventManager.OnGateIsCollectedAction -= OnGateIsCollected;
            LevelManager.Instance.eventManager.OnBoostCollectedAction -= OnBoostCollected;
        }


        private void OnBoostCollected(BoostTypes type)
        {
            switch (type)
            {
                case BoostTypes.SHIELD:
                    ShieldBoost();
                    break;
                default:
                    Debug.Log("Undefined BoostTypes -> Please Check GunController Script");
                    break;
            }
        }

        private void OnBreakStageIsFinish()
        {
            Fire();
        }

        private void OnGateIsCollected(GateEnum gateEnum, IGate gate)
        {
            switch (gateEnum)
            {
                case GateEnum.RANGE:
                    IncreaseRange(gate.OnGetPoint());
                    break;

                case GateEnum.FIRERATE:
                    IncreaseFireSpeed(gate.OnGetPoint());
                    break;

                case GateEnum.TRIPLESHOT:
                    TripleShotUpgrade(gate.OnGetUpgrade());
                    break;

                case GateEnum.BULLETSIZEUP:
                    BiggerBulletsUpgrade(gate.OnGetUpgrade());
                    break;

                default:
                    Debug.Log("Undefined GateEnum -> Please Check GunController Script");
                    break;
            }
        }



        public override void Fire()
        {
            if (!isActive)
            {
                return;
            }

            for (int i = 0; i < fireBulletCount; i++)
            {
                GameObject bullet = PoolManager.Instance.GetPooledObject(strBullet);
                bullet.transform.position = fireTransforms[i].transform.position;
                bullet.transform.rotation = fireTransforms[i].transform.rotation;

                if (bullet.TryGetComponent(out GunBulletController gunBulletController))
                {
                    gunBulletController.ManuelStart(lifeTime, bulletScaleIndex);
                }
            }


            FireTween();
        }

        private void IncreaseFireSpeed(float fireSpeedIndex)
        {
            firePerSecond -= fireSpeedIndex / 100f;
            if (firePerSecond < 0.1f)
            {
                firePerSecond = 0.1f;
            }
        }

        private void IncreaseRange(float rangeIndex)
        {
            lifeTime += rangeIndex / 100f;
        }

        private void TripleShotUpgrade(bool tripleShotIsActive)
        {
            fireBulletCount = 3;

        }

        private void BiggerBulletsUpgrade(bool biggerBulletsUpgrade)
        {
            bulletScaleIndex = 0.5f;
        }


        public void ShieldBoost()
        {
            if (_coroutineShield != null)
            {
                StopCoroutine(_coroutineShield);
            }

            _coroutineShield = StartCoroutine(ShieldTimer());
        }

        private IEnumerator ShieldTimer()
        {
            _gunCollisionController.isImmune = true;
            _shieldParticle.Play();
            yield return _shieldTime;
            _gunCollisionController.isImmune = false;
            _shieldParticle.Stop();
        }
    }

}
