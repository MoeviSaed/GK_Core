
namespace PoolsContainer.Example
{
    public class CommonContainer : MonoSingleContainer<PoolObject>
    {
        protected override void Created(PoolObject obj)
        {

        }
    }
}