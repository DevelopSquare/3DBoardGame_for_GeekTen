/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/06/13
  Contents    回転を表すクラス
              逆操作もできる
*/
using System.Collections;
using System.Collections.Generic;

namespace Move
{
    public class Rotation : ReversibleMoveBase
    {
        /// <summary>回転する指し手としてセットする</summary>
        /// <param name="faceId">回転する駒のFaceId</param>
        /// <param name="rotateDirection">回転回数（右回転1回：+1）</param>
        public Rotation(int faceId, int rotateDirection)
        {
            this.fromFaceId = faceId;
            this.rotateDirection = rotateDirection;
        }
    }
}
