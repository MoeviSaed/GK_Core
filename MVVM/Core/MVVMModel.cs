using System;
using System.Collections.Generic;
using MVVM.Core;

namespace MVVM.Core
{
    public abstract class MVVMModel
    {
        protected  Dictionary<Type, Data> DataMap;

        public MVVMModel()
        {
            DataMap = new Dictionary<Type, Data>();
            InitializeDatas();
        }

        protected abstract void InitializeDatas();

        public T Data<T>() where T : Data => DataMap[typeof(T)] as T;
    }

    // public class TestModel : MVVMModel
    // {
    //     protected override void InitializeDatas()
    //     {
    //         DataMap.Add(typeof(ParameterCustom), new ParameterCustom());
    //     }
    //
    //     public class ParameterCustom : DataDefaultValueMod<ParameterCustom>
    //     {
    //         public override float DefaultValue => 0; // Link to Data
    //         public ParameterCustom() { }
    //     }
    // }

}