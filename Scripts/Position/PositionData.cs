/*
  Author      小路重虎
  LastUpdate  2020/05/09
  Since       2020/05/09
  Contents    盤面保存用のクラス
              実質PositionObjectsのMemento
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;
using Piece;
using Field;

namespace Position
{
    public class PositionData
    {
        public List<PiecePosition> piecePositions;

        public PositionData()
        {
            piecePositions = new List<PiecePosition>();
        }

        /// <summary>新しいPieceDataを生成するstatic関数</summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>入力PlayerBaseに対応した局面を再現できるPositonData</returns>
        public static PositionData SaveByPlayer(PlayerBase p1, PlayerBase p2)
        {
            PositionData pd = new PositionData();

            List<PlayerBase> playerList = new List<PlayerBase>() { p1, p2 };
            foreach(PlayerBase player in playerList)
            {
                List<Pieces> myPieces = player.GetMyPieces();
                int owner;
                if (player.GetPlayerKind() == PlayerKind.HumanPlayer)//
                {
                    owner = 0;
                }
                else
                {
                    owner = 1;
                }

                foreach (Pieces myPiece in myPieces)
                {
                    PiecePosition piecePosition = new PiecePosition();
                    piecePosition.owner = owner;
                    piecePosition.pieceKind = myPiece.GetKind();
                    piecePosition.faceId = myPiece.GetFaceId();
                    piecePosition.forwardFaceId = myPiece.GetForwardFaceId();

                    pd.piecePositions.Add(piecePosition);
                }
            }

            return pd;
        }
    }
}

