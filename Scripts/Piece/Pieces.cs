/*
  Author      藤澤典隆
  LastUpdate  2020/07/11
  Since       2020/03/16
  Contents    フィールドに設置された駒が持つclass 移動、回転、画面から削除、IDと名前をGetするメソッド
*/
using UnityEngine;
using GameManager;
using System.Collections.Generic;
using Field;
using Piece.Extend;

namespace Piece
{
    public class Pieces : MonoBehaviour, IPieces
    {
        public Camera.CameraBase PieceCamera;
        [SerializeField] private int Id = 100;
        private PieceKind Name;
        private int FaceId;
        private int ForwardFaceId;
        private int ShapeType;
        public bool isDebug = false;

        private void ForwardTest(int a, int b)
        {
            List<int> list = new List<int>();
            list.Add(a);
            list.Add(b);
            GameManager.ManagerStore.fieldManager.PointMovableFace(list);
        }

        //初期化関数　IdとName、向きを書き込む
        public void Init(int pieceId, PieceKind piecekind, int faceId, int forwardFaceId)
        {
            ForwardFaceId = forwardFaceId;
            PieceCamera = GetComponent<Camera.PlayCamera>();
            Id = pieceId;
            Name = piecekind;
            FaceId = faceId;
            ShapeType = this.transform.parent.gameObject.GetComponent<Field.SurfaceInfo>().SideNum;
            //transform.LookAt(ManagerStore.fieldManager.GetSurfaceInfoById(forwardFaceId).transform);
            //ManagerStore.fieldManager.AligmentObject(this.gameObject);
        }


        //PlayGameシーンで利用。PlayerBaseへの移行準備中
        //面の絶対Idを指定するとそこに駒が移動する
        public void MovePiece(int faceId)
        {
            GameManager.ManagerStore.fieldManager.DeleteFieldInfo(Id);
            GameManager.ManagerStore.fieldManager.SetPieceAsChild(faceId, this.gameObject);
            GameManager.ManagerStore.fieldManager.WriteFieldInfo(Id, faceId);
            GameObject surfacObj = GameManager.ManagerStore.fieldManager.GetFace2Face(faceId, FaceId);
            if (faceId != ForwardFaceId)
            {
                surfacObj = GameManager.ManagerStore.fieldManager.GetFace2Face(faceId, ForwardFaceId);
            }
            transform.LookAt(surfacObj.transform);
            ManagerStore.fieldManager.AligmentObject(this.gameObject);
            ForwardFaceId = surfacObj.GetComponent<Field.SurfaceInfo>().FaceId;
            ShapeType = this.transform.parent.gameObject.GetComponent<Field.SurfaceInfo>().SideNum;
            FaceId = faceId;
            //テストコード
            //ForwardTest(ForwardFaceId, ForwardFaceId);
        }

        //rotate辺回転する ex)rotateが-1で10角形なら-36度
        public void RotatePiece(int rotate)
        {
            int rotationAmount = (360 / GetShape()) * rotate;
            Quaternion rot = Quaternion.AngleAxis(rotationAmount, Vector3.up);
            Quaternion q = this.transform.rotation;
            this.transform.rotation = q * rot;
            ForwardFaceId = ManagerStore.fieldManager.ConvertRelative2AbsId(GetFaceId(), 0, GetForwardDirection());
            //ForwardTest(ForwardFaceId, ForwardFaceId);
        }


        //駒に振られた一意なIDを返す
        public int GetPieceId()
        {
            return Id;
        }

        //駒の種類を返す
        public PieceKind GetKind()
        {
            return Name;
        }

        //PieceManagerとPlayerに反映されるように注意が必要
        public void Delete()
        {
            Debug.Log("test");
            Destroy(this.gameObject);
        }


        //存在する面の絶対IDを返す
        public int GetFaceId()
        {
            return FaceId;
        }

        //何角形上にいるマスか返す
        public int GetShape()
        {
            return ShapeType; ;
        }

        //駒の向きを返す
        public Vector3 GetForwardDirection()
        {
            Transform parent = transform.parent;
            Transform granpa = parent.parent;

            var direction = transform.TransformDirection(Vector3.forward);
            direction = parent.TransformDirection(direction);
            //direction = granpa.TransformDirection(direction); 

            //Debug.DrawLine
            //(
            //    transform.position,
            //    transform.position + direction * 100f, Color.blue);
            return direction;
        }

        //駒が実際に向いている正面の面のIDを返す
        public int GetForwardFaceId()
        {
            return ForwardFaceId;
        }

        

        public void SetFaceId(int faceId) {
            FaceId = faceId;
            ShapeType = this.transform.parent.gameObject.GetComponent<Field.SurfaceInfo>().SideNum;
        }

        public void SetForwardFaceId(int forwardFaceId)
        {
            ForwardFaceId = forwardFaceId;
        }




        
    }

}