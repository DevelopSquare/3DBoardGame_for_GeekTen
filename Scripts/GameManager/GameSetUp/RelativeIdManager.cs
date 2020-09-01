/*
  Author      �X�F��
  LastUpdate  2020/05/31
  Since       2020/03/30
  Contents    N GameObject
              ��J�[�h�̂�ID�𑊑�ID�ɕϊ�����ւ���X�N���v�g
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager.GameSetUp
{
    public class RelativeIdManager : MonoBehaviour
    {
        //��̃I�u�W�F�N�g�擾
        public GameObject Pawn;
        public GameObject Queen;
        public GameObject King;

        /// <summary>
        /// ��̃J�[�h���擾���Ċ����ID�ŕԂ�
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        /// 
        public int ChangeToRelativeId(GameObject objectId)
        {            
            if (objectId == Pawn)
            {
                return -2;
            }
            if (objectId == Queen)
            {
                return -5;
            }
            if (objectId == King)
            {
                return -10;
            }

            return -1;
        }


    }
}