using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class ColumnGroupSpawner : MonoBehaviour
    {
        public List<GameObject> Objs;

        private void Start()
        {
            for (int i = 0; i < 200; i++)
            {
                GameObject obj = Instantiate(Objs[UnityEngine.Random.Range(0, Objs.Count)], transform);
                obj.transform.localPosition = new Vector3((i + 1) * 4.5f, 0, 0f);
            }
        }
    }
}