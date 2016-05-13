﻿using System;
using System.Xml.Linq;

namespace PrtgExeScriptSensor
{
    public static class PrtgExeScriptAdvanced
    {
        public static void Run(Func<IPrtgReport> probe)
        {
            var report = GenerateReport(probe);

            var serializedReport = report.Serialize().ToString(SaveOptions.DisableFormatting);

            Console.Write(serializedReport);
        }

        public static IPrtgReport GenerateReport(Func<IPrtgReport> probe)
        {
            try
            {
                return probe();
            }
            catch (Exception exception)
            {
                return PrtgReport.Failed(exception.ToString());
            }
        }
    }
}