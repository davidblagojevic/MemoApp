using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Wrapper
{
    public class Result<T>: IResult<T>
    {
        public T Value { get; set; }
        public OutcomeEnum Outcome { get; set; }
    }

    public enum OutcomeEnum
    {
        Success,
        Failure
    }
}
