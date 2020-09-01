/*
  Author      小路重虎
  LastUpdate  2020/03/23
  Since       2020/03/16
  Contents    各画面のベースになるStateBase抽象クラス
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputID;
using Sound;

namespace GameManager
{
    public abstract class StateBase : MonoBehaviour
    {
        protected InputManager input;
        protected SoundManager sound;
    }
}

