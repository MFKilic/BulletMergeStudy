using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateFx.Controller
{
    public class BoostController : MonoBehaviour , IBoost
    {
        [SerializeField] private BoostTypes _type;

        public BoostTypes BoostType()
        {
            gameObject.SetActive(false);
            return _type;
        }

        
    }
}

