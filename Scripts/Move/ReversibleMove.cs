/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/05/09
  Contents    動きを表すクラス
              逆操作もできる
*/
using System.Collections;
using System.Collections.Generic;

namespace Move
{
    public class ReversibleMove : ReversibleMoveBase
    {
        /// <summary>駒を取らなかった動きとしてセットする</summary>
        /// <param name="fromFaceId">移動元のFaceId</param>
        /// <param name="fromForwardFaceId">移動する駒が移動前に向いていた向き（正面FaceId）</param>
        /// <param name="toFaceId">移動先のFaceId</param>
        public ReversibleMove(int fromFaceId, int fromForwardFaceId, int toFaceId)
        {
            this.fromFaceId = fromFaceId;
            this.fromForwardFaceId = fromForwardFaceId;
            this.toFaceId = toFaceId;
            this.rotateDirection = 0;
            this.isCaptured = false;
        }

        /// <summary>駒を取った動きとしてセットする</summary>
        /// <param name="fromFaceId">移動元のFaceId</param>
        /// <param name="fromForwardFaceId">移動する駒が移動前に向いていた向き（正面FaceId）</param>
        /// <param name="toFaceId">移動先のFaceId</param>
        /// <param name="capturedPieceKind">取った駒の種類</param>
        /// <param name="capturedPieceForwardFaceId">取った駒の向き（正面FaceId）</param>
        public ReversibleMove(int fromFaceId, int fromForwardFaceId, int toFaceId, PieceKind capturedPieceKind, int capturedPieceForwardFaceId)
        {
            this.fromFaceId = fromFaceId;
            this.fromForwardFaceId = fromForwardFaceId;
            this.toFaceId = toFaceId;
            this.rotateDirection = 0;
            this.capturedPieceKind = capturedPieceKind;
            this.capturedPieceForwardFaceId = capturedPieceForwardFaceId;
            this.isCaptured = true;
        }

        /*
        /// <summary>回転する動きとしてセットする</summary>
        /// <param name="faceId">回転する駒のFaceId</param>
        /// <param name="rotateDirection">回転回数（右回転1回：+1）</param>
        public new void InitRotation(int faceId, int rotateDirection)
        {
            base.InitRotation(faceId, rotateDirection);
            SetNotCaptured();
        }

        /// <summary>駒を取らなかった動きとしてセットする</summary>
        /// <param name="fromFaceId">移動元のFaceId</param>
        /// <param name="toFaceId">移動先のFaceId</param>
        public new void InitNotCapturedMove(int fromFaceId, int toFaceId)
        {
            base.InitNotCapturedMove(fromFaceId, toFaceId);
            SetNotCaptured();
        }

        /// <summary>駒を取った動きとしてセットする</summary>
        /// <param name="fromFaceId">移動元のFaceId</param>
        /// <param name="toFaceId">移動先のFaceId</param>
        /// <param name="capturedPieceKind">取った駒の種類</param>
        /// <param name="capturedPieceForwardFaceId">取った駒の向き（正面FaceId）</param>
        public void InitCapturedMove(int fromFaceId, int toFaceId, PieceKind capturedPieceKind, int capturedPieceForwardFaceId)
        {
            SetMoveFromFaceId(fromFaceId);
            SetMoveToFaceId(toFaceId);
            SetCapturedPieceKind(capturedPieceKind);
            SetCapturedPieceForwardFaceId(capturedPieceForwardFaceId);
        }
        */

    }
}
