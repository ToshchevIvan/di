using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.CloudObjects;


namespace TagsCloudContainer.Renderers
{
    public interface IRenderer<out TResult> : IDisposable
    {
        TResult Render(IEnumerable<ITag> tags);
    }
}
