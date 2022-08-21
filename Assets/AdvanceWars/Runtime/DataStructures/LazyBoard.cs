using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RGV.DesignByContract.Runtime.Contract;

namespace AdvanceWars.Runtime.DataStructures
{
    public class LazyBoard<T> : IDictionary<Vector2Int, T> where T : new()
    {
        readonly IDictionary<Vector2Int, T> delegated = new Dictionary<Vector2Int, T>();

        public T this[Vector2Int key]
        {
            get => delegated.ContainsKey(key) ? delegated[key] : delegated[key] = new T();
            set => delegated[key] = value;
        }

        #region IDictionary implementation
        public IEnumerator<KeyValuePair<Vector2Int, T>> GetEnumerator()
        {
            return delegated.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)delegated).GetEnumerator();
        }

        public void Add(KeyValuePair<Vector2Int, T> item)
        {
            delegated.Add(item);
        }

        public void Clear()
        {
            delegated.Clear();
        }

        public bool Contains(KeyValuePair<Vector2Int, T> item)
        {
            return delegated.Contains(item);
        }

        public void CopyTo(KeyValuePair<Vector2Int, T>[] array, int arrayIndex)
        {
            delegated.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<Vector2Int, T> item)
        {
            return delegated.Remove(item);
        }

        public int Count => delegated.Count;

        public bool IsReadOnly => delegated.IsReadOnly;

        public void Add(Vector2Int key, T value)
        {
            delegated.Add(key, value);
        }

        public bool ContainsKey(Vector2Int key)
        {
            return delegated.ContainsKey(key);
        }

        public bool Remove(Vector2Int key)
        {
            return delegated.Remove(key);
        }

        public bool TryGetValue(Vector2Int key, out T value)
        {
            return delegated.TryGetValue(key, out value);
        }

        public ICollection<Vector2Int> Keys => delegated.Keys;

        public ICollection<T> Values => delegated.Values;
        #endregion

        public Vector2Int CoordsOf(T t)
        {
            var value = delegated.FirstOrDefault(x => x.Value.Equals(t));
            Require(value).Not.Null();

            return value.Key;
        }
    }
}