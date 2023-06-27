using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TemplateFx.GameState;

namespace TemplateFx
{
    public enum GameStage
    {
        NONE,
        MERGE,
        BREAK,
        RUNNER
    }
    public class EventManager : MonoBehaviour
    {
       
        private GameStage _stage;

        public delegate void OnFirstInputDelegate();
        public event OnFirstInputDelegate OnFirstInputEvent;

        public Action OnBulletPosChangedAction;

        public Action OnBreakBulletAction;

        public Action OnMergeStageIsFinishAction;

        public Action OnBreakStageIsFinishAction;

        public Action OnRunnerStageIsFinishAction;

        public Action<GateEnum, IGate> OnGateIsCollectedAction;

        public Action<float> OnBulletHitTheGunAction;

        public GameStage GetStage()
        {
            return _stage;
        }

        public void OnFirstInputIsPressed()
        {
            if (_stage != GameStage.MERGE)
            {
                _stage = GameStage.MERGE;
                OnFirstInputEvent?.Invoke();
            }

        }

        public void OnBulletPosChanged()
        {
            OnBulletPosChangedAction?.Invoke();
        }

        public void OnBreakBullet()
        {
            OnBreakBulletAction?.Invoke();
        }

        public void OnMergeStageIsFinish()
        {
            if (_stage != GameStage.BREAK)
            {
                _stage = GameStage.BREAK;
                OnMergeStageIsFinishAction?.Invoke();
            }
        }

        public void OnBreakStageIsFinish()
        {
            if (_stage != GameStage.RUNNER)
            {
                _stage = GameStage.RUNNER;
                OnBreakStageIsFinishAction.Invoke();
            }

        }

        public void OnRunnerStageIsFinish()
        {
            OnRunnerStageIsFinishAction?.Invoke();
        }

        public void OnGateIsCollected(GateEnum gateEnum, IGate gate)
        {
            OnGateIsCollectedAction?.Invoke(gateEnum, gate);
        }

        public void OnBulletHitTheGun(float xPos)
        {
            OnBulletHitTheGunAction?.Invoke(xPos);
        }


    }
}

