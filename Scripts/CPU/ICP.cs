/*
  Author      森拓哉
  LastUpdate  2020/03/23
  Since       2020/03/23
  Contents CPUの動き用インターフェースの定義
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Field;
namespace CP
{
     interface ICP
    {
      //抽象メソッド
     　 void SetInfo( PlayerBase cp, FieldManager field);
      //CPMoveクラスを返す
      CPMove BestMove();
    }
}
