/*
  Author      小路重虎
  LastUpdate  2020/05/09
  Since       2020/05/09
  Contents    対戦後の検討
              PostGameAnalysisシーン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager.PostGameAnalysis
{
    public interface IAnalysis
    {
        void PlaceFirstPosition();
        void PlacePreviousPosition();
        void PlaceNextPosition();
        void PlaceLastPosition();
        void GoToResult();
    }
}
