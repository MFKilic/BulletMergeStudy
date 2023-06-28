using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TemplateFx.CameraControl;

public enum GateEnum
{
    RANGE, FIRERATE, TRIPLESHOT, BULLETSIZEUP
}
public enum GateMovementEnum
{
    Static, Backward, LeftAndRight
}
namespace TemplateFx.Controller
{
    public class GateController : MonoBehaviour , IGate
    {

        [Header("Editor")]

        [SerializeField] bool isRandomize;


        [Header("Game")]
        public GateEnum gateType;
        public GateMovementEnum gateMovementType;
        public int gatepoint;
        public TextMeshPro gateText, gatePointText;
        public bool isUsed = false;
        private float distance;

        private Vector3 _startScale;

        private Vector3 _startEulerAngles;

        private Tween _shakeRotTween;

        private void RandomizeMaker()
        {
            int randomGateTypeNumber = Random.Range((int)GateEnum.RANGE, (int)GateEnum.BULLETSIZEUP + 1);

            int randomGateMovementNumber = Random.Range((int)GateMovementEnum.Static, (int)GateMovementEnum.LeftAndRight + 1);

            int randomGatePoint = Random.Range(-20, 101);

            gateType = (GateEnum)randomGateTypeNumber;

            gateMovementType = (GateMovementEnum)randomGateMovementNumber;

            gatepoint = randomGatePoint;
        }

        private void Start()
        {
            if(isRandomize)
            {
                RandomizeMaker();
            }

           
            
           

            _startScale = transform.localScale;
            _startEulerAngles = transform.eulerAngles;

            gateText.text = gateType.ToString();
           

            if (gateType == GateEnum.TRIPLESHOT || gateType == GateEnum.BULLETSIZEUP)
            {
                gatePointText.gameObject.SetActive(false);
            }
            else
            {
                gatePointText.text = gatepoint.ToString();
            }

            if (gateMovementType == GateMovementEnum.LeftAndRight)
            {
                HorizontalMovement();
            }
            else if (gateMovementType == GateMovementEnum.Backward)
            {
                StartCoroutine(DistanceCalculator());
            }
        }

        private IEnumerator DistanceCalculator()
        {
            for (; ; )
            {

                yield return new WaitForEndOfFrame();
                distance = Vector3.Distance(CameraController.Instance.controller.transform.position, transform.position);
                if (distance <= 10f)
                {
                    VerticalMovement();
                    break;
                }

            }

        }

        private void VerticalMovement()
        {
            transform.DOMoveZ(transform.position.z + 10, 5).SetEase(Ease.Linear);
        }



        private void HorizontalMovement()
        {
            transform.DOMoveX(2.5f, 2).SetEase(Ease.Linear).OnComplete(() => { transform.DOMoveX(-2.5f, 2).SetEase(Ease.Linear).OnComplete(HorizontalMovement); });
        }

        public void GateNumberIncrease(int gateNumber)
        {
            gatepoint += gateNumber;
            gatePointText.text = gatepoint.ToString();
            if(_shakeRotTween != null)
            {
                _shakeRotTween.Kill();
            }

            transform.eulerAngles = _startEulerAngles;
            _shakeRotTween = transform.DOShakeRotation(0.2f, 15, 10, 90).OnComplete(() => { DoFixer(transform); });
        }

        public void GateIsCollected()
        {
            isUsed = true;
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce).OnComplete(() => { gameObject.SetActive(false); });
           
        }

        private void DoFixer(Transform transform)
        {
            transform.eulerAngles = _startEulerAngles;
            transform.localScale = _startScale;
        }

        public int OnGetPoint()
        {
            GateIsCollected();
            return gatepoint;
        }

        public bool OnGetUpgrade()
        {
            GateIsCollected();
            return true;
        }

        public void OnUpgradeNumberIndex(int index)
        {
            GateNumberIncrease(index);
        }
    }
}

