/*
  Author      小路重虎
  LastUpdate  2020/06/18
  Since       2020/03/11
  Contents    プレイヤー情報に関するスクリプト
              PlayerBaseクラス
*/
/*
  GameManager.ManagerStore.Awake()でpiecesManager,fieldManagerのセットが済んでいること
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Piece;
using Field;
using Effect;

namespace Player
{
    public class PlayerBase : MonoBehaviour, IPlayer
    {
        static private int PieceNum = 0;

        private Dictionary<int, Pieces> myPieces = new Dictionary<int, Pieces>();
        private PlayerKind playerKind;
        private FieldManager fieldManager = null;
        private PiecesManager piecesManager = null;

        void Awake()
        {
            Init();
        }

        void Start()
        {
            if (fieldManager == null && piecesManager == null)
            {
                SetManager(GameManager.ManagerStore.fieldManager, GameManager.ManagerStore.piecesManager);
            }
        }

        //PlayerIDを設定する
        public void SetPlayerKind(PlayerKind playerKind)
        {
            this.playerKind = playerKind;
        }

        //FieldManager,PiecesManagerを置き換える
        public void SetManager(FieldManager fieldManager, PiecesManager piecesManager)
        {
            this.fieldManager = fieldManager;
            this.piecesManager = piecesManager;
        }

        //プレイヤーIDを取得
        public PlayerKind GetPlayerKind()
        {
            return playerKind;
        }

        //駒をプレイヤーのものとして登録
        public void AddMyPieces(PieceKind pieceKind, int absoluteFaceId, int forwardFaceId, bool withAnimation = true)
        {
            //Debug.Log($"addmyp kind: {pieceKind}  faceID: {absoluteFaceId}  playerkind: {playerKind}");

            GameObject piecePrefab = piecesManager.GetPiecePrefab(pieceKind,playerKind);
            GameObject newPieceObject = Instantiate(piecePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Pieces newPiece = newPieceObject.GetComponent<Pieces>();

            if (withAnimation == false)
            {
                newPiece.NotCreateEffect();
            }

            fieldManager.WriteFieldInfo(PieceNum, absoluteFaceId);
            fieldManager.SetPieceAsChild(absoluteFaceId, newPieceObject);

            newPiece.Init(PieceNum, pieceKind, absoluteFaceId, forwardFaceId);

            newPieceObject.transform.LookAt(fieldManager.GetSurfaceInfoById(forwardFaceId).transform);
            fieldManager.AligmentObject(newPieceObject);

            myPieces.Add(newPiece.GetPieceId(), newPiece);
            PieceNum += 1;
        }

        //駒を削除する
        public void DeleteMyPieces(int pieceId)
        {
            GetPieceById(pieceId).Delete();
            fieldManager.DeleteFieldInfo(pieceId);
            myPieces.Remove(pieceId);
        }

        //面の絶対Idを指定するとそこに駒が移動する
        public void MovePiece(int pieceId, int faceId)
        {
            Pieces piece = GetPieceById(pieceId);
            GameObject pieceObject = piece.gameObject;
            int FaceId = piece.GetFaceId();

            fieldManager.DeleteFieldInfo(pieceId);
            fieldManager.SetPieceAsChild(faceId, pieceObject);
            fieldManager.WriteFieldInfo(pieceId, faceId);

            GameObject surfacObj = fieldManager.GetFace2Face(faceId, FaceId);
            piece.transform.LookAt(surfacObj.transform);
            fieldManager.AligmentObject(piece.gameObject);

            int forwardFaceId = surfacObj.GetComponent<Field.SurfaceInfo>().FaceId;
            
            piece.SetFaceId(faceId);
            piece.SetForwardFaceId(forwardFaceId);
        }

        //駒をrotate辺回転する　ex)rotateが-1で10角形なら-36度
        public void RotatePiece(int pieceId, int rotate)
        {
            Pieces piece = GetPieceById(pieceId);

            int rotationAmount = (360 / piece.GetShape()) * rotate;
            Quaternion rot = Quaternion.AngleAxis(rotationAmount, Vector3.up);
            Quaternion q = piece.transform.rotation;
            piece.transform.rotation = q * rot;

            int forwardFaceId = fieldManager.ConvertRelative2AbsId(piece.GetFaceId(), 0, piece.GetForwardDirection());

            piece.SetForwardFaceId(forwardFaceId);
        }

        //プレイヤーが持っている駒を取得
        public List<Pieces> GetMyPieces()
        {
            List<Pieces> pieces = new List<Pieces>(myPieces.Values);
            return pieces;
        }

        //駒の種類を限定してプレイヤーが持っている駒を取得
        public List<Pieces> GetMyPiecesByKind(PieceKind piecekind)
        {
            List<Pieces> list=new List<Pieces>();
            foreach (Pieces piece in myPieces.Values)
            {
                if (piece.GetKind() == piecekind)
                {
                    list.Add(piece);
                }
            }
            return list;
        }

        //IDから駒を取得
        public Pieces GetPieceById(int pieceId)
        {
            if (HasPiece(pieceId))
            {
                return myPieces[pieceId];
            }
            else
            {
                return null;
            }
        }

        //そのIDの駒を持っているかどうか
        public bool HasPiece(int pieceId)
        {
            return myPieces.ContainsKey(pieceId);
        }

        //Destroyの前に必ず実行
        public void ClearPieces()
        {
            //駒オブジェクト、盤面情報を全削除
            foreach (int id in myPieces.Keys)
            {
                fieldManager.DeleteFieldInfo(id);
                GetPieceById(id).Delete();
            }
            myPieces.Clear();
        }

     //ここからprivate

        //クラスを初期化する
        private void Init()
        {
            myPieces = new Dictionary<int, Pieces>();
        }
    }
}

