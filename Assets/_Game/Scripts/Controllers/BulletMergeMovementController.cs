using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TemplateFx.Managers;
using UnityEngine;


namespace TemplateFx.Merge
{

    public class BulletMergeMovementController : MonoBehaviour
    {
        private const string strMergeParticle = "MergeParticle";
        private const string strMergeSound = "MergeSound";
        [SerializeField] BulletController bulletController;
        GridClass choosenPlatform;
        GridClass oldChoosenPlatform;
        Transform choosenGrid;
        Transform currentTransform;
        bool returnToPlace = false;
        // Start is called before the first frame update
        void Start()
        {
            FindNearLocation();
            Vector3 endVector = new Vector3(choosenGrid.position.x, 1, choosenGrid.position.z);
            transform.DOMove(endVector, 0.2f);
            transform.DORotate(choosenGrid.eulerAngles, 0.2f);
        }

        private void DoFixer()
        {
            transform.localScale = Vector3.one;
        }

        public void SelectTheBullet()
        {
            FindNearLocation(true);

            transform.DOShakeScale(0.3f, 0.3f, 5, 90).SetEase(Ease.InOutBounce).OnComplete(DoFixer);

            currentTransform = choosenGrid;
        }

        public void ConfigureToTheGround()
        {
            FindNearLocation();
            Vector3 endVector = new Vector3(choosenGrid.position.x, 1, choosenGrid.position.z);
            transform.DOMove(endVector, 0.2f);
            transform.DORotate(choosenGrid.eulerAngles, 0.2f);


        }




        private void FindNearLocation(bool isSelect = false)
        {
            GridManager gridManager = GridManager.Instance;

            GridClass[] grids = gridManager.gridClasses;

            returnToPlace = false;

            float maxDistance = 1000;

            float currentDistance;

            foreach (GridClass gridClass in grids)
            {
                currentDistance = Vector3.Distance(gridClass.gridTransform.position, transform.position);
                if (currentDistance < maxDistance)
                {
                    maxDistance = currentDistance;

                    choosenPlatform = gridClass;
                }
            }

            if (isSelect)
            {
                choosenPlatform.bulletController = bulletController;
                oldChoosenPlatform = choosenPlatform;
            }
            else
            {
                if (choosenPlatform.gridIsFull)
                {

                    if (bulletController.mergeIndex == choosenPlatform.bulletController.mergeIndex && bulletController.mergeIndex < MergeManager.Instance.bulletMergeTypes.Length - 1)
                    {
                        if (bulletController != choosenPlatform.bulletController)
                        {
                            RemoveObjectToGrid(true);

                            GameObject mergeParticle =  PoolManager.Instance.GetPooledObject(strMergeParticle);
                            SoundManager.Instance.SoundPlay(strMergeSound);
                            mergeParticle.transform.position = transform.position;

                            if(mergeParticle.TryGetComponent(out ParticleSystem ps))
                            {
                                ps.Play();
                            }

                            MergeManager.Instance.CreateBullet(bulletController.mergeIndex, choosenPlatform.gridTransform);

                            Destroy(gameObject);
                        }
                    }
                    else
                    {
                        choosenGrid = currentTransform;


                        returnToPlace = true;

                    }
                }
                else
                {
                    ChoosenGrid();
                }
            }
        }

        public void RemoveObjectToGrid(bool isGridRemove = false)
        {
            if(isGridRemove)
            {
                Destroy(choosenPlatform.bulletController.gameObject);
            }
         
           
            choosenPlatform.gridIsFull = false;
            choosenPlatform.bulletController = null;
            if (oldChoosenPlatform != null)
            {
                oldChoosenPlatform.gridIsFull = false;
                oldChoosenPlatform.bulletController = null;
            }

            if (!isGridRemove)
            {
                LevelManager.Instance.eventManager.OnBreakBullet();
            }
        }

        private void ChoosenGrid()
        {
            if (oldChoosenPlatform != null)
            {
                oldChoosenPlatform.gridIsFull = false;
                oldChoosenPlatform.bulletController = null;
            }
            choosenGrid = choosenPlatform.gridTransform;
            oldChoosenPlatform = choosenPlatform;
            choosenPlatform.bulletController = bulletController;

            choosenPlatform.gridIsFull = true;
            MergeManager.Instance.SaveBullets();
            LevelManager.Instance.eventManager.OnBulletPosChanged();
        }


    }

}
