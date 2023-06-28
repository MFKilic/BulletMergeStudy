using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateFx.Controller
{
    public class CollectibleController : MonoBehaviour, ICollectible
    {
        private enum CollectibleTypes
        {
            Coin, Boost,Money
        }

        [SerializeField] private CollectibleTypes collectibleType;

        [SerializeField] private int _price = 25;
        [SerializeField] private Transform _collectibleTransform;

        public int CollectCollectibe()
        {
            gameObject.SetActive(false);
            return _price;
        }

        // Start is called before the first frame update
        void Start()
        {
            switch (collectibleType)
            {
                case CollectibleTypes.Coin:
                    _collectibleTransform.DOLocalRotate(new Vector3(0, 360, 90), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
                    break;
                case CollectibleTypes.Boost:

                    break;
            }
        }

        
    }
}


