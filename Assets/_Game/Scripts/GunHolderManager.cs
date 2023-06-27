using DG.Tweening;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using UnityEngine;

namespace TemplateFx
{

    public class GunHolderManager : Singleton<GunHolderManager>
    {
        [SerializeField] private Transform[] gunTransforms;
        [SerializeField] private Transform gunMoveTransform;
        [SerializeField] private GunGridManager _gunGridSystem;

        private void OnEnable()
        {
            LevelManager.Instance.eventManager.OnBulletHitTheGunAction += OnBulletHitTheGun;
        }
        private void OnDisable()
        {
            LevelManager.Instance.eventManager.OnBulletHitTheGunAction -= OnBulletHitTheGun;
        }

        private void OnBulletHitTheGun(float xPos)
        {
            
        }
    }

}

