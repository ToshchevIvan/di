using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.Tags;


namespace TagsCloudContainer.Renderers
{
    public interface IRenderer<TResult> : IDisposable
    {
        TResult Render(IEnumerable<ITag> tags);
    }
}
