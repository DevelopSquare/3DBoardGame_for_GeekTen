/*
  Author      藤澤典隆
  LastUpdate  2020/03/16
  Since       2020/03/10
  Contents    駒のprafabが持つScript。
  Init、Rotate、Move、testfuncが利用できる。
*/

using UnityEngine;
namespace Piece
{
    public class FieldPiece : Pieces
    {
        // public GameObject AttachedPieceObject;
        public void testfunc()
        {
            Debug.Log("アクセス成功！！");
            //Camera.PlayCamera PC = AttachedPieceObject.GetComponent<Camera.PlayCamera>();
            //PC.PlayTest();
            //MovePiece(new Vector3(0.5f, 0, 0));
            //RotatePiece(new Vector3(45f, 45f, 45f));
        }

    }
}
