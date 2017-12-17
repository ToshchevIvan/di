using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.Tags;


namespace TagsCloudContainer.Renderers
{
    public interface IRenderer<TResult> : IDisposable
    {
        Result<TResult> Render(IEnumerable<ITag> tags);
    }
}
