using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateFx.Controller
{
    public class GunGridController : MonoBehaviour
    {
        [SerializeField] private GunController _gunController;
        private bool _isGunSet;
        public void SetTheGun()
        {
            if (_isGunSet)
            {
                return;
            }
            _isGunSet = true;
            _gunController.isActive = true;
            GunGridManager.Instance.SetGunTransforms(transform);
        }
    }

}

