using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVVM.Core
{
    #region Data

    //////////////// DATA /////////////////
    public abstract class Data
    {
        private event Action _onDataChanged;

        public Data() { }

        public void AddListener(Action listener) => _onDataChanged += listener;

        public void RemoveListener(Action listener) => _onDataChanged -= listener;
        protected virtual void SendMessage() => _onDataChanged?.Invoke();
    }

    #endregion

    #region Different Data

    //////////////// DIFFERENT VALUES /////////////////
    public abstract class DataDefaultValue<T, Y> : Data where T : Data
    {
        public abstract Y DefaultValue { get; }
        protected Y _value;

        private event Action<Y> _onDataChangedValue;

        protected DataDefaultValue()
        {
            _value = DefaultValue;
        }

        public virtual Y Value
        {
            get => _value;
            protected set
            {
                _value = value;
                SendMessage();
            }
        }

        public void AddListener(Action<Y> listener) => _onDataChangedValue += listener;
        public void RemoveListener(Action<Y> listener) => _onDataChangedValue -= listener;
        protected override void SendMessage()
        {
            _onDataChangedValue?.Invoke(Value);
            base.SendMessage();
        }
    }

    public abstract class DataSaveValue<T, Y> : Data where T : Data
    {
        protected abstract Y data { get; set; }
        protected Y _value;

        private event Action<Y> _onDataChangedValue;

        protected DataSaveValue()
        {
            _value = data;
        }

        public virtual Y Value
        {
            get => _value;
            protected set
            {
                data = value;
                _value = value;
                SendMessage();
            }
        }
   
        public void AddListener(Action<Y> listener) => _onDataChangedValue += listener;
        public void RemoveListener(Action<Y> listener) => _onDataChangedValue -= listener;
        protected override void SendMessage()
        {
            _onDataChangedValue?.Invoke(Value);
            base.SendMessage();
        }
    }

    public abstract class DataVariableValue<T, Y> : Data where T : Data
    {
        public Y DefaultValue { get; private set; }
        protected Y _value;

        private event Action<Y> _onDataChangedValue;

        protected DataVariableValue(Y defaultValue)
        {
            DefaultValue = defaultValue;
            _value = DefaultValue;
        }

        public virtual Y Value
        {
            get => _value;
            protected set
            {
                _value = value;
                SendMessage();
            }
        }

        public void AddListener(Action<Y> listener) => _onDataChangedValue += listener;
        public void RemoveListener(Action<Y> listener) => _onDataChangedValue -= listener;
        protected override void SendMessage()
        {
            _onDataChangedValue?.Invoke(Value);
            base.SendMessage();
        }
    }

    #endregion

    #region Default Data Types

    //////////////// DEFAULT DATA TYPES /////////////////

    public abstract class DataDefaultValueInt<T> : DataDefaultValue<T, int> where T : DataDefaultValue<T, int>
    {
        protected virtual int Min => 0;
        protected virtual int Max => Int32.MaxValue;

        public override int Value
        {
            get => _value;
            protected set
            {
                _value = Mathf.Clamp(value, Min, Max);
                SendMessage();
            }
        }
    }

    public abstract class DataDefaultValueFloat<T> : DataDefaultValue<T, float> where T : DataDefaultValue<T, float>
    {
        protected virtual float Min => 0f;
        protected virtual float Max => float.MaxValue;

        public override float Value
        {
            get => _value;
            protected set
            {
                _value = Mathf.Clamp(value, Min, Max);
                SendMessage();
            }
        }
    }

    public abstract class DataDefaultValueBool<T> : DataDefaultValue<T, bool> where T : DataDefaultValue<T, bool>
    {
        public override bool Value
        {
            get => _value;
            protected set
            {
                _value = value;
                SendMessage();
            }
        }

        public void SetValue(bool value) => Value = value;
    }

    public abstract class DataDefaultValueString<T> : DataDefaultValue<T, string> where T : DataDefaultValue<T, string>
    {
        public override string Value
        {
            get => _value;
            protected set
            {
                _value = value;
                SendMessage();
            }
        }

        public void SetValue(string value) => Value = value;
    }

    public abstract class DataDefaultValueMod<T> : DataDefaultValueFloat<T> where T : DataDefaultValueFloat<T>
    {
        protected Dictionary<object, float> Modificators;
        protected Dictionary<object, float> Percentages;

        protected virtual float MinPercentage => 0f;
        protected virtual float MaxPercentage => int.MaxValue;

        protected virtual float MinModificator => 0f;
        protected virtual float MaxModificator => int.MaxValue;

        protected DataDefaultValueMod()
        {
            Modificators = new Dictionary<object, float>();
            Percentages = new Dictionary<object, float>();
            Refresh();
        }

        public float BaseValue => base.Value;

        public override float Value
        {
            get => _completeValue;
            protected set
            {
                _value = value;
                Refresh();
                SendMessage();
            }
        }

        protected float _completeValue;

        protected void Refresh()
        {
            float value = BaseValue + CompleteModificator();
            value = Mathf.Clamp(value, MinModificator, MaxModificator);
            value *= (CompletePercentage() * 0.01f);
            _completeValue = Mathf.Clamp(value, Min, Max);
        }

        public bool AddModificator(object obj, float modificator, bool add = false)
        {
            if (Modificators.ContainsKey(obj))
            {
                Modificators[obj] = add ? Modificators[obj] + modificator : modificator;
                Refresh();
                SendMessage();
                return false;
            }
            else
            {
                Modificators.Add(obj, modificator);
                Refresh();
                SendMessage();
                return true;
            }
        }

        public bool RemoveModificator(object obj)
        {
            if (Modificators.ContainsKey(obj))
            {
                Modificators.Remove(obj);
                Refresh();
                SendMessage();
                return true;
            }

            return false;
        }

        public bool AddPercentageModificator(object obj, float percentage, bool add = false)
        {      
            if (Percentages.ContainsKey(obj))
            {
                Percentages[obj] = add ? Percentages[obj] + percentage : percentage;
                Refresh();
                SendMessage();
                return false;
            }
            else
            {
                Percentages.Add(obj, percentage);
                Refresh();
                SendMessage();
                return true;
            }
        }

        public bool RemovePercentageModificator(object obj)
        {
            if (Percentages.ContainsKey(obj))
            {
                Percentages.Remove(obj);
                Refresh();
                SendMessage();
                return true;
            }

            return false;
        }


        private float CompletePercentage()
        {
            float result = 100f;
            foreach (KeyValuePair<object, float> kvp in Percentages)
            {
                result += Percentages[kvp.Key];
            }

            return Mathf.Clamp(result, MinPercentage, MaxPercentage);
        }

        private float CompleteModificator()
        {
            float result = 0;
            foreach (KeyValuePair<object, float> kvp in Modificators)
            {
                result += Modificators[kvp.Key];
            }

            return result;
        }
    }

    #endregion

    #region Save Data Types

    //////////////// SAVE DATA TYPES /////////////////

    public abstract class DataSaveValueInt<T> : DataSaveValue<T, int> where T : DataSaveValue<T, int>
    {
        protected virtual int Min => 0;
        protected virtual int Max => Int32.MaxValue;

        public override int Value
        {
            get => _value;
            protected set
            {
                _value = Mathf.Clamp(value, Min, Max);
                data = _value;
                SendMessage();
            }
        }
    }

    public abstract class DataSaveValueFloat<T> : DataSaveValue<T, float> where T : DataSaveValue<T, float>
    {
        protected virtual float Min => 0f;
        protected virtual float Max => float.MaxValue;

        public override float Value
        {
            get => _value;
            protected set
            {
                _value = Mathf.Clamp(value, Min, Max);
                data = _value;
                SendMessage();
            }
        }
    }

    public abstract class DataSaveValueBool<T> : DataSaveValue<T, bool> where T : DataSaveValue<T, bool>
    {
        public override bool Value
        {
            get => _value;
            protected set
            {
                _value = value;
                data = _value;
                SendMessage();
            }
        }

        public void SetValue(bool value) => Value = value;
    }

    public abstract class DataSaveValueString<T> : DataSaveValue<T, string> where T : DataSaveValue<T, string>
    {
        public override string Value
        {
            get => _value;
            protected set
            {
                _value = value;
                data = _value;
                SendMessage();
            }
        }

        public void SetValue(string value) => Value = value;
    }

    public abstract class DataSaveValueMod<T> : DataSaveValueFloat<T> where T : DataSaveValueFloat<T>
    {
        protected Dictionary<object, float> Modificators;
        protected Dictionary<object, float> Percentages;

        protected virtual float MinPercentage => 0f;
        protected virtual float MaxPercentage => int.MaxValue;

        protected virtual float MinModificator => 0f;
        protected virtual float MaxModificator => int.MaxValue;


        protected DataSaveValueMod()
        {
            Modificators = new Dictionary<object, float>();
            Percentages = new Dictionary<object, float>();
            Refresh();
        }

        public float BaseValue => base.Value;

        public override float Value
        {
            get => _completeValue;
            protected set
            {
                _value = value;
                data = _value;
                Refresh();
                SendMessage();
            }
        }

        protected float _completeValue;

        private void Refresh()
        {
            float value = BaseValue + CompleteModificator();
            value = Mathf.Clamp(value, MinModificator, MaxModificator);
            value *= (CompletePercentage() * 0.01f);
            _completeValue = Mathf.Clamp(value, Min, Max);
        }

        public bool AddModificator(object obj, float modificator, bool add = false)
        {
            if (Modificators.ContainsKey(obj))
            {
                Modificators[obj] = add ? Modificators[obj] + modificator : modificator;
                Refresh();
                SendMessage();
                return false;
            }
            else
            {
                Modificators.Add(obj, modificator);
                Refresh();
                SendMessage();
                return true;
            }
        }

        public bool RemoveModificator(object obj)
        {
            if (Modificators.ContainsKey(obj))
            {
                Modificators.Remove(obj);
                Refresh();
                SendMessage();
                return true;
            }

            return false;
        }

        public bool AddPercentageModificator(object obj, float percentage, bool add = false)
        { 
            if (Percentages.ContainsKey(obj))
            {
                Percentages[obj] = add ? Percentages[obj] + percentage : percentage;
                Refresh();
                SendMessage();
                return false;
            }
            else
            {
                Percentages.Add(obj, percentage);
                Refresh();
                SendMessage();
                return true;
            }
        }

        public bool RemovePercentageModificator(object obj)
        {
            if (Percentages.ContainsKey(obj))
            {
                Percentages.Remove(obj);
                Refresh();
                SendMessage();
                return true;
            }

            return false;
        }


        private float CompletePercentage()
        {
            float result = 100f;
            foreach (KeyValuePair<object, float> kvp in Percentages)
            {
                result += Percentages[kvp.Key];
            }

            return Mathf.Clamp(result, MinPercentage, MaxPercentage);
        }

        private float CompleteModificator()
        {
            float result = 0;
            foreach (KeyValuePair<object, float> kvp in Modificators)
            {
                result += Modificators[kvp.Key];
            }

            return result;
        }
    }

    public abstract class DataSaveValueStringArray<T> : DataSaveValue<T, string[]> where T : DataSaveValue<T, string[]>
    {
        public override string[] Value
        {
            get => _value;
            protected set
            {
                _value = value;
                data = _value;
                SendMessage();
            }
        }

        public void SetValue(string[] value) => Value = value;
    }

    public abstract class DataSaveValueDictonary<T, TKey, TValue> : DataSaveValue<T, string> where T : DataSaveValue<T, string>
    {
        public DataSaveValueDictonary()
        {
            _jsonDictionary = new JsonDictionary<TKey, TValue>();
            ConvertFromJSON(ref _jsonDictionary);
        }

        private JsonDictionary<TKey, TValue> _jsonDictionary;

        public override string Value
        {
            get => _value;
            protected set
            {
                _value = value;
                data = _value;
                SendMessage();
            }
        }
        

        public Dictionary<TKey, TValue> ValueMap
        {
            get
            {
                ConvertFromJSON(ref _jsonDictionary);
                return new Dictionary<TKey, TValue>(_jsonDictionary.Dictionary);
            }
            set
            {
                _jsonDictionary.SetDictionary(value);
                Value = ConvertToJSON(ref _jsonDictionary);
            }
        }

        private void ConvertFromJSON(ref JsonDictionary<TKey, TValue> jsonDictionary)
        {
            if (data != "")
            {
                jsonDictionary = JsonUtility.FromJson<JsonDictionary<TKey, TValue>>(data);
                jsonDictionary.SetKeyValue(jsonDictionary.KeyValues);
            }
            else jsonDictionary.SetDictionary(new Dictionary<TKey, TValue>());
        }

        private string ConvertToJSON(ref JsonDictionary<TKey, TValue> jsonDictionary)
        {
            return JsonUtility.ToJson(jsonDictionary);
        }
    }

    #endregion

    #region Variable Data Types

    //////////////// VARIABLE DATA TYPES /////////////////

    public abstract class DataVariableValueInt<T> : DataVariableValue<T, int> where T : DataVariableValue<T, int>
    {
        public DataVariableValueInt(int defaultValue) : base(defaultValue) { }

        protected virtual int Min => Int32.MinValue;
        protected virtual int Max => Int32.MaxValue;

        public override int Value
        {
            get => _value;
            protected set
            {
                _value = Mathf.Clamp(value, Min, Max);
                SendMessage();
            }
        }
    }

    public abstract class DataVariableValueFloat<T> : DataVariableValue<T, float> where T : DataVariableValue<T, float>
    {
        public DataVariableValueFloat(float defaultValue) : base(defaultValue) { }

        protected virtual float Min => float.MinValue;
        protected virtual float Max => float.MaxValue;

        public override float Value
        {
            get => _value;
            protected set
            {
                _value = Mathf.Clamp(value, Min, Max);
                SendMessage();
            }
        }
    }

    public abstract class DataVariableValueBool<T> : DataVariableValue<T, bool> where T : DataVariableValue<T, bool>
    {
        public DataVariableValueBool(bool defaultValue) : base(defaultValue) { }

        public override bool Value
        {
            get => _value;
            protected set
            {
                _value = value;
                SendMessage();
            }
        }

        public void SetValue(bool value) => Value = value;
    }

    public abstract class DataVariableValueString<T> : DataVariableValue<T, string> where T : DataVariableValue<T, string>
    {
        public DataVariableValueString(string defaultValue) : base(defaultValue) { }

        public override string Value
        {
            get => _value;
            protected set
            {
                _value = value;
                SendMessage();
            }
        }

        public void SetValue(string value) => Value = value;
    }

    public abstract class DataVariableValueMod<T> : DataVariableValueFloat<T> where T : DataVariableValueFloat<T>
    {
        protected Dictionary<object, float> Modificators;
        protected Dictionary<object, float> Percentages;

        protected virtual float MinPercentage => 0f;
        protected virtual float MaxPercentage => int.MaxValue;

        protected virtual float MinModificator => 0f;
        protected virtual float MaxModificator => int.MaxValue;

        protected DataVariableValueMod(float defaultValue) : base(defaultValue) 
        {
            Modificators = new Dictionary<object, float>();
            Percentages = new Dictionary<object, float>();
            Refresh();
        }

        public float BaseValue => base.Value;

        public override float Value
        {
            get => _completeValue;
            protected set
            {
                _value = value;
                Refresh();
                SendMessage();
            }
        }

        protected float _completeValue;

        protected void Refresh()
        {
            float value = BaseValue + CompleteModificator();
            value = Mathf.Clamp(value, MinModificator, MaxModificator);
            value *= (CompletePercentage() * 0.01f);
            _completeValue = Mathf.Clamp(value, Min, Max);
        }

        public bool AddModificator(object obj, float modificator, bool add = false)
        {
            if (Modificators.ContainsKey(obj))
            {
                Modificators[obj] = add ? Modificators[obj] + modificator : modificator;
                Refresh();
                SendMessage();
                return false;
            }
            else
            {
                Modificators.Add(obj, modificator);
                Refresh();
                SendMessage();
                return true;
            }
        }

        public bool RemoveModificator(object obj)
        {
            if (Modificators.ContainsKey(obj))
            {
                Modificators.Remove(obj);
                Refresh();
                SendMessage();
                return true;
            }

            return false;
        }

        public bool AddPercentageModificator(object obj, float percentage, bool add = false)
        {
            if (Percentages.ContainsKey(obj))
            {
                Percentages[obj] = add ? Percentages[obj] + percentage : percentage;
                Refresh();
                SendMessage();
                return false;
            }
            else
            {
                Percentages.Add(obj, percentage);
                Refresh();
                SendMessage();
                return true;
            }
        }

        public bool RemovePercentageModificator(object obj)
        {
            if (Percentages.ContainsKey(obj))
            {
                Percentages.Remove(obj);
                Refresh();
                SendMessage();
                return true;
            }

            return false;
        }


        private float CompletePercentage()
        {
            float result = 100f;
            foreach (KeyValuePair<object, float> kvp in Percentages)
            {
                result += Percentages[kvp.Key];
            }

            return Mathf.Clamp(result, MinPercentage, MaxPercentage);
        }

        private float CompleteModificator()
        {
            float result = 0;
            foreach (KeyValuePair<object, float> kvp in Modificators)
            {
                result += Modificators[kvp.Key];
            }

            return result;
        }
    }

    public abstract class DataVariableValueStringArray<T> : DataVariableValue<T, string[]> where T : DataVariableValue<T, string[]>
    {
        public DataVariableValueStringArray(string[] defaultValue) : base(defaultValue) { }
        public override string[] Value
        {
            get => _value;
            protected set
            {
                _value = value;
                SendMessage();
            }
        }

        public void SetValue(string[] value) => Value = value;
    }

    public abstract class DataVariableValueDictonary<T, TKey, TValue> : DataVariableValue<T, Dictionary<TKey, TValue>> where T : DataVariableValue<T, Dictionary<TKey, TValue>>
    {
        public DataVariableValueDictonary(Dictionary<TKey, TValue> dictonary) : base(dictonary) { }

        public virtual TValue this[TKey key]
        {
            get => Get(key);
            set => Set(key, value);
        }

        protected virtual TValue Get(TKey key)
        {
            if (Value.ContainsKey(key)) return Value[key];
            else return default(TValue);
        }

        protected virtual void Set(TKey key, TValue value)
        {
            if (Value.ContainsKey(key)) Value[key] = value;
            else Value.Add(key, value);
            SendMessage();
        }
    }

    #endregion

    #region Modules

    [Serializable]
    public struct JsonDictionary<TKey, TValue>
    {
        public Dictionary<TKey, TValue> Dictionary;
        public JsonKeyValue[] KeyValues;

        public JsonDictionary(Dictionary<TKey, TValue> dictionary)
        {
            Dictionary = dictionary;
            KeyValues = default;
            SetDictionary(dictionary);
        }

        public JsonDictionary(JsonKeyValue[] keyValues)
        {
            Dictionary = null;
            KeyValues = keyValues;
            SetKeyValue(keyValues);
        }

        public void SetDictionary(Dictionary<TKey, TValue> dictionary)
        {
            Dictionary = new Dictionary<TKey, TValue>(dictionary);
            KeyValues = new JsonKeyValue[dictionary.Count];

            int i = 0;
            foreach (KeyValuePair<TKey, TValue> kvp in Dictionary)
                KeyValues[i++] = new JsonKeyValue(kvp.Key, kvp.Value);
        }

        public void SetKeyValue(JsonKeyValue[] keyValues)
        {
            Dictionary = new Dictionary<TKey, TValue>();
            KeyValues = keyValues;

            if (KeyValues != null)
            {
                foreach (JsonKeyValue kvp in KeyValues)
                    Dictionary.Add(kvp.Key, kvp.Value);
            }
        }

        [Serializable]
        public struct JsonKeyValue
        {
            public TKey Key;
            public TValue Value;

            public JsonKeyValue(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
    }

    #endregion
}