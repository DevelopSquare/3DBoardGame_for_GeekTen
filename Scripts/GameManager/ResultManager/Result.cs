/*
  Author      小路重虎
  LastUpdate  2020/08/27(tomomasa タイトルに戻るときにsoundmanagerがDestroyされるようにした
  LastUpdate  2020/08/30(髙橋　勝ち負けによって結果表示の色が変わる)
  Since       2020/03/16
  Contents    結果画面
              Resultクラス
*/

/*
  Resultシーンが必須
  あらかじめResultDataクラスに値を設定しておく必要あり
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using GameManager;


namespace GameManager.ResultManager
{
    public class Result : StateBase, IResult
    {
        private PlayerKind winner;
        private WinType winType;
        private int score;
        private Text winnerText;
        private Text winTypeText;
        private Image resultPanel;

        void Start()
        {
            GameObject winnerTextObject = GameObject.Find("WinnerText");
            GameObject winTypeTextObject = GameObject.Find("WinTypeText");
            GameObject resultPanelObject = GameObject.Find("ResultPanel");
            winnerText = winnerTextObject.GetComponent<Text>();
            winTypeText = winTypeTextObject.GetComponent<Text>();
            resultPanel = resultPanelObject.GetComponent<Image>();


            SetResult();

            DisplayResult();
        }

        void Update()
        {
            /* テスト用
            if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad0))
            {
                SetResult(0);
                DisplayResult();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Keypad1))
            {
                SetResult(1);
                DisplayResult();
            }
            //*/
        }

        //試合結果を設定するメソッド
        public void SetResult()
        {
            winner = ResultData.winner;
            winType = ResultData.wintype;
        }

        //ボタンを押すと呼ばれる　一局を通して見る、PostGameAnalysisシーンへ移動
        public void GoToAnalysis()
        {
            SceneManager.LoadScene("PostGameAnalysis");
        }

        //Restartボタンを押すと呼ばれる　タイトル画面から再スタート
        public void Restart()
        {
            Destroy(GameObject.Find("SoundManager"));
            SceneManager.LoadScene("Title");
        }

        //Quitボタンを押すと呼ばれる　アプリ終了
        public void Quit()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//テスト時
    #else
            Application.Quit();//実行時
    #endif
        }

        //ボタンを押すと呼ばれる　Twitterに共有する 5/28の議事録より
        public void ShareTwitter()
        {
            //urlの作成
            string text = "";
            switch (winner)
            {
                case PlayerKind.HumanPlayer:
                    text = UnityWebRequest.EscapeURL("3DBoardGameでコンピュータに勝利！");
                    break;
                case PlayerKind.CP:
                    text = UnityWebRequest.EscapeURL("3DBoardGameでコンピュータに敗北！");
                    break;
                case PlayerKind.Draw:
                    text = UnityWebRequest.EscapeURL("3DBoardGameでコンピュータと引き分け！");
                    break;
            }
            string tag = UnityWebRequest.EscapeURL("#3DBoardGame #DevelopSquare");

            string url = "https://twitter.com/intent/tweet?text=" + text +
                "https://twitter.com/developsquare/status/1264484900083646466/photo/1%0a%0a" + tag;
            Application.OpenURL(url);
        }

        //スコア計算(?)
        public void CalculateScore()
        {

        }

    //ここからprivateメソッド

        //結果を表示する
        private void DisplayResult()
        {


            string winnerString = "No Result";
            string winTypeString = "";
            Color stringColor = new Color(0.14f, 0.86f, 0.82f, 1.0f);
            Sprite panelSprite = Resources.Load<Sprite>("Message/GreenMsg");


            switch (winner)
            {
                case PlayerKind.Draw://引き分け
                    winnerString = "引き分け";
                    break;
                case PlayerKind.HumanPlayer://Human勝利
                    winnerString = "You win!";

                    switch (winType)
                    {
                        case WinType.GetKing:
                            winTypeString = "相手のキングを取った";
                            break;
                        case WinType.ReachPole:
                            winTypeString = "相手側の極に到達";
                            break;
                        case WinType.None:
                            break;
                    }
                    break;
                case PlayerKind.CP://CP勝利
                    winnerString = "You lose…";
                    stringColor = new Color(1.0f, 0.39f, 0.43f, 1.0f);
                    panelSprite = Resources.Load<Sprite>("Message/RedMsg");
                    switch (winType)
                    {
                        case WinType.GetKing:
                            winTypeString = "相手にキングを取られた";
                            break;
                        case WinType.ReachPole:
                            winTypeString = "相手があなた側の極に到達";
                            break;
                        case WinType.None:
                            break;
                    }
                    break;
            }

            /*
            switch (winner)
            {
                case PlayerKind.Draw://引き分け
                    winnerString = "引き分け";
                    break;
                case PlayerKind.HumanPlayer://Human勝利
                    winnerString = "You win!";
                    break;
                case PlayerKind.CP://CP勝利
                    winnerString = "You lose…";
                    break;
            }

            switch (winType)
            {
                case WinType.GetKing:
                    winTypeString = "相手のキングを取った";
                    break;
                case WinType.ReachPole:
                    winTypeString = "相手側の極に到達";
                    break;
                case WinType.None:
                    break;
            }
            */

            winnerText.text = winnerString;
            winTypeText.text = winTypeString;
            winnerText.color = stringColor;
            winTypeText.color = stringColor;
            resultPanel.sprite = panelSprite;
        }
    }
}
