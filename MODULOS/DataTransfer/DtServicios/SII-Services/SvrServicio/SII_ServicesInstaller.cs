using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;


namespace SII_Services.SvrServicio
{
    [RunInstaller(true)]
    public partial class SII_ServicesInstaller : Installer
    {
        public SII_ServicesInstaller()
        {
            InitializeComponent();
        }
    }
}
