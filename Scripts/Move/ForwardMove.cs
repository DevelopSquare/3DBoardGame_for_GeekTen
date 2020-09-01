/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/05/09
  Contents    動きを表すクラス
              逆操作はできない
*/
using System.Collections;
using System.Collections.Generic;

namespace Move
{
    public class ForwardMove : ForwardMoveBase
    {
        /// <summary></summary>
        /// <param name="fromFaceId">移動元のFaceId</param>
        /// <param name="toFaceId">移動先のFaceId</param>
        public ForwardMove(int fromFaceId, int toFaceId)
        {
            this.fromFaceId = fromFaceId;
            this.toFaceId = toFaceId;
            this.rotateDirection = 0;
        }
        /*
        /// <summary>回転する動きとしてセットする</summary>
        /// <param name="faceId">回転する駒のFaceId</param>
        /// <param name="rotateDirection">回転回数（右回転1回：+1）</param>
        public void InitRotation(int faceId, int rotateDirection)
        {
            SetMoveFromFaceId(faceId);
            SetRotateDirection(rotateDirection);
        }

        /// <summary>
        ///駒を取らなかった動きとしてセットする
        /// </summary>
        /// <param name="fromFaceId"></param>
        /// <param name="toFaceId"></param>
        public void InitNotCapturedMove(int fromFaceId, int toFaceId)
        {
            SetMoveFromFaceId(fromFaceId);
            SetMoveToFaceId(toFaceId);
        }
        */
    }
}
