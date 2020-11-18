using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MRI.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ImagePointDataModel : BaseDataModel
    {

        public int Text { get; private set; }

        public PointF CenterPoint { get; private set; } = new PointF();

        public ImagePointDataModel(
            string text, PointF point, float width, float height)
        {
            Text = Int32.Parse(text);

            var _x = point.X + width / 2;
            var _y = point.Y + height / 2;

            CenterPoint = new PointF(_x, _y);

        }



    }
}
