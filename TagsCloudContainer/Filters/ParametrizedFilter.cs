﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace TagsCloudContainer.Filters
{
    public class ParametrizedFilter : IFilter
    {
        private readonly Func<string, bool> filterFunc;

        public ParametrizedFilter(Func<string, bool> filterFunc)
        {
            this.filterFunc = filterFunc;
        }
        
        public IEnumerable<string> Filter(IEnumerable<string> values)
        {
            return values.Where(filterFunc);
        }
    }
}
