using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TemplateFx.Controller
{
    public class ObstacleController : MonoBehaviour, IObstacle
    {
        private enum ObstacleTypes
        {
            Saw, Rocket,NONE
        }

        [SerializeField] private ObstacleTypes obstacleType;

        [SerializeField] private Transform _obstacleTransform;

        private void Start()
        {
            switch (obstacleType)
            {
                case ObstacleTypes.Saw:
                    _obstacleTransform.DOLocalRotate(Vector3.forward * 360, 1, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
                    break;
                case ObstacleTypes.Rocket:
                    _obstacleTransform.DOLocalRotate(Vector3.forward * 360, 1, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
                    _obstacleTransform.DOLocalMoveX(0.2f, 1).SetLoops(-1,LoopType.Yoyo);
                    break;
            }
        }

        private void Update()
        {
            if (obstacleType == ObstacleTypes.Rocket)
            {
                transform.position += Vector3.back * Time.deltaTime * 7;
                if (transform.position.z < 40)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}

