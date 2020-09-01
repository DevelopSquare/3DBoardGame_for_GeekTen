/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/06/13
  Contents    指し手を表す抽象クラス
*/
using System.Collections;
using System.Collections.Generic;

namespace Move
{
    public abstract class ForwardMoveBase : IForwardMove
    {
        protected int fromFaceId;
        protected int toFaceId;
        protected int rotateDirection = 0;//0なら駒を動かす指し手、0でなければ駒を回転させる指し手

        //駒の動かす前のFaceIdを取得
        public int GetMoveFromFaceId()
        {
            return fromFaceId;
        }

        //動かした駒の移動先のFaceIdを取得
        public int GetMoveToFaceId()
        {
            return toFaceId;
        }

        //回転回数を取得（右回転1回：+1）
        public int GetRotateDirection()
        {
            return rotateDirection;
        }

        //駒を動かすかどうか、動かすときtrue
        public bool IsMove()
        {
            return (rotateDirection == 0);
        }

        //駒を回転させるかどうか、回転させるときtrue
        public bool IsRotate()
        {
            return !IsMove();
        }
    }
}
