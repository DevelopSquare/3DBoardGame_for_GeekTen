/*
  Author      田中木介
  LastUpdate  2020/08/31(髙橋 ホログラム駒を可視化する関数を追加)
  Since       2020/04/10
  Contents    アニメーションの制御
              アニメーションをしたい駒にアタッチする            
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piece;
using GameManager;
using Sound;

namespace Effect
{
    public class AnimationCtrl : MonoBehaviour
    {
        //private EffectManager.Animation animation;

        private Animator anim;
        private Pieces pieces;
        private SoundManager sound;

        private GameObject EffectMoveObj;
        private GameObject EffectCreateObj;
        private GameObject EffectDestroyObj;
        private GameObject EffectBeatedObj;

        // Start is called before the first frame update
        void Awake()
        {
            anim = gameObject.GetComponent<Animator>();
            pieces = gameObject.GetComponent<Pieces>();

            EffectMoveObj = transform.Find("EffectMove").gameObject;
            EffectCreateObj = transform.Find("EffectCreate").gameObject;
            EffectDestroyObj = transform.Find("EffectDestroy").gameObject;
            EffectBeatedObj = transform.Find("EffectBeated").gameObject;

            var soundObj = GameObject.Find("SoundManager");
            sound = soundObj.GetComponent<SoundManager>();

        }

        

        public void TransitionIdle()
        {
            //Debug.Log("Idle!!");
            anim.SetInteger("Animation", (int)EffectManager.Animation.Idle);
        }

        public void PlayMove()
        {
            Debug.Log("Play Move");
            EffectMoveObj.SetActive(true);
        }

        public void PlayCreate()
        {
            EffectCreateObj.SetActive(true);
        }

        public void PlayDestroy()
        {
            EffectDestroyObj.SetActive(true);
        }

        public void PlayBeated()
        {
            EffectBeatedObj.SetActive(true);
        }



        public void StopMove()
        {
            Debug.Log("Stop Move");
            EffectMoveObj.SetActive(false);
        }

        public void StopCreate()
        {
            EffectCreateObj.SetActive(false);
        }

        public void StopDestroy()
        {
            EffectDestroyObj.SetActive(false);

            //DeletePiece();
        }

        public void StopBeated()
        {
            EffectBeatedObj.SetActive(false);

            //DeletePiece();
        }


        public void SEBeated()
        {
            SE("EffectBeated");
        }

        public void SECreate()
        {
            SE("EffectCreate");
        }

        public void SEMove()
        {
            SE("EffectMove");
        }

        public void SEDestroy()
        {
            SE("EffectDestroy");
        }

        private void DeletePiece()
        {
            if (pieces == null) return;
            //if (ManagerStore.cp.HasPiece(pieces.GetPieceId()))
            //{
            //    ManagerStore.cp.DeleteMyPieces(pieces.GetPieceId());
            //}else if (ManagerStore.humanPlayer.HasPiece(pieces.GetPieceId()))
            //{
            //    ManagerStore.humanPlayer.DeleteMyPieces(pieces.GetPieceId());

            //}
        }

        //ホログラム駒を可視化
        private void ActivePiece()
        {
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        private  void SE(string name)
        {
            
            sound.PlaySE(name);
        }
    }

}


