﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CoreBank
{
    public class Paths
    {
                // Paths 
        public string ConfigFile;
        public string ProjectPath;
        public string TestCasePath;
        public string ResourcePath;             
        public string TempPath;                 // Temporary user path
        public string ConfigPath;               // Configuration path
        public string ProcessPath;              // Process path
        
        public Paths()
        {
            ConfigFile = Global.Default._ConfigFile;
            ProcessPath = Global.Default._ProcessPath;
            ProjectPath = Global.Default._ProjectPath;
            ResourcePath = Global.Default._ResourcePath;
            TestCasePath = Global.Default._TestCasePath;

            // Set run folder in temp map
            TempPath = System.Environment.GetEnvironmentVariable("TEMP");
            ConfigPath = Path.Combine(TempPath, ConfigFile);
            
        }

    }
}
