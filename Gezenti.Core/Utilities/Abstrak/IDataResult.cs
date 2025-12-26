using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gezenti.Core.Utilities.Abstrak
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }

    }
}
