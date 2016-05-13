using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace UI {

    public abstract class ModelBase {

        public abstract void Init();
        protected abstract void OnReconnected();

    }

    public class ModelManager : Singleton<ModelManager> {

        List<ModelBase> modelSet = null;

        public void Init() {
            modelSet = new List<ModelBase>();

        }

        public T GetModel<T>() where T : ModelBase {
            T model = null;

            for (int i = 0; i < modelSet.Count; i++) {
                if (modelSet[i] is T) {
                    model = modelSet[i] as T;
                }
            }

            return model;
        }

    }


}

