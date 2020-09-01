/*
  Author      田中木介
  LastUpdate  2020/04/12
  Since       2020/04/09
  Contents    Piecesの拡張メソッド
  　　　　　　using Effect でPieces.Functionで使える
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piece;
using Sound;

namespace Effect
{
    static class EffectManager 
    {

        public enum Animation
        {
            BeatedPiece=1,
            MoveBegin=2,
            MoveFinish=3,
            DestroyPiece=4,
            CreatePiece=5,
            Idle=0
        }

        public static bool IsAnimate(this Pieces pieces)
        {
            Animator anim = pieces.gameObject.GetComponent<Animator>();

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("CreatePiece") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("BeatedPiece") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("MoveBegin") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("MoveFinish") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("DestroyPiece") )
            {
                return true;
            }

            return false;
        }




        public static void CreateEffect(this Pieces piece)
        {
            
            //Debug.Log("Created!!");

            Animator anim= piece.gameObject.GetComponent<Animator>();

            anim.SetInteger("Animation", (int)Animation.CreatePiece);


        }

        public static void DestroyEffect(this Pieces piece)
        {
            //Debug.Log("Destroyed!!");

            Animator anim = piece.gameObject.GetComponent<Animator>();

            anim.SetInteger("Animation", (int)Animation.DestroyPiece);

        }

        public static void MoveBeginEffect(this Pieces piece)
        {
            //Debug.Log("MoveBegin!!");

            Animator anim = piece.gameObject.GetComponent<Animator>();

            anim.SetInteger("Animation", (int)Animation.MoveBegin);

        }

        public static void MoveFinishEffect(this Pieces piece)
        {
            //Debug.Log("MoveFinish!!");

            Animator anim = piece.gameObject.GetComponent<Animator>();

            anim.SetInteger("Animation", (int)Animation.MoveFinish);

        }

        public static void BeatedEffect(this Pieces piece)
        {
           // Debug.Log("Beated!!");

            Animator anim = piece.gameObject.GetComponent<Animator>();

            anim.SetInteger("Animation", (int)Animation.BeatedPiece);

        }

        public static void NotCreateEffect(this Pieces piece)
        {
            //Debug.Log("Beated!!");

            Animator anim = piece.gameObject.GetComponent<Animator>();

            anim.SetBool("isIdle",true);
        }

        
    }


}

