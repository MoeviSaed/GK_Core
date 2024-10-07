using GTC;
using PoolsContainer.Core;
using UnityEngine;

namespace PoolsContainer.Example
{
    public class CommonContainer : MonoSingleContainer<PoolObject>
    {
        protected override void Created(PoolObject obj)
        {

        }
    }
}