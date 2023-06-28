using System.Collections;
using System.Collections.Generic;
using TemplateFx.Controller;
using TemplateFx.Merge;
using UnityEngine;

namespace TemplateFx.Collision
{
    public class GunBulletCollisionController : MonoBehaviour
    {
        [SerializeField] private GunBulletController _gunBulletController;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IGate gate))
            {
                gate.OnUpgradeNumberIndex(_gunBulletController.damage);

            }
            if (other.TryGetComponent(out IHittable hittable))
            {
                hittable.Hit(_gunBulletController.damage);
                gameObject.SetActive(false);
            }
            if (other.TryGetComponent(out IObstacle obstacle))
            {
                gameObject.SetActive(false);
            }
        }
    }
}


