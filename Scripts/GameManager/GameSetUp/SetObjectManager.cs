/*
  Author      森友雅
  LastUpdate  2020/08/30（髙橋　召喚時の演出のために,一度Allyを見えなくする）
  Since       2020/03/16
  Contents    N GameSetUp
              駒を設置(取り消し)する際に利用する
*/

using Effect;
using Field;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace GameManager.GameSetUp
{
    public class SetObjectManager : MonoBehaviour
    {
        // 使用スクリプト
        GetInfoManager getinfo = new GetInfoManager();
        private int[] settableFace = new int[11];

      

        /// <summary>
        /// 設置済みの駒の取り消し
        /// </summary>
        /// <param name="squareId"></param>
        /// 
        public void RemovePiece(int squareId)
        {
            int pieceId;
            Piece.Pieces piece;

            pieceId = ManagerStore.fieldManager.IsPieceOnFace(squareId);
            piece = ManagerStore.humanPlayer.GetPieceById(pieceId);
            piece.DestroyEffect();
            ManagerStore.humanPlayer.DeleteMyPieces(pieceId);
        }


        /// <summary>
        /// 全ての設置済みの駒の取り消し
        /// </summary>
        /// <param name="squareId"></param>
        /// 
        public void RemoveAllPieces()
        {
            int[] settableSquare = { 21, 22, 23, 24, 25, 45, 46, 47, 48, 49 };
            int pieceId;
            for (int i = 0; i < 10; i++)
            {
                if (ManagerStore.fieldManager.IsPieceOnFace(settableSquare[i]) != -1)
                {
                    pieceId = ManagerStore.fieldManager.IsPieceOnFace(settableSquare[i]);
                    ManagerStore.humanPlayer.DeleteMyPieces(pieceId);
                }
            }
        }

        /// <summary>
        /// マスに駒を設置する
        /// </summary>
        /// <param name="squareId"></param>
        /// <param name="pieceId"></param>
        /// 
        public void SetPiece(int squareId, int pieceId)
        {
            PieceKind pieceKind;
            int forwardFaceId;
            Piece.Pieces piece;

            pieceKind = getinfo.PieceKind(pieceId);
            forwardFaceId = ManagerStore.fieldManager.GetFace2Face(squareId, 61).GetComponent<SurfaceInfo>().FaceId;
            ManagerStore.humanPlayer.AddMyPieces(pieceKind, squareId, forwardFaceId);
            ColliderOff(ManagerStore.fieldManager.IsPieceOnFace(squareId));
            pieceId = ManagerStore.fieldManager.IsPieceOnFace(squareId);
            piece = ManagerStore.humanPlayer.GetPieceById(pieceId);

            //召喚時の演出のために,一度Allyを見えなくする
            piece.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);

            piece.CreateEffect();
        }


        /// <summary>
        /// 選択マスが利用可能マスか
        /// </summary>
        /// <param name="squareId"></param>
        /// <returns></returns>
        /// 
        public Boolean CanBeUsed(int squareId)
        {
            var l = new List<int>();

            l = ManagerStore.fieldManager.GetPiecePlayerSettableFace();
            for (int i = 1; i < l.Count(); i++)
            {
                if (l[i] == squareId)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// pieceIdからコライダーをオフにする
        /// </summary>
        /// <param name="pieceId"></param>
        /// <returns></returns>
        public void ColliderOff(int pieceId)
        {
            GameObject pieceObj;

            pieceObj = getinfo.GetPieceObj(pieceId);

            pieceObj.GetComponent<BoxCollider>().enabled = false;
        }


        /// <summary>
        /// 全てのコライダーをオンにする
        /// </summary>
       public void ColiiderOn()
        {
            int[] settableFace = { 21, 22, 23, 24, 25, 45, 46, 47, 48, 49, 61 };
            int pieceId;
            Piece.Pieces piece;
            GameObject pieceObj;

            for (int i = 0; i < 11; i++)
            {
                if (ManagerStore.fieldManager.IsPieceOnFace(settableFace[i]) != -1)
                {
                    pieceId = ManagerStore.fieldManager.IsPieceOnFace(settableFace[i]);
                    piece = ManagerStore.humanPlayer.GetPieceById(pieceId);
                    pieceObj = getinfo.GetPieceObj(pieceId);
                    piece.gameObject.GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
    }
}
