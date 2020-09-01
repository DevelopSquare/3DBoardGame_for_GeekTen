/*
  Author      森友雅
  LastUpdate  2020/05/31
  Since       2020/03/16
  Contents    N GameSetUp
              取得したい情報を実行するスクリプト
*/

using UnityEngine;

namespace GameManager.GameSetUp
{
    public class GetInfoManager
    {

        /// <summary>
        /// マスのIDから駒のポイントを取得
        /// </summary>
        /// <param name="squareId"></param>
        /// <returns></returns>
        /// 
        public int PiecePoint(int squareId)
        {     
            int point = 0;
            int pieceId = -1;
            PieceKind pieceKind = 0;

            if (squareId >= 0)
            {
                pieceId = ManagerStore.fieldManager.IsPieceOnFace(squareId);
                if (pieceId != -1)
                {
                    Piece.Pieces piece = ManagerStore.humanPlayer.GetPieceById(pieceId);
                    pieceKind = piece.GetKind();
                    point = (int)ManagerStore.piecesManager.GetSummonCost(pieceKind);
                }
            }
            else if(squareId < -1)
            {
                if(squareId == -2)
                {
                    pieceKind = global::PieceKind.Pawn;
                }
                else if(squareId == -5)
                {
                    pieceKind = global::PieceKind.Queen;
                }

                point = (int)ManagerStore.piecesManager.GetSummonCost(pieceKind);
            }

            return point;
        }


        /// <summary>
        /// pieceIDからから駒の種類を取得
        /// </summary>
        /// <param name="pieceId"></param>
        /// <returns></returns>
        /// 
        public PieceKind PieceKind(int pieceId)
        {
            
            PieceKind pieceKind = 0;

            if (pieceId == -2)
            {
                pieceKind = global::PieceKind.Pawn;
            }
            else if (pieceId == -5)
            {
                pieceKind = global::PieceKind.Queen;
            }
            else if (pieceId == -10)
            {
                pieceKind = global::PieceKind.King;
            }
            return pieceKind;
        }


        /// <summary>
        /// squareIDから駒の種類を既定のIDで取得
        /// </summary>
        /// <param name="squareId"></param>
        /// <returns></returns>
        /// 
        public int PieceId(int squareId)
        {
            int returnPieceId = -1;
            int pieceId;
            PieceKind pieceKind;
            Piece.Pieces piece;

            pieceId = ManagerStore.fieldManager.IsPieceOnFace(squareId);
            piece = ManagerStore.humanPlayer.GetPieceById(pieceId);
            pieceKind = piece.GetKind();

            if (pieceKind == global::PieceKind.Pawn)
            {
                returnPieceId = -2;
            }
            else if (pieceKind == global::PieceKind.Queen)
            {
                returnPieceId = -5;
            }

            return returnPieceId;
        }


        /// <summary>
        /// pieceIdからGameObjectを取得
        /// </summary>
        /// <param name="pieceId"></param>
        /// <returns></returns>
        public GameObject GetPieceObj(int pieceId)
        {
            Piece.Pieces piece;

            piece = ManagerStore.humanPlayer.GetPieceById(pieceId);

            return piece.gameObject;

        }

        
    }
}
