using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using TemplateFx.Merge;
using System.Runtime.InteropServices.WindowsRuntime;

namespace TemplateFx.Managers
{
    public class MergeManager : Singleton<MergeManager>
    {
        public BulletMergeType[] bulletMergeTypes;

        public GameObject bulletGameObject;

        public SaveClassBullet saveClassBullets;


        public void OnBuyBulletButtonClicked()
        {
            GridClass[] gridClasses = GridManager.Instance.gridClasses;

            foreach (GridClass gridClass in gridClasses)
            {
               if(!gridClass.gridIsFull)
                {
                    CreateBullet(-1, gridClass.gridTransform);
                    break;
                }
            }

          
        }

        private void Start()
        {
            LoadBullets();
        }

        public void SaveBullets()
        {
            saveClassBullets = JsonSave.Read();

            GridClass[] saveGrid = GridManager.Instance.gridClasses;

            for(int i = 0 ; i < saveGrid.Length; i++)
            {
                saveClassBullets.listOfSaveBullets[i].isFull = saveGrid[i].gridIsFull;
                saveClassBullets.listOfSaveBullets[i].v3BulletPos = saveGrid[i].gridTransform.position;
                if (saveGrid[i].bulletController != null)
                {
                    saveClassBullets.listOfSaveBullets[i].bulletMergeLevel = saveGrid[i].bulletController.mergeIndex;
                }
              
            }

            JsonSave.Save(saveClassBullets);
        }

        private void LoadBullets()
        {
            saveClassBullets = JsonSave.Read();

            for (int i = 0; i < saveClassBullets.listOfSaveBullets.Count; i++)
            {
                if (saveClassBullets.listOfSaveBullets[i].isFull)
                {
                    CreateBullet(saveClassBullets.listOfSaveBullets[i].bulletMergeLevel - 1, saveClassBullets.listOfSaveBullets[i].v3BulletPos);
                }
            }
        }

        public void CreateBullet(int mergeIndex, Transform createTransform)
        {
            GameObject bulletObject = Instantiate(bulletGameObject, createTransform.position, createTransform.rotation, LevelManager.Instance.characterHolderTransform);
            if (bulletObject.TryGetComponent(out BulletController bulletController))
            {
                bulletController.bulletMergeType = bulletMergeTypes[mergeIndex + 1];
            }

          
        }
        public void CreateBullet(int mergeIndex, Vector3 createVector)
        {
            GameObject bulletObject = Instantiate(bulletGameObject, createVector, Quaternion.identity, LevelManager.Instance.characterHolderTransform);
            if (bulletObject.TryGetComponent(out BulletController bulletController))
            {
                bulletController.bulletMergeType = bulletMergeTypes[mergeIndex + 1];
            }

        }

    }
}

