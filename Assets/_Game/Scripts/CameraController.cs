
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using Pixelplacement;
using TemplateFx.Managers;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace TemplateFx.CameraControl
{
    public class CameraController : Singleton<CameraController>
    {

        [Header("Editor")]

        [SerializeField] bool isMergeStageOffset;

        [Header("Game")]

        public GameObject goPlayer;

        public Camera cameraHimself;

        [HideInInspector] public GunMovementController controller;

        [Header("MergeStageVector")]

        [SerializeField] Vector3 v3MergeOffset;

        [SerializeField] Vector3 v3MergeRotation;

        [Header("GunStageVector")]

        [SerializeField] Vector3 v3GunStageOffset;

        [SerializeField] Vector3 v3GunStageRotation;

        [Header("CameraSettings")]

        Vector3 v3StartPos;

        public float fFollowSpeed = 5;

        public float fLookSpeed = 5;

        private Tweener camShakeTweener;

        private bool isGunFollow;

        private bool isFollowStage;

        private void OnValidate()
        {
            if(isMergeStageOffset)
            {
                transform.position = v3MergeOffset;
                transform.eulerAngles = v3MergeRotation;
            }
            else
            {
                transform.position = v3GunStageOffset;
                transform.eulerAngles = v3GunStageRotation;
            }
            
        }

        void Awake()
        {
            v3StartPos = transform.position;
        
            transform.position = v3MergeOffset;
            transform.eulerAngles = v3MergeRotation;
        }

        private void OnEnable()
        {
            LevelManager.Instance.eventManager.OnMergeStageIsFinishAction += OnBulletMergeFinish;

            LevelManager.Instance.eventManager.OnBreakStageIsFinishAction += OnBreakStageIsFinish;

            LevelManager.Instance.eventManager.OnBreakBulletAction += OnBreakBullet;

            GameState.Instance.OnPrepareNewGameEvent += OnPrepareNewGameEvent;
        }

        private void OnPrepareNewGameEvent()
        {
            isGunFollow = false;
            isFollowStage = false;
            transform.position = v3StartPos;
            transform.eulerAngles = v3MergeRotation;

        }

        private void OnBreakBullet()
        {
            goPlayer = GridManager.Instance.GetFrontFollowObject();

            if (goPlayer == null)
            {
                LevelManager.Instance.eventManager.OnBreakStageIsFinish();
            }
        }

        private void OnBulletMergeFinish()
        {
            isFollowStage = true;
            goPlayer = GridManager.Instance.GetFrontFollowObject();
        }

        private void OnBreakStageIsFinish()
        {
            isGunFollow = true;
            transform.eulerAngles = v3GunStageRotation;
            goPlayer = controller.gameObject;
        }

        private void OnDisable()
        {

            LevelManager.Instance.eventManager.OnMergeStageIsFinishAction -= OnBulletMergeFinish;

            LevelManager.Instance.eventManager.OnBreakStageIsFinishAction -= OnBreakStageIsFinish;

            LevelManager.Instance.eventManager.OnBreakBulletAction -= OnBreakBullet;

            GameState.Instance.OnPrepareNewGameEvent -= OnPrepareNewGameEvent;

        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                goPlayer = GridManager.Instance.GetFrontFollowObject();
            }
        }

        void LateUpdate()
        {
            if(goPlayer == null || !isFollowStage)
            {
                return;
            }

            FollowBullets();

        }

        public void ShakeCamera(float fDelay, float fDuration, float fStrength = 3f, int nVibrato = 10, float fRandomness = 90f, bool bFadeOut = true)
        {
            StartCoroutine(Shake(fDelay, fDuration, fStrength, nVibrato, fRandomness, bFadeOut));
        }

        public IEnumerator Shake(float fDelay, float fDuration, float fStrength = 3f, int nVibrato = 10, float fRandomness = 90f, bool bFadeOut = true)
        {
            if (fDelay > 0)
            {
                yield return new WaitForSeconds(fDelay);
            }
            else
            {
                yield return null;
            }

            camShakeTweener = Camera.main.DOShakePosition(fDuration, fStrength, nVibrato, fRandomness, bFadeOut);
        }




        private void FollowBullets()
        {
            Vector3 _targetPos = Vector3.zero;

            if(isGunFollow)
            {
                _targetPos = goPlayer.transform.position + v3GunStageOffset;
                
            }
            else
            {
                _targetPos = new Vector3(0, goPlayer.transform.position.y, goPlayer.transform.position.z) + v3MergeOffset;
            }

            transform.position = Vector3.Lerp(transform.position, _targetPos, fFollowSpeed * Time.deltaTime);

        }

    }







}

