/*
  Author      森友雅
  LastUpdate  2020/04/06
  Since       2020/03/14
  Contents    N InputIDの抽象クラス
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputID
{
    public abstract class InputEvent 
    {
        public abstract float GetTouchDeltaTime();

    }
}
