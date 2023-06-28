using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using TMPro;
using UnityEngine;

namespace TemplateFx.Merge
{
    public class BulletController : MonoBehaviour
    {
        public int mergeIndex;
        public int bulletHealth;
        public BulletMergeType bulletMergeType;
        [SerializeField] private BulletMergeMovementController _bulletMergeMovementController;
        [SerializeField] private TextMeshPro _bulletLevelText;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<MeshFilter>().mesh = bulletMergeType.bulletMesh;
            GetComponent<MeshRenderer>().material = bulletMergeType.bulletMaterial;
            bulletHealth = bulletMergeType.bulletHealth;
            mergeIndex = bulletMergeType.bulletMergeIndex;
            _bulletLevelText.text = (mergeIndex + 1).ToString();
        }

        public void BulletHealthChanged(int healthIndex)
        {
            bulletHealth -= healthIndex;
            if (bulletHealth <= 0)
            {
                if (GridManager.Instance.ObjectCounter() != 1)
                {
                    bulletHealth = 0;
                    BulletDestroyer();
                }


            }
        }

        public void BulletDestroyer()
        {
            _bulletMergeMovementController.RemoveObjectToGrid();
            if (LevelManager.Instance.eventManager.GetStage() == GameStage.MERGE)
            {
                LevelManager.Instance.eventManager.OnBreakBullet();
            }


            Destroy(gameObject);
        }
    }

}
