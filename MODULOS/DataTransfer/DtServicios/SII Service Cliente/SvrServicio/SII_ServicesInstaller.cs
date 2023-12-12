using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

using Microsoft.Win32; 

namespace SII_Servicio_Cliente.SvrServicio
{
    [RunInstaller(true)]
    public partial class SII_Servicio_ClienteInstaller : Installer
    {
        public SII_Servicio_ClienteInstaller()
        {
            InitializeComponent();
            ////////// HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services  

            ////////// Here is where we set the bit on the value in the registry.
            ////////// Grab the subkey to our service
            //////RegistryKey ckey = Registry.LocalMachine.OpenSubKey(string.Format(@"SYSTEM\CurrentControlSet\Services\{0}", serviceInstaller.ServiceName), true);
            
            ////////// Good to always do error checking!
            //////if (ckey != null)
            //////{
            //////    //// Ok now lets make sure the "Type" value is there, 
            //////    ////and then do our bitwise operation on it.
            //////    if (ckey.GetValue("Type") != null)
            //////    {
            //////        ////ckey.SetValue("Type", ((int)ckey.GetValue("Type") | 256));
            //////        ckey.SetValue("Type", 272);
            //////    }
            //////}
        }
    }
}
