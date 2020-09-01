/*
  Author      森拓哉
  LastUpdate  2020/04/06
  Since       2020/03/23
  Contents BestMoveとして返す用のクラス。駒のid,駒の向き・駒の移動さき（相対id）を持つ
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CP
{
    class CPMove
    {
      private int pieceId;
      private int moveFaceId;
      private int rotateDirection;

      //コンストラクタ
      public CPMove()
      {
        this.pieceId = -1;
        this.moveFaceId = -1;
        this.rotateDirection = 0; //　プラマイ整数だとどっか向く
      }

      //駒のid(pieceId)を返す
      public int GetMovePieceId()
      {
        return this.pieceId;
      }

        public void SetMovePieceId(int pieceId)
        {
            this.pieceId = pieceId;
        }

      //面の相対id(moveFaceId)を返す
      public int GetMoveFaceId()
      {
        return this.moveFaceId;
      }
        public void SetMoveFaceId(int moveFaceId)
        {
            this.moveFaceId = moveFaceId; 
        }

      //方向を返す
       public int GetRotateDirection()
      {
        return this.rotateDirection;
      }

        public void SetRotateDirection(int rotateDirection)
        {
            this.rotateDirection = rotateDirection;
        }
    }
}
