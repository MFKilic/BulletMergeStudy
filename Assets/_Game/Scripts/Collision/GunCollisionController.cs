using System.Collections;
using System.Collections.Generic;
using TemplateFx.Controller;
using TemplateFx.Managers;
using TemplateFx.Merge;
using UnityEngine;

namespace TemplateFx.Collision
{
    public class GunCollisionController : MonoBehaviour
    {
        private const string strWin = "Win";
        private const string strGateCollect = "GateCollect";
        private const string strPop = "Pop";

        [SerializeField] private GunController _gunController;
        [HideInInspector] public bool isImmune;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IGate gate))
            {

                if (other.TryGetComponent(out GateController gateController))
                {
                    GateEnum gateEnum = gateController.gateType;

                    SoundManager.Instance.SoundPlay(strGateCollect);
                    LevelManager.Instance.eventManager.OnGateIsCollected(gateEnum, gate);

                }


            }
            if (other.TryGetComponent(out IHittable hittable))
            {
                GameState.Instance.OnFinishGame(LevelFinishStatus.WIN);
            }

            if (!isImmune)
            {
                if (other.TryGetComponent(out IObstacle obstacle))
                {
                    GameState.Instance.OnFinishGame(LevelFinishStatus.LOSE);
                }

                if (other.TryGetComponent(out IBoost boost))
                {
                    SoundManager.Instance.SoundPlay(strPop);
                    LevelManager.Instance.eventManager.OnBoostCollected(boost.BoostType());

                }
            }

            if(other.CompareTag(strWin))
            {
                GameState.Instance.OnFinishGame(LevelFinishStatus.WIN);
            }

            if (other.TryGetComponent(out ICollectible collectible))
            {
                SoundManager.Instance.SoundPlay(strPop);
                UIManager.Instance.viewPlay.OnMoneyChange(collectible.CollectCollectibe());
            }




        }
    }

}

