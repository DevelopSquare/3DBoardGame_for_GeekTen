/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/05/09
  Contents    指し手を表すインタフェース
*/
using System.Collections;
using System.Collections.Generic;

namespace Move
{
    public interface IForwardMove
    {
        //駒を動かす場合、回転させる場合両方に使う
        //駒の動かす前のFaceIdを取得
        int GetMoveFromFaceId();

        //駒を動かす場合のみ使う
        //動かした駒の移動先のFaceIdを取得
        int GetMoveToFaceId();

        //駒を回転させる場合のみ使う
        //回転回数を取得（右回転1回：+1）
        int GetRotateDirection();

        //駒を動かすかどうか、動かすときtrue
        bool IsMove();
        //駒を回転させるかどうか、回転させるときtrue
        bool IsRotate();
    }
}