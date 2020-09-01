/*
  Author      小路重虎
  LastUpdate  2020/07/04
  Since       2020/05/09
  Contents    対戦後の検討
              PostGameAnalysisシーン
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Position;
using Move;

namespace GameManager.PostGameAnalysis
{
    public class Analysis : StateBase, IAnalysis
    {
        private PositionObjects position;
        private Text MoveNumTextComponent;

        private int crrmove = 0;
        private int maxmove;
        private List<IReversibleMove> moveRecord;
        private List<PositionData> positionRecord;

        void Start()
        {
            position = GameObject.Find("PositionObjects").GetComponent<PositionObjects>();
            MoveNumTextComponent = GameObject.Find("MoveNum").GetComponent<Text>();
            SetRecord();
        }

        void Update()
        {

        }

        //"<<"を押す
        public void PlaceFirstPosition()
        {
            PlacePosition(0);
        }

        //"<-"を押す
        public void PlacePreviousPosition()
        {
            PlacePosition(crrmove - 1);
        }

        //"->"を押す
        public void PlaceNextPosition()
        {
            PlacePosition(crrmove + 1);
        }

        //">>"を押す
        public void PlaceLastPosition()
        {
            PlacePosition(maxmove);
        }

        //"Back"を押す
        public void GoToResult()
        {
            SceneManager.LoadScene("Result");
        }



    //ここからprivate

        //棋譜データ読み込み　現在テスト用にpublic化
        public void SetRecord()
        {
            moveRecord = ResultData.moveRecord;
            positionRecord = ResultData.positionRecord;
            crrmove = -2;
            maxmove = positionRecord.Count - 1;
            PlacePosition(0);
        }

        //手数を変更する
        private void PlacePosition(int n)
        {
            if (0 <= n && n <= maxmove)
            {
                /*Moveネームスペース変更のため一時的にコメントアウト
                if (n == crrmove + 1)//一手だけ進めるパターン
                {
                    position.Move(moveRecord[crrmove]);
                }
                
                else if (n == crrmove - 1)//一手だけ戻すパターン
                {
                    position.MoveBack(moveRecord[crrmove - 1]);
                }
                else*/
                if (crrmove != n)
                {
                    position.Load(positionRecord[n]);
                }
                crrmove = n;
                ChangeMoveNumText();
            }
        }

        //現在の手数を表示する
        private void ChangeMoveNumText()
        {
            MoveNumTextComponent.text = crrmove.ToString();
        }
    }

}
