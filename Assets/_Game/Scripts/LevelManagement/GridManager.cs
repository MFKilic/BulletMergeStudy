using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using TemplateFx.Merge;

namespace TemplateFx.Managers
{
    [System.Serializable]
    public class GridClass
    {
        public Transform gridTransform;
        public BulletController bulletController;
        public bool gridIsFull;
    }

    public class GridManager : Singleton<GridManager>
    {
        public GridClass[] gridClasses;
        public int objectCount;

        private void Start()
        {
           
        }

        public int ObjectCounter()
        {
            objectCount = 0;
            foreach (GridClass gridClass in gridClasses)
            {
                if (gridClass.gridIsFull)
                {
                    objectCount++;
                }
            }

            return objectCount;
        }

        public GameObject GetFrontFollowObject()
        {
  
            float gridZPos = -1000;
            objectCount = 0;
            GameObject followObject = null;
            foreach (GridClass gridClass in gridClasses)
            {
                if (gridClass.gridIsFull)
                {
                    objectCount++;
                    if (gridClass.bulletController.transform.position.z > gridZPos)
                    {
                        gridZPos = gridClass.bulletController.transform.position.z;
                        followObject = gridClass.bulletController.transform.gameObject;
                    }
                }
            }

            
          
            return followObject;
        }
    }
}


