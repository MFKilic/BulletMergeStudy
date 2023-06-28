using System.Collections;
using System.Collections.Generic;
using TemplateFx.CameraControl;
using TemplateFx.Controller;
using TemplateFx.Managers;
using UnityEngine;


namespace TemplateFx.Move
{
    public class GunMovementController : MonoBehaviour
    {
        float _currentXPos = 0;
        [SerializeField] private float _borderIndex = 5;
        [SerializeField] private float _sensivity = 2;
        [SerializeField] private float _speed = 5;
        [SerializeField] private bool _isMoveable;
        private void OnEnable()
        {
            InputController.OnInputChanged += InputController_OnInputChanged;
            LevelManager.Instance.eventManager.OnBreakStageIsFinishAction += OnBreakStageIsFinish;
        }

        private void InputController_OnInputChanged(float horizontalInput, float verticalInput)
        {
            _currentXPos = horizontalInput;
        }

        private void OnBreakStageIsFinish()
        {
            _isMoveable = true;
        }

        private void OnDisable()
        {
            InputController.OnInputChanged -= InputController_OnInputChanged;
            LevelManager.Instance.eventManager.OnBreakStageIsFinishAction -= OnBreakStageIsFinish;
        }

        private void Start()
        {
            GunGridManager.Instance.gunMovementObjectTransform = transform;
            CameraController.Instance.controller = this;
        }


        // Update is called once per frame
        void Update()
        {
            if (!_isMoveable || !GameState.Instance.IsPlaying())
            {
                return;
            }
            
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.right * _currentXPos * _sensivity, Time.deltaTime * 5);

            transform.position += _speed * Vector3.forward * Time.deltaTime;

            if(transform.position.x < -_borderIndex)
            {
                transform.position = new Vector3(-_borderIndex, transform.position.y, transform.position.z);
            }
            if (transform.position.x > _borderIndex)
            {
                transform.position = new Vector3(_borderIndex, transform.position.y, transform.position.z);
            }
        }
    }

}
