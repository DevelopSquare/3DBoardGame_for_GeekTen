/*
  Author      田中木介
  LastUpdate  2020/05/02
  Since       2020/05/02

  Author      森友雅
  LastUpdate  2020/08/28
  Since       2020/07/17

  Contents    Menuの管理
*/

using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MiniMap;
using InputID;

namespace Menu
{

    public class MenuManager : MonoBehaviour
    {
        //private GameObject messageObject;

        [SerializeField] private TitleButton title_button;

        private Canvas canvas;
        public GameObject menuBar;
        private ChoiceMessage mQuite;
        private TextMessage mPause;

        private int BaseLayer = 0;
        private int highLayer = 100;

        private MiniMapCameraCtrl camera;
        private InputEventFactory input;

        private GameObject Barrier;

        // Start is called before the first frame update
        void Awake()
        {
            canvas = transform.parent.gameObject.GetComponent<Canvas>();
            camera = GameObject.Find("Main Camera").GetComponent<MiniMapCameraCtrl>();

            input = GameObject.Find("InputManager").GetComponent<InputEventFactory>();

            Barrier = UnityEngine.Camera.main.transform.Find("Barrier").gameObject;

            //Debug.Log("Barrier  " + Barrier);
        }

        // Update is called once per frame
        void Update()
        {
            PauseCtrl();
            QuiteCtrl();
        }

        public void OnTitle()
        {
            title_button.OnDialog();
        }

        public void OnPause()
        {

            //mPause = message.CreateTextMessage().GetComponent<TextMessage>();

            // messageObject = mObj;
            //string text = "Pause";
            //mPause.SetText(text);

            TimeCtrl(0);
        }

        public void OpenMenu()
        {
            menuBar.SetActive(false);
            this.gameObject.SetActive(true);
            //canvas.sortingOrder = highLayer;
            if(Barrier!=null) Barrier.SetActive(true);
            input.rock = true;
        }

        public void CloseMenu()
        {
            menuBar.SetActive(true);
            //canvas.sortingOrder = BaseLayer;
            this.gameObject.SetActive(false);
            if (Barrier != null) Barrier.SetActive(false);
            input.rock = false;
        }

        private void QuiteCtrl()
        {
            if (mQuite == null) return;
            //Debug.Log("Clicked : "+ mQuite.GetClick());

            switch (mQuite.GetClick())
            {
                case ClickKind.yes :
                    Quit();
                    break;
                case ClickKind.no:
                    mQuite.Delete();
                    break;
                default:
                    break;
            }
            
        }

        private void PauseCtrl()
        {
            if (mPause == null) return;
            switch (mPause.GetClick())
            {
                case ClickKind.close :
                    TimeCtrl(1f);
                    mPause.Delete();
                    break;
                default:
                    break;
            }

            
        }

        private void TimeCtrl(float scale)
        {
            Time.timeScale = scale;
        }

        private void Quit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
            #endif
        }
    }
}


