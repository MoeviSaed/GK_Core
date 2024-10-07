using System;
using MVVM.Core;

namespace MVVM.Core
{
    public abstract class MVVMViewModel<T> where T : MVVMModel
    {
        protected MVVMViewModel()
        {
            Model = CreateModel();
        }
        protected readonly T Model;

        protected abstract T CreateModel();

        public Y Data<Y>() where Y : Data => Model.Data<Y>(); // Get value from model

        public void AddListener<Y>(Action listener) where Y : Data
        {
            Model.Data<Y>().AddListener(listener); // Add listener to model by value type.
        }

        public void RemoveListener<Y>(Action listener) where Y : Data
        {
            Model.Data<Y>().RemoveListener(listener); // Remove listener from model by value type.
        }
    }
    
}