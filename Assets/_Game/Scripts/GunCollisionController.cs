using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using TemplateFx.Merge;
using UnityEngine;

namespace TemplateFx
{
    public class GunCollisionController : MonoBehaviour
    {
    

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IGate gate))
            {
              
                if (other.TryGetComponent(out GateController gateController))
                {
                    GateEnum gateEnum = gateController.gateType;

                    LevelManager.Instance.eventManager.OnGateIsCollected(gateEnum,gate);
            
                }


            }
            if (other.TryGetComponent(out IHittable hittable))
            {
                GameState.Instance.OnFinishGame(LevelFinishStatus.WIN);
            }


            if (other.TryGetComponent(out IObstacle obstacle))
            {
                GameState.Instance.OnFinishGame(LevelFinishStatus.LOSE);
            }

        }
    }

}

