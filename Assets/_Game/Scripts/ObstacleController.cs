using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TemplateFx
{
    public class ObstacleController : MonoBehaviour, IObstacle
    {
        private enum ObstacleTypes
        {
            Saw, Rocket
        }

        private ObstacleTypes obstacleType;

        [SerializeField] private Transform _obstacleTransform;

        private void Start()
        {
            switch (obstacleType)
            {
                case ObstacleTypes.Saw:
                    _obstacleTransform.DOLocalRotate(Vector3.forward * 360, 1, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
                    break;
                case ObstacleTypes.Rocket:

                    break;
            }
        }
    }
}

