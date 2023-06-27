using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateFx.Managers
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        [SerializeField] private GameObject _world;
        public void SpawnWorld()
        {
            Instantiate(_world,LevelManager.Instance.enviromentHolderTransform);
        }
    }
}


