/*
  Author      藤澤典隆
  LastUpdate  2020/04/2
  Since       2020/03/11
  Contents    PlayGameでのターンを切り替える。4/1時点でクラス図と一部齟齬あり。
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager.PlayGameManager
{

    public class TurnManager : MonoBehaviour
    {
        
        int crrTurn=1;
        public bool IsPlayerTurn()
        {
            if (crrTurn % 2 == 1) return true;
            else return false;
        }
        public int GetTurn()
        {
            return crrTurn;
        }
        public void ChangeTuen()
        {
            crrTurn += 1;
        }
    }

}
