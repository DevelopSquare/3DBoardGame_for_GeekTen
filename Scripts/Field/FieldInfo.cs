/*
  Author      田中木介
  LastUpdate  2020/03/14
  Since       2020/03/11
  Contents    面をまとめて制御
              GameObjectにアッタッチする
              基本的にココのメソッドは使わない
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Field
{
    public class FieldInfo : MonoBehaviour
    {
        [SerializeField] private Transform FieldParent = default;

        [SerializeField] private GameObject[] ArrowObj = default; 

        private Transform[] FieldChildren;

        private Dictionary<int, SurfaceInfo> FieldInfoDict = new Dictionary<int, SurfaceInfo>();

        void Awake()
        {
            InitializeField();

            //int c = 0;

            //var rl = RelativeId(10, Vector3.forward);
            ////Debug.Log(rl.Count);
            //foreach (int i in rl)
            //{
            //    Debug.Log("Realative Face id = "+c + "Absolute id = "+i );
            //    c++;
            //}


        }

        void Update()
        {
        }

        public Transform Field
        {
            get { return FieldParent; }
        }


        public Dictionary<int, SurfaceInfo> FieldInfoDictionary
        {
        
            get
            {
                return FieldInfoDict;
            }
        }

        public List<int> NeighborFace(int faceId , Vector3 pieceDirection)
        {   
            var surfaceInfo = FieldInfoDictionary[faceId];

            List<int> neighborFace = surfaceInfo.GetFaces(pieceDirection);
            //Debug.Log(surfaceInfo.GetFaces(pieceDirection).Count);
            return neighborFace;
        }

        public List<int> RelativeId(int crrId,Vector3 pieceDirection )
        {
            //var  folderList = new List<int>() { }; 
            List<int> folderList = NeighborFace(crrId,pieceDirection);
            //folderList.AddRange(baseIds);
            for(int i=0; i < FieldInfoDictionary.Count;i++)
            {
              
                    folderList.AddRange(NeighborFace(folderList[i],Vector3.forward));
               
            }
            
            IEnumerable<int> d_l = folderList.Distinct();
            var rFaces = d_l.ToList();
            
            rFaces.Remove(crrId);
            return rFaces;

        }
        
        public void ActivateArrows(List<int> movableFace)
        {

            for(int i = 0; i < FieldInfoDictionary.Count; i++)
            {
                FieldInfoDictionary[i].ActivateArrow(false);

                if (movableFace.IndexOf(i) >= 0)
                {
//                    Debug.Log(i);
                    FieldInfoDictionary[movableFace[movableFace.IndexOf(i)]].ActivateArrow(true) ;
                }
                
            }
        }

        public void SetPiece2Child(int faceId , GameObject pieceObj)
        {
            FieldInfoDictionary[faceId].SetChild(pieceObj);
        }

        


        private void InitializeField()
        {

            SurfaceInfo surFaceInfo;
            int faceId = 0;
            Transform child;
            //Debug.Log("Initializing Surface Info");
            for (int i=0; i<FieldParent.childCount;i++)
            {
                child = FieldParent.GetChild(i);

                surFaceInfo = child.gameObject.GetComponent<SurfaceInfo>();
                if (surFaceInfo == null) continue;

                //Debug.Log(child.gameObject.name);
                surFaceInfo.FaceId = faceId;

                GameObject Arrow = default;

                switch (surFaceInfo.SideNum)
                {
                    case 4:
                        Arrow = ArrowObj[0];
                        break;
                    case 6:
                        Arrow = ArrowObj[1];
                        break;
                    case 10:
                        Arrow = ArrowObj[2];
                        break;
                    default:
                        Arrow = ArrowObj[0];
                        break;
                }

                FieldInfoDict[faceId] = surFaceInfo;

                faceId += 1;

                //Set Arrow as face' child 
                var arrowObj = Instantiate(Arrow,Vector3.zero, Quaternion.identity);

               

                arrowObj.transform.parent = child;
                arrowObj.SetActive(false); 
                arrowObj.transform.localPosition = Vector3.up*0.2f;
                arrowObj.name = "Arrow";
                //arrowObj.transform.localScale = new Vector3(50,100,50);

                

            }
        }


   
    }



}


