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


        void Awake()
        {
            StartCoroutine(this.Spawn());
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
