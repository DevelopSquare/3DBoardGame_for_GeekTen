/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/03/26
  Contents    ゲーム画面から結果画面へ渡す試合結果
              5/23　ドローをNoneに変更。
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Position;
using Move;

namespace GameManager
{
    public class ResultData : MonoBehaviour
    {
        public static PlayerKind winner = PlayerKind.None;
        public static WinType wintype = WinType.None;
        public static List<PositionData> positionRecord;
        public static List<IReversibleMove> moveRecord;

        void Awake()
        {
            positionRecord = new List<PositionData>();
            moveRecord = new List<IReversibleMove>();
        }
    }
}

