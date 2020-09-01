/*
  Author      小路重虎
  LastUpdate  2020/05/09
  Since       2020/03/16
  Contents    結果画面
              Resultクラスのインターフェース
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager.ResultManager
{
    public interface IResult
    {
        void SetResult();
        void GoToAnalysis();
        void Restart();
        void Quit();
        void CalculateScore();
    }
}
