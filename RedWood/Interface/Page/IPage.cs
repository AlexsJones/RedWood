using RedWood.Interface.Debug;
using RedWood.Interface.Driver;

namespace RedWood.Interface.Page
{
    public interface IPage
    {
        IDriver Driver { get;  }
        ILogger Logger { get;  }
    }
}
