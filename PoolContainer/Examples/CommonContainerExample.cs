using PoolsContainer.Core;

namespace PoolsContainer.Example
{
    public class CommonContainerExample : MonoSingleContainer<PoolObject>
    {
        protected override void Created(PoolObject obj)
        {

        }
    }
}