using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using UnityEngine;

namespace TemplateFx
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _rocketPrefab;
        private WaitForSeconds _spawnTime = new WaitForSeconds(5);

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(RocketSpawnTimer());
        }

        private IEnumerator RocketSpawnTimer()
        {
            for (; ; )
            {
                yield return _spawnTime;
                Instantiate(_rocketPrefab, transform.position, Quaternion.identity, transform);
            }

        }

    }
}


