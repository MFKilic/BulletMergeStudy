using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using UnityEngine;
using Pixelplacement;
using DG.Tweening;

namespace TemplateFx
{
    public class GunGridManager : Singleton<GunGridManager>
    {
        [SerializeField] private GameObject _emptyTransformPrefab;

        [HideInInspector] public Transform gunMovementObjectTransform;

        private List<Transform> _listOfGunTransforms = new List<Transform>();

        [SerializeField] private List<Transform> _listOfGunGridTransforms = new List<Transform>();


        float _mirrorNumber = 0;
        bool _mirrorNumberisNegative;
        
        public void SetGunTransforms(Transform trGun)
        {
            _listOfGunTransforms.Add(trGun);
            trGun.SetParent(gunMovementObjectTransform);
          
        }

        private void OnEnable()
        {
            LevelManager.Instance.eventManager.OnBreakStageIsFinishAction += OnBreakStageIsFinish;
            GameState.Instance.OnPrepareNewGameEvent += OnPrepareNewGameEvent;
        }

        private void OnPrepareNewGameEvent()
        {
            _listOfGunTransforms.Clear();
            _listOfGunGridTransforms.Clear();
        }

        private void OnDisable()
        {
            LevelManager.Instance.eventManager.OnBreakStageIsFinishAction -= OnBreakStageIsFinish;
            GameState.Instance.OnPrepareNewGameEvent -= OnPrepareNewGameEvent;
        }

        public void OnBreakStageIsFinish()
        {
          
            _mirrorNumber = 0;
            _mirrorNumberisNegative = false;
            for (int i = 0; i < _listOfGunTransforms.Count; i++)
            {
                GameObject transformPrefab = Instantiate(_emptyTransformPrefab, gunMovementObjectTransform);
                if (_listOfGunTransforms.Count % 2 == 0 && _listOfGunTransforms.Count != 0)
                {
                    transformPrefab.transform.localPosition = Vector3.right * MirrorNumber(true);
                }
                else
                {
                    transformPrefab.transform.localPosition = Vector3.right * MirrorNumber(false);
                }
                _listOfGunGridTransforms.Add(transformPrefab.transform);
            }

            for(int i = 0; i < _listOfGunGridTransforms.Count; i++)
            {
                _listOfGunTransforms[i].DOLocalJump(_listOfGunGridTransforms[i].localPosition,1,1,0.2f);
            }
        }

        private float MirrorNumber(bool isEven = false)
        {
            if (isEven)
            {
                if (_mirrorNumber == 0)
                {
                    _mirrorNumberisNegative = true;
                }
            }

            if (_mirrorNumberisNegative)
            {
                if(isEven)
                {
                    _mirrorNumber += 0.75f;
                }
                else
                {
                    _mirrorNumber += 1.25f;
                }
             
                _mirrorNumber *= -1;
                _mirrorNumberisNegative = false;
            }
            else
            {
                _mirrorNumber = Mathf.Abs(_mirrorNumber);
                _mirrorNumberisNegative = true;
            }

            return _mirrorNumber;
        }
    }
}

