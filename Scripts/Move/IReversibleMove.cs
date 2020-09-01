/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/05/09
  Contents    逆操作もできる指し手を表すインタフェース
*/
using System.Collections;
using System.Collections.Generic;

namespace Move
{
    public interface IReversibleMove : IForwardMove
    {
        //駒を動かす場合のみ使う
        //動かした駒の動かす前の向きを取得
        int GetMoveFromForwardFaceId();

        //以下２つは駒を動かして相手の駒を取る場合のみ使う
        //取った駒の種類を取得
        PieceKind GetCapturedPieceKind();
        //取った駒の向き（正面FaceId）を取得
        int GetCapturedPieceForwardFaceId();

        //駒を取ったかどうか、駒を取っていたらtrue
        bool IsCaptured();
    }
}