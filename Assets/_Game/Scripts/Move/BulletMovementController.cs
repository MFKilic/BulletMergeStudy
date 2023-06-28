using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using TemplateFx.Merge;
using UnityEngine;

namespace TemplateFx.Move
{
    public class BulletMovementController : MonoBehaviour
    {
        [SerializeField] float speed = 10;
        [SerializeField] float zBorder = 32;
        [SerializeField] private LeanSelectable _leanSelectable;
        [SerializeField] private LeanDragTranslateAlong _leanDragTranslateAlong;
        bool isMove;

        private void OnEnable()
        {
            LevelManager.Instance.eventManager.OnMergeStageIsFinishAction += OnMergeStage;
        }

        private void OnMergeStage()
        {
            _leanSelectable.enabled = false;
            _leanDragTranslateAlong.enabled = false;
            isMove = true;
        }

        private void OnDisable()
        {
            LevelManager.Instance.eventManager.OnMergeStageIsFinishAction -= OnMergeStage;
        }
      
   
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                isMove = false;
            }

            if(!isMove)
            {
                return;
            }

            if(zBorder < transform.position.z)
            {
                isMove = false;
            }

            transform.position += Vector3.forward * speed * Time.deltaTime;

        }

       
    }

  


}

