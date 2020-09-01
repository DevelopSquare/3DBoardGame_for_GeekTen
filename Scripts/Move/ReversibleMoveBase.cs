/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/06/13
  Contents    逆操作もできる指し手の抽象クラス
*/
using System.Collections;
using System.Collections.Generic;

namespace Move
{
    public abstract class ReversibleMoveBase : ForwardMoveBase, IReversibleMove
    {
        protected int fromForwardFaceId;
        protected PieceKind capturedPieceKind;
        protected int capturedPieceForwardFaceId;
        protected bool isCaptured = false;

        //動かした駒の動かす前の向きを取得
        public int GetMoveFromForwardFaceId()
        {
            return fromForwardFaceId;
        }

        //取った駒の種類を取得
        public PieceKind GetCapturedPieceKind()
        {
            return capturedPieceKind;
        }

        //取った駒の向き（正面FaceId）を取得
        public int GetCapturedPieceForwardFaceId()
        {
            return capturedPieceForwardFaceId;
        }

        //駒を取ったかどうか、駒を取っていたらtrue
        public bool IsCaptured()
        {
            return isCaptured;
        }

    }
}
