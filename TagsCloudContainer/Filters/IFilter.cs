﻿using System.Collections.Generic;


namespace TagsCloudContainer.Filters
{
    public interface IFilter
    {
        IEnumerable<string> Filter(IEnumerable<string> values);
    }
}
