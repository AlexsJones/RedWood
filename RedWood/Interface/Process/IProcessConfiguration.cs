using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedWood.Interface.Process
{
    public interface IProcessConfiguration
    {
        string FetchFileName();

        string FetchArguments();

    }
}
