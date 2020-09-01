/*
  Author      田中木介
  LastUpdate  2020/04/17
  Since       2020/03/11
  Contents    面のごとの情報格納と操作
              操作したい面にアッタッチする
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Field
{
    public class SurfaceInfo : MonoBehaviour
    {
        [SerializeField] private float rayLength = 10;

        [SerializeField] private int faceId = 0;

        [SerializeField] private int sideNum = 0;

        [SerializeField] private float bias = 1f;

        private GameObject Arrow ;

        private Vector3 normalVec = default;
        [SerializeField] private List<int> adgacentFaces = new List<int>();

        Color[] color = { Color.red, Color.blue, Color.green, Color.black, Color.cyan,
                        Color.black, Color.gray, Color.magenta, Color.white, Color.yellow, };
    
      // MeshFilter meshFilter= GetComponent<MeshFilter>();

     /*  Define property of  surface infomation */
    
     //FaceId : Read Only
            public int FaceId
        {
            get{
                return faceId;
            }

            set
            {
                faceId = value;
            }
        }
     //Number of sides :  Read Only 
        public int SideNum
        {
            get
            {
                return sideNum;
            }

     
        }
     //Position of surface : Read Only
        public Vector3 FacePosition
        {
            get
            {
                return this.transform.position;
            }
        }

     //Position of surface : Read Only
        public Vector3 Normal
        {
            get
            {
                return this.normalVec;
            }
        }




        public List<int> GetFaces(Vector3 pieceDirection)
        {
            /*
             TODO:回りの面情報を取得
             Argument:駒の向き
             Return:回りのマスのId
             */

            //角度と方向ベクトル

            //Debug.DrawRay(transform.position, pieceDirection * rayLength, Color.black, 10);

            var neaberFaces = new List<int>();

            var meshNormals=GetNormal();

            var basicDirection = GetBasicDirection();//meshNormals[1] - meshNormals[1];

            var faceDirection = basicDirection;//Vector3.ProjectOnPlane(transform.TransformDirection(transform.forward), normalVec);

           
            //var from2Angle = Vector3.SignedAngle(pieceDirection, transform.TransformDirection(transform.forward), meshNormals);// transform.TransformDirection(transform.up));
            var from2Angle = Vector3.SignedAngle(faceDirection, pieceDirection, meshNormals);// transform.TransformDirection(transform.up));

           

                if (from2Angle<0)
                
                {
                    from2Angle = 360f+from2Angle;
                }
            //Debug.Log("Angle : "+from2Angle+" faceid : "+faceId);
            //Debug.Log("MyIndex : "+ (int)(from2Angle / (360 / sideNum)) + "FaceId  : " +faceId + "Angle : "+from2Angle );
            var directionIndex = ComplementIndex(Mathf.Abs((int)(from2Angle/(360 / sideNum))));
            //Debug.Log("Index  : "+String.Join(",",directionIndex));


            foreach(int i in directionIndex)
            {
                neaberFaces.Add(adgacentFaces[i]);
            }

            
            return neaberFaces;
        }

        

        public void ActivateArrow(bool isActivate) {
            /*
             TODO:矢印を有効化
             Argument:True　or False
             Return:none
             */
            //Arrow.GetComponent<Renderer>().enabled = isActivate ;
            Arrow.SetActive(isActivate);
            }

        public void SetChild(GameObject obj)
        {
            /*
             TODO:GameObjectを面の子要素にする
             Argument:GameObject
             Return:none
             */
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
        }




        /*Don't Touch The Code Below !! */

        void Start()
        {
            /*Inisialization*/
            Arrow = this.transform.Find("Arrow").gameObject;

            initAdjacentFaces();

            //ActivateArrow(true);
        }

        private void initAdjacentFaces()
        {
            /*
             TODO:回りの面情報を初期化
             Argument:None
             Return:回りのマスのId
             */

            //角度と方向ベクトル

            //Debug.DrawRay(transform.position, pieceDirection * rayLength, Color.black, 10);

            var neaberFaces = new List<int>();

            var meshNormals = GetNormal();
            normalVec = meshNormals;

            var basicDirection = GetBasicDirection();//meshNormals[1] - meshNormals[1];


            //var from2Angle = Vector3.SignedAngle(pieceDirection, transform.TransformDirection(transform.forward), meshNormals);// transform.TransformDirection(transform.up));
            //var from2Angle = Vector3.SignedAngle(transform.TransformDirection(transform.forward), pieceDirection, meshNormals);// transform.TransformDirection(transform.up));

            //if (pieceDirection.x * pieceDirection.y < 0)
            //{
            //    //Debug.Log("Inversed");
            //    //from2Angle = Vector3.SignedAngle(transform.TransformDirection(transform.forward), pieceDirection, meshNormals);// transform.TransformDirection(transform.up));
            //}

            //if (from2Angle < 0)

            //{
            //    from2Angle = Mathf.Abs(from2Angle) + 180;
            //}
            var directionIndex = ComplementIndex(0);//ComplementIndex(Mathf.Abs((int)(from2Angle / (360 / sideNum))));
            //Debug.Log("Index  : "+String.Join(",",directionIndex));

            Physics.queriesHitBackfaces = true;

            RaycastHit hit;


            for (int i = 0; i < sideNum; i++)
            {
                var rayDirection = transform.rotation * basicDirection; //transform.TransformDirection(transform.forward);
                Quaternion axisAngle1 = Quaternion.AngleAxis((directionIndex[i] * 360 / sideNum) + 180 * (sideNum - 2) / (sideNum * 8), meshNormals);
                Quaternion axisAngle2 = Quaternion.AngleAxis((directionIndex[i] * 360 / sideNum) + 180 * (sideNum - 2) / (sideNum * 4), meshNormals);

                var rayDirection1 = axisAngle1 * rayDirection;
                var rayDirection2 = axisAngle2 * rayDirection;


                Ray ray = new Ray(transform.position - meshNormals * bias, rayDirection1);
                Ray ray2 = new Ray(transform.position - meshNormals * bias, rayDirection2);

                if (Physics.Raycast(ray, out hit, 10.0f))
                {
                    var faceId = hit.collider.gameObject.GetComponent<SurfaceInfo>().FaceId;
                    if (neaberFaces.IndexOf(faceId) < 0)
                    {
                        neaberFaces.Add(faceId);
                    }
                    //Debug.Log(hit.collider.gameObject.name);

                }
                else if (Physics.Raycast(ray2, out hit, 10.0f))
                {
                    var faceId = hit.collider.gameObject.GetComponent<SurfaceInfo>().FaceId;
                    if (neaberFaces.IndexOf(faceId) < 0)
                    {
                        neaberFaces.Add(faceId);
                    }
                }

                //Debug.DrawRay(ray.origin, ray.direction * rayLength, color[i], 1);



            }

            adgacentFaces = neaberFaces;

            
        }
        private List<int> ComplementIndex(int startIndex)
            {

            //Debug.Log("     Start Index : "+startIndex);
            List<int> l= new List<int>();
                for (int i = 0; i < sideNum; i++)
                {
                    if (startIndex + i < sideNum)
                    {
                       l.Add(startIndex+i);
                    }
                    else
                    {
                        l.Add(Mathf.Abs(sideNum-startIndex-i));
                    }
                
                }

                return l;
            }

       

        private Vector3 GetNormal()
        {
            var meshFilt = GetComponent<MeshFilter>();
            if (meshFilt == null) {
                Debug.Log("Couldn't get surface mesh id = "+faceId);
                 }

            var mesh = meshFilt.mesh;

            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;

            Vector3 pos = vertices[1];
            pos.x *= transform.localScale.x;
            pos.y *= transform.localScale.y;
            pos.z *= transform.localScale.z;
            pos += transform.position;


            //Debug.DrawLine
            //(
            //   transform.position,
            //   pos + normals[1] * 10*-1, Color.white);

            return normals[1];

        }

        private Vector3 GetBasicDirection()
        {
            var meshFilt = GetComponent<MeshFilter>();
            if (meshFilt == null)
            {
                Debug.Log("Couldn't get surface mesh id = " + faceId);
            }

            var mesh = meshFilt.mesh;

            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;

            var poses=new List<Vector3>();

            for (var i = 0; i < normals.Length; i++)
            {
                Vector3 pos = vertices[i];
                pos.x *= transform.localScale.x;
                pos.y *= transform.localScale.y;
                pos.z *= transform.localScale.z;
                pos += transform.position;

                poses.Add(pos);
            }

            
            return poses[0]-poses[1];
        }


        void Update()
        {
                //GetFaces(transform.position);

            }
    }



}




