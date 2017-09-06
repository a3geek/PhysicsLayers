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
        [SerializeField]
        private bool autoRespawn = true;
        [SerializeField, Range(0f, 1f)]
        private float respawnRate = 0.05f;
        
        private bool spawned = false;
        private Dictionary<int, List<AbstractCollisionCallbacks>> collisions = new Dictionary<int, List<AbstractCollisionCallbacks>>();


        void Awake()
        {
            StartCoroutine(this.Spawn());
        }

        void Update()
        {
            if(this.spawned == false)
            {
                return;
            }
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                this.collisions[LayersManager.UnityLayerCount]
                    .ForEach(coll => coll.gameObject.SetActive(!coll.gameObject.activeSelf));
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                var colls = this.collisions[LayersManager.UnityLayerCount];
                
                for(var i = 0; i < colls.Count; i++)
                {
                    colls[i].Layer.ChangeLayer(LayersManager.UnityLayerCount + 1);
                    colls[i].UpdateIgnoreLayers();
                }
            }

            if(this.autoRespawn == false || Random.value > this.respawnRate)
            {
                return;
            }

            var keys = this.collisions.Keys.ToList();
            var layerID = keys[Random.Range(0, keys.Count)];

            if(Random.value <= 0.5f)
            {
                Debug.Log("Destroy");
                var colls = this.collisions[layerID];
                var coll = colls[Random.Range(0, colls.Count)];

                this.collisions[layerID].Remove(coll);
                Destroy(coll.gameObject);
            }
            else
            {
                Debug.Log("Create");
                var info = this.fieldInfo.Infos.FirstOrDefault(e => e.Prefab.LayerID == layerID);
                this.Create(info.Prefab, info.Parent, info.Color);
            }
        }
        
        private IEnumerator Spawn()
        {
            for(var i = 0; i < this.fieldInfo.Infos.Count; i++)
            {
                var info = this.fieldInfo.Infos[i];

                var parent = new GameObject(info.Prefab.name + "s");
                parent.transform.parent = transform;
                info.Parent = parent.transform;

                this.collisions.Add(info.Prefab.LayerID, new List<AbstractCollisionCallbacks>());

                for(var j = 0; j < info.Count; j++)
                {
                    this.Create(info.Prefab, parent.transform, info.Color);
                    yield return new WaitForSeconds(0f);
                }
            }

            this.spawned = true;
        }

        private void Create(AbstractCollisionCallbacks prefab, Transform parent, Color color)
        {
            var obj = Instantiate(prefab);
            obj.transform.parent = parent;

            obj.GetComponent<Renderer>().material.SetColor("_Color", color);
            this.collisions[obj.LayerID].Add(obj);
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

                [HideInInspector]
                public Transform Parent = null;
            }
            
            public List<Info> Infos = new List<Info>();
        }
    }
}
