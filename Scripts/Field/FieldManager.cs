/*
  Author      田中木介
  LastUpdate  2020/04/29
  Since       2020/03/11
  Contents    FieldManager
              GameObjectにアッタッチする
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Field
{
    public class FieldManager : MonoBehaviour,IField
    {
        //[SerializeField] private GameObject TestCube= default;
        [SerializeField] private int playerBasedFaceId=61;
        [SerializeField] private int cpBasedFaceId=50;
        [SerializeField] private int FaceId=50;

        private FieldInfo fieldInfo;
        private FieldPieceInfo fieldPieceInfo;

        int c=0;

        void Awake()
        {
            
            fieldInfo = GetComponent<FieldInfo>();
            fieldPieceInfo = this.GetComponent<FieldPieceInfo>();


            //Debug.Log(String.Join(",",GetPiecePlayerSettableFace()));

            //WriteFieldInfo(1,0);
            //Debug.Log("FieldPieceInfo    "+fieldPieceInfo);
            //Debug.Log("isPieceOnFace    "+IsPieceOnFace(4));

            //AligmentObject(TestCube);
        }

        void Start()
        {
            SetArrow();

            

        }


        /*For Debug*/
        void Update()
        {
            //var surFace= GetAdjacentSurface(32, TestCube.transform.TransformDirection(TestCube.transform.forward));
            //GetAdjacentSurface(8, TestCube.transform.TransformDirection(TestCube.transform.forward));
            //var faces=GetAdjacentSurface(FaceId, TestCube.transform.forward);
            //Debug.Log(String.Join(",",faces));

            //RotateObj(TestCube);


            //if (Input.GetKeyUp(KeyCode.LeftArrow))
            //{
            //    Debug.Log("Abs ID = 0 RelativeIs = "+c + "Id ="+ConvertRelative2AbsId(61,c, TestCube.transform.TransformDirection(TestCube.transform.forward)));
            //    c++;

            //}
            //// 右に移動
            //if (Input.GetKeyUp(KeyCode.RightArrow))
            //{
            //    Debug.Log("Abs ID = 0 RelativeIs = " + c + "Id =" + ConvertRelative2AbsId(61, c, TestCube.transform.TransformDirection(TestCube.transform.forward)));
            //    c--;
            //}
            //// 前に移動
            //if (Input.GetKeyUp(KeyCode.UpArrow))
            //{
            //}

            //Debug.Log("player : "+String.Join(",",GetPiecePlayerSettableFace()));
            //Debug.Log("cp : "+String.Join(",",GetPieceCPSettableFace()));

//            Debug.Log("CrrId = 46 oneId = 61 another = "+GetFace2Face(46,61).GetComponent<SurfaceInfo>().FaceId);
        }



        public Vector3 GetPositionFromId(int id)
        {
            /*
             TODO:Idから面の座標を取得
             Argument:面のId
             Return:面の座標
             */
            var info = fieldInfo.FieldInfoDictionary;
        
            return  info[id].FacePosition;
        }

        public void WriteFieldInfo(int pieceId, int faceId)
        {
            /*
             TODO:駒と面の紐づけ
             Argument:駒のId　面のId
             Return:none
             */
            fieldPieceInfo.FieldPieceInfoDict.Add(pieceId,faceId); 
        }

        public void DeleteFieldInfo(int pieceId)
        {
            /*
             TODO:駒と面の紐づけを解除
             Argument:駒のId　面のId
             Return:none
             */
            fieldPieceInfo.FieldPieceInfoDict.Remove(pieceId);
        }

        public int ReadFieldInfo(int pieceId)
        {
            /*
             TODO:駒と面の紐づけを参照
             Argument:駒のId　面のId
             Return:none
             */
            int fieldId;

            fieldId = fieldPieceInfo.FieldPieceInfoDict[pieceId];

            return fieldId;
        }

        public List<int> GetAdjacentSurface(int crrId, Vector3 FowardDirection)
        {
            /*
             TODO:面の周囲の面のIdを取得
             Argument:現在のマス　駒の向き
             Return:面のId（リスト）
             */
            var surface = fieldInfo.FieldInfoDictionary[crrId];
            var direction = ProjectionVec(FowardDirection,surface);

            //Debug.DrawLine
            //(
            //    surface.gameObject.transform.position,
            //    surface.gameObject.transform.position+direction*100f, Color.blue,100f);

            return fieldInfo.NeighborFace(crrId,direction);
        }

        public Vector3 ProjectionVec(Vector3 oriVec,SurfaceInfo face )
        {
            return Vector3.ProjectOnPlane(oriVec, face.Normal);
        }

        public void PointMovableFace(List<int> movableFace)
        {
            /*
             TODO:面上の矢印を有効化
             Argument:有効化する面のIdのリスト　有効化するかのフラグ
             Return:Project Vector on plane 
             */
            fieldInfo.ActivateArrows(movableFace);
        }

        public int ConvertRelative2AbsId(int crrId, int relativeId, Vector3 pieceDirection)
        {
            /*
             TODO:面の相対Idを絶対Idに変換
             Argument:絶対Id　相対Id　駒の向き
             Return:面の絶対Id
             */
            //Debug.LogWarning("pieceDir : " + pieceDirection);

            return fieldInfo.RelativeId(crrId,pieceDirection)[relativeId];
        }

        public void SetPieceAsChild(int faceId, GameObject pieceObj)
        {
            /*
             TODO:駒を面の子オブジェクトとする
             Argument:面のId　駒のGameObject
             Return:none
             */            
            fieldInfo.SetPiece2Child(faceId, pieceObj);

            AligmentObject(pieceObj);
            

        }

        public void AligmentObject(GameObject obj)
        {
            /*
             TODO:Vertical the GameObject to parent object .
             Argument: GameObject
             Return:none
             */
       
            var parentObj = obj.transform.parent.gameObject;

            var meshFilt = parentObj.GetComponent<MeshFilter>();
            if (meshFilt == null)
            {
                Debug.Log("Couldn't get surface mesh ");
                return;
            }

            var mesh = meshFilt.mesh;

            //Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;

            //obj.transform.up = normals[1];

            //obj.transform.up = new Vector3(normals[1].x,normals[1].y,normals[1].z);
            Vector3 look = obj.transform.forward;
            Quaternion up = Quaternion.LookRotation(normals[1], -look);
            Quaternion forward = up * Quaternion.Euler(90.0f, 0.0f, 0.0f);
            obj.transform.rotation = forward;

        }

        public void RotateObj(GameObject obj)
        {
            /*
             TODO:オブジェクトを回転
             Argument: GameObject
             Return:none
             */
            //AligmentObject(obj);
            // x軸を軸にして毎秒2度、回転させるQuaternionを作成（変数をrotとする）
            Quaternion rot = Quaternion.AngleAxis(2, Vector3.up);
            // 現在の自信の回転の情報を取得する。
            Quaternion q = obj.transform.rotation;
            // 合成して、自身に設定
            obj.transform.rotation = q * rot;
        }

        public int IsPieceOnFace(int faceId)
        {
            /*
                 TODO:指定されたマスに駒があるかどうか
                 Argument: マスの絶対Id
                 Return:駒のId　なければ-1
                 */
            if (fieldPieceInfo.FieldPieceInfoDict.ContainsValue(faceId))
            {
                var v= fieldPieceInfo.FieldPieceInfoDict.FirstOrDefault(c => c.Value == faceId);
                var key = v.Key;
                return key;
            }

            return -1;
        }

        public List<int> GetPieceCPSettableFace()
        {
            /*
             TODO:cpBasedFaceIdを極とした11面を求める
             Argument: none
             Return:極を最初に持った11の面のId
             */
            var l = new List<int>();
            l.Add(cpBasedFaceId);

            l.AddRange(GetAdjacentSurface(cpBasedFaceId, Vector3.forward));

            return l;
        }

        public List<int> GetPiecePlayerSettableFace()
        {
            /*
             TODO:playerBasedFaceIdを極とした11面を求める
             Argument: none
             Return:極を最初に持った11の面のId
             */
            var l = new List<int>();
            l.Add(playerBasedFaceId);

            l.AddRange(GetAdjacentSurface(playerBasedFaceId,Vector3.forward));

            return l;
        }

        public GameObject GetFace2Face(int crrId,int onesideFaceId)
        {
            /*
             TODO:ある面の対になる面のIdを求める
             Argument: 現在位置の面のId　　、　ある面のId
             Return:onesideFaceIdとcrrIdの面に対して対になる面のId
             */
            
            var crrSurface = fieldInfo.FieldInfoDictionary[crrId];
            var oneSurface= fieldInfo.FieldInfoDictionary[onesideFaceId];

            var facesList = crrSurface.GetFaces(Vector3.forward);

            int oneFaceId = facesList.IndexOf(onesideFaceId);

            var faceList_ = new List<int>();
            for(int i = 0; i < facesList.Count; i++)
            {
                var faceIndex = (int)Mathf.Repeat(oneFaceId+i, facesList.Count);//Mathf.Clamp(oneFaceId + i, 0f, (float)(facesList.Count - 1));
                //Debug.Log("lol index : " + faceIndex+" : "+ (oneFaceId + i));
                faceList_.Add(facesList[faceIndex]);
            }

            return fieldInfo.FieldInfoDictionary[faceList_[crrSurface.SideNum/2]].gameObject;
        }

        public SurfaceInfo GetSurfaceInfoById(int faceId)
        {
            return fieldInfo.FieldInfoDictionary[faceId];

        }

       

        /*Don't Touch The Code Below !!*/

        private void SetArrow()
        {
            

            var childTransform = fieldInfo.Field.gameObject.GetComponentsInChildren<Transform>();
            //Debug.Log(transform);

            foreach (Transform child in childTransform)
            {
                //Debug.Log(child.gameObject);
                //child is your child transform
                Transform arrow = child.Find("Arrow");
                if (arrow!=null&& arrow.gameObject.name == "Arrow")
                {
                    
                    AligmentObject(arrow.gameObject);
                }
                
            }
        }

        

        
    }

    
    

}


