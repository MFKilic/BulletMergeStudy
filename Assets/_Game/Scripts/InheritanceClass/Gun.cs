using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TemplateFx
{

    public abstract class Gun : MonoBehaviour
    {
        public Transform[] fireTransforms;

        public GameObject bulletObject;

        public float firePerSecond;

        public float bulletScaleIndex = 0.25f;

        public int fireBulletCount = 1;

        public float lifeTime = 1;
        public abstract void Fire();
    }

}


