using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateFx.Controller
{
    public class BreakeableWallController : BreakeableObjects, IBreakeable
    {
        [SerializeField] private BreakeableType[] _breakeableTypes = null;
        private int _randomNumber;
        private int _breakeableNumber;
        private int _priceNumber;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        public int OnPrice()
        {
            return _priceNumber;
        }

        public Material BreakeableMaterial()
        {
            return _meshRenderer.material;
        }

        public Mesh BreakeableMesh()
        {
            return _meshFilter.mesh;
        }

        public int OnBreak()
        {
            OnBreakBehavior();

            return _breakeableNumber;
        }

        public override void OnBreakBehavior()
        {
            gameObject.SetActive(false);
        }


        void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _randomNumber = Random.Range(0, _breakeableTypes.Length);
            _meshFilter.mesh = _breakeableTypes[_randomNumber].breakeableMesh;
            _meshRenderer.material = _breakeableTypes[_randomNumber].breakeableMaterial;
            _breakeableNumber = _breakeableTypes[_randomNumber].breakeableDamageIndex;
            _priceNumber = _breakeableTypes[_randomNumber].breakeablePriceIndex;
        }

    }

}

