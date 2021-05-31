using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Wrapper
{
    public interface IResult<T>
    {
        public T Value { get; set; }
        public OutcomeEnum Outcome { get; set; }
    }
}
