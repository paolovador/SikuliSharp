﻿using System;
using System.IO;

namespace SikuliSharp
{
	public static class Sikuli
	{
		public static ISikuliSession CreateSession()
		{
			return new SikuliSession(CreateRuntime());
		}

		public static SikuliRuntime CreateRuntime()
		{
			return new SikuliRuntime(
				new AsyncDuplexStreamsHandlerFactory(),
				new SikuliScriptProcessFactory()
				);
		}

		public static string RunProject(string projectPath, string projectScriptFilePath)
		{
			return RunProject(projectPath, projectScriptFilePath, null);
		}

		public static string RunProject(string projectPath, string projectScriptFilePath, string args)
		{
			if (projectPath == null) throw new ArgumentNullException("projectPath");

			if (!Directory.Exists(projectPath))
				throw new DirectoryNotFoundException(string.Format("Project not found in path '{0}'", projectPath));

			var processFactory = new SikuliScriptProcessFactory();
			var version = processFactory.GetSikuliVersion().WithProject(projectPath, projectScriptFilePath, args);
			using (var process = processFactory.Start(version))
			{
				var output = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
				return output;
			}
		}
	}
}
