/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/05/09
  Contents    盤面再現用のインターフェース
              PositionObjectsクラスにつける
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Move;
using Player;
using Field;
using Piece;

namespace Position
{
    public interface IPosition
    {
        PlayerBase GetPlayer(int n);
        FieldManager GetFieldManager();
        PiecesManager GetPiecesManager();

        //指し手を進める
        void Move(IForwardMove move);
        //指し手を戻す
        void MoveBack(IReversibleMove move);
        //局面保存
        PositionData Save();
        //局面読み込み
        void Load(PositionData pd);
    }

}
