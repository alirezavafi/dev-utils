﻿using Microsoft.Maui.Layouts;

namespace DevUtils.Layouts
{
    public class HorizontalWrapLayout : StackLayout
    {
        public HorizontalWrapLayout()
        {
        }

        protected override ILayoutManager CreateLayoutManager()
        {
            return new HorizontalWrapLayoutManager(this);
        }
    }
}
