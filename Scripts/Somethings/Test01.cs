using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Test01 : MonoBehaviour
    {
        void Update()
        {
            Debug.Log(gameObject.GetInstanceID());
        }
    }

}
