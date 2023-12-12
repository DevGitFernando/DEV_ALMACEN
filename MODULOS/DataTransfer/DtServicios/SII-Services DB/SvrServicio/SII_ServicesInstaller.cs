using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;


namespace SII_Services_DB.SvrServicio
{
    [RunInstaller(true)]
    public partial class SII_Services_DBInstaller : Installer
    {
        public SII_Services_DBInstaller()
        {
            InitializeComponent();
        }
    }
}
