using System.ComponentModel;

namespace rtUtility.rtControl
{
    using Control = System.Windows.Forms.Control;

    public static class ComponentExtension
    {
        public static bool GetDesignMode(this Component aComponent)
        {
            bool mode = (aComponent.Site == null) ? false : aComponent.Site.DesignMode;

            if (!(aComponent is Control))
                return mode;

            Control parent = (aComponent as Control).Parent;
            while ((!mode) && (parent != null)) {
                ISite site = parent.Site;
                if (site != null)
                    mode = site.DesignMode;

                parent = parent.Parent;
            }

            return mode;
        }
    }
}
