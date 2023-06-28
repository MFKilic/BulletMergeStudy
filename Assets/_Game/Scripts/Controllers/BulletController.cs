using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using UnityEngine;

namespace TemplateFx.Merge
{
    public class BulletController : MonoBehaviour
    {
        public int mergeIndex;
        public int bulletHealth;
        public BulletMergeType bulletMergeType;
        [SerializeField] BulletMergeMovementController _bulletMergeMovementController;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<MeshFilter>().mesh = bulletMergeType.bulletMesh;
            GetComponent<MeshRenderer>().material = bulletMergeType.bulletMaterial;
            bulletHealth = bulletMergeType.bulletHealth;
            mergeIndex = bulletMergeType.bulletMergeIndex;
        }

        public void BulletHealthChanged(int healthIndex)
        {
            bulletHealth -= healthIndex;
            if (bulletHealth <= 0)
            {
                if(GridManager.Instance.ObjectCounter() != 1)
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
