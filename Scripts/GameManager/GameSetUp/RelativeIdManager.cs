/*
  Author      森友雅
  LastUpdate  2020/05/31
  Since       2020/03/30
  Contents    N GameObject
              駒カードののIDを相対IDに変換する関するスクリプト
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager.GameSetUp
{
    public class RelativeIdManager : MonoBehaviour
    {
        //駒のオブジェクト取得
        public GameObject Pawn;
        public GameObject Queen;
        public GameObject King;

        /// <summary>
        /// 駒のカードを取得して既定のIDで返す
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        /// 
        public int ChangeToRelativeId(GameObject objectId)
        {            
            if (objectId == Pawn)
            {
                return -2;
            }
            if (objectId == Queen)
            {
                return -5;
            }
            if (objectId == King)
            {
                return -10;
            }

            return -1;
        }


    }
}