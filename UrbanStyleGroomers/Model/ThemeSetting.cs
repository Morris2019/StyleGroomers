using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class ThemeSetting
    {
        public int Id { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string SidebarBgColor { get; set; }
        public string SidebarTextColor { get; set; }
        public string TopbarTextColor { get; set; }
        public string CustomCss { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
