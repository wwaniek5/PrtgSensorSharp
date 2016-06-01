﻿using System;
using FluentAssertions;
using NUnit.Framework;

namespace PrtgSensorSharp.Tests
{
    public class PrtgExeScriptAdvancedSensorTests
    {
        [Test]
        public void can_print_prtg_report_to_the_console()
        {
            using (var consoleOutput = CapturedConsoleOutput.StartCapturing())
            {
                PrtgExeScriptAdvanced.Run(() => PrtgReport.Successful(new[]
                {
                    new PrtgResult("First channel", 10),
                    new PrtgResult("Second channel", 20)
                }));

                consoleOutput.ReadAll().Should().Be(
                    "<prtg>" +
                        "<result>" +
                            "<channel>First channel</channel>" +
                            "<value>10</value>" +
                        "</result>" +
                        "<result>" +
                            "<channel>Second channel</channel>" +
                            "<value>20</value>" +
                        "</result>" +
                    "</prtg>"
                );
            }
        }

        [Test]
        public void returns_failed_report_when_generating_report_throws_exception()
        {
            using (var consoleOutput = CapturedConsoleOutput.StartCapturing())
            {
                PrtgExeScriptAdvanced.Run(() =>
                {
                    throw new Exception("welp!");
                });

                consoleOutput.ReadAll().Should().Match(
                    "<prtg>" +
                        "<error>1</error>" +
                        "<text>Sensor has failed - unhandled exception was thrown.</text>" +
                    "</prtg>"
                );
            }
        }
    }
}