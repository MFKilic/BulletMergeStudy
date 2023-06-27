using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using UnityEngine;
using DG.Tweening;
using TemplateFx.Merge;

namespace TemplateFx
{
    public class GunController : Gun
    {
        [SerializeField] private Transform _recoilTransform;
        public bool isActive;

        private void FireTween()
        {
            if (!GameState.Instance.IsPlaying())
            {
                return;
            }
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
        }
        private void OnDisable()
        {
            LevelManager.Instance.eventManager.OnBreakStageIsFinishAction -= OnBreakStageIsFinish;
            LevelManager.Instance.eventManager.OnGateIsCollectedAction -= OnGateIsCollected;
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
            if(!isActive)
            {
                return;
            }

            for(int i = 0; i < fireBulletCount; i++)
            {
                GameObject bullet = Instantiate(bulletObject, fireTransforms[i].position, fireTransforms[i].rotation, LevelManager.Instance.enviromentHolderTransform);

                if (bullet.TryGetComponent(out GunBulletController gunBulletController))
                {
                    gunBulletController.ManuelStart(lifeTime,bulletScaleIndex);
                }
            }
            

            FireTween();
        }

        public void IncreaseFireSpeed(float fireSpeedIndex)
        {
            firePerSecond -= fireSpeedIndex / 100f;
            if (firePerSecond < 0.1f)
            {
                firePerSecond = 0.1f;
            }
        }

        public void IncreaseRange(float rangeIndex)
        {
            lifeTime += rangeIndex / 100f;
        }

        public void TripleShotUpgrade(bool tripleShotIsActive)
        {
            fireBulletCount = 3;

        }

        public void BiggerBulletsUpgrade(bool biggerBulletsUpgrade)
        {
            bulletScaleIndex = 0.5f;
        }

    }

}
