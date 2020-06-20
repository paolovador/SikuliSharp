using System;

namespace SikuliSharp
{
	public interface ISikuliVersion
	{
		string Arguments { get; }
		string ReadyMarker { get; }
		string[] InitialCommands { get; }
		ISikuliVersion WithProject(string projectPath, string projectScriptFilePath, string args);
	}

	public class Sikuli101Version : ISikuliVersion
	{
		private readonly string _jar;
		public string ReadyMarker =>  "... use ctrl-d to end the session";
		public string[] InitialCommands => null;
		public string Arguments { get; private set; }

		public Sikuli101Version(string sikuliScriptJar)
		{
			_jar = sikuliScriptJar;
			Arguments = string.Format("-jar \"{0}\" -i", _jar);
		}

		public ISikuliVersion WithProject(string projectPath, string projectScriptFilePath, string args)
		{
			Arguments = string.Format("-jar \"{0}\" -r {1} {2}", _jar, projectPath, args);
			return this;
		}
	}

	public class Sikuli110Version : ISikuliVersion
	{
		private readonly string _jar;
		public string ReadyMarker =>  "... use ctrl-d to end the session";
		public string[] InitialCommands => null;
		public string Arguments { get; private set; }

		public Sikuli110Version(string sikuliXJar)
		{
			_jar = sikuliXJar;
			Arguments = string.Format("-jar \"{0}\" -i", _jar);
		}

		public ISikuliVersion WithProject(string projectPath, string projectScriptFilePath, string args)
		{
			Arguments = string.Format("-jar \"{0}\" -r {1} {2}", _jar, projectPath, args);
			return this;
		}
	}

	public class Sikuli114Version : ISikuliVersion
	{
		private readonly string _apiJar;
		private readonly string _jythonJar;
		public string ReadyMarker => null; // "Use exit() or Ctrl-D (i.e. EOF) to exit";
		public string[] InitialCommands => new[]
		{
			"import org.sikuli.script.SikulixForJython",
			"from sikuli.Sikuli import *"
		};
		public string Arguments { get; private set; }


		public Sikuli114Version(string apiJar, string jythonJar)
		{
			_apiJar = apiJar;
			_jythonJar = jythonJar;
			Arguments = string.Format("-cp \"{0};{1}\" org.python.util.jython", _apiJar, _jythonJar);
		}

		public ISikuliVersion WithProject(string projectPath, string projectScriptFilePath, string args)
		{
			//Arguments = string.Format("-cp \"{0};{1}\" org.python.util.jython \"{2}\" {3}", _apiJar, _jythonJar, projectScriptFilePath, args);
			Arguments = string.Format("-jar \"{0}\" -r {1} {2}", _apiJar, projectPath, args);
			return this;
		}
	}
}