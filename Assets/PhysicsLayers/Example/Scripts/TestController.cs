using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = UnityEngine.Random;

namespace a3geek.PhysicsLayers.Examples
{
    using Abstracts;
    using Layers.Abstracts;

    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    public class TestController : MonoBehaviour
    {
        [SerializeField]
        private FieldInfo fieldInfo = new FieldInfo();

        private List<AbstractCollisionCallbacks> collisions = new List<AbstractCollisionCallbacks>();


        void Awake()
        {
            StartCoroutine(this.Spawn());
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                this.collisions
                    .FindAll(coll => coll.LayerID == LayersManager.UnityLayerCount)
                    .ForEach(coll => coll.gameObject.SetActive(!coll.gameObject.activeSelf));
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                var colls = this.collisions
                    .FindAll(coll => coll.LayerID == LayersManager.UnityLayerCount).ToArray();

                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                //実行時間を知りたい処理.
                for(var i = 0; i < colls.Length; i++)
                {
                    colls[i].Layer.ChangeLayer(33);
                }

                // list → 333ms
                // Array -> 23ms

                // 100で333ms → 3.33ms / 1
                // 100で25ms → 0.25ms / 1
                
                sw.Stop();
                Debug.Log("経過時間:" + sw.ElapsedMilliseconds + "ms");

                for(var i = 0; i < colls.Length; i++)
                {
                    colls[i].UpdateIgnoreLayers();
                }
            }
        }

        private IEnumerator Spawn()
        {
            for(var i = 0; i < this.fieldInfo.Infos.Count; i++)
            {
                var info = this.fieldInfo.Infos[i];

                var parent = new GameObject(info.Prefab.name + "s");
                parent.transform.parent = transform;

                for(var j = 0; j < info.Count; j++)
                {
                    var obj = Instantiate(info.Prefab);
                    obj.name = i + ":" + obj.name;
                    obj.transform.parent = parent.transform;
                    
                    obj.GetComponent<Renderer>().material.SetColor("_Color", info.Color);
                    this.collisions.Add(obj);

                    yield return new WaitForSeconds(0f);
                }
            }
        }
        
        [Serializable]
        private class FieldInfo
        {
            [Serializable]
            public class Info
            {
                public AbstractCollisionCallbacks Prefab = null;
                public int Count = 1;
                public Color Color = Color.white;
            }
            
            public List<Info> Infos = new List<Info>();
        }
    }
}
