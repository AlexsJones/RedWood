using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Config;
using System.Runtime.InteropServices;
[assembly: AssemblyCompany("LIGHTLANCE LTD")]
[assembly: AssemblyCopyright("Copyright © LIGHTLANCE LTD 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(true)]
[assembly: AssemblyVersion("1.0.0.33")]
// By default, the "Product version" shown in the file properties window is
// the same as the value specified for AssemblyFileVersionAttribute.
// Set AssemblyInformationalVersionAttribute to be the same as
// AssemblyVersionAttribute so that the "Product version" in the file
// properties window matches the version displayed in the GAC shell extension.
[assembly: AssemblyInformationalVersion("1.0.0.33")] // a.k.a. "Product version"
[assembly: log4net.Config.XmlConfigurator(Watch = true)]