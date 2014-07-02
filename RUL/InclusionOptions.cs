using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUL
{
    public enum InclusionOptions
    {
        /// <summary>
        /// Includes both the lower and the upper bound
        /// </summary>
        Both,
        /// <summary>
        /// Only includes the lower bound
        /// </summary>
        Lower,
        /// <summary>
        /// Only includes the upper bound
        /// </summary>
        Upper,
        /// <summary>
        /// Both bounds are excluded
        /// </summary>
        None
    }
}
