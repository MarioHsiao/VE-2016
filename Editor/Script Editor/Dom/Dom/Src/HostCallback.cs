// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Rajneesh Noonia" email="Rajneesh.Noonia@Xansa.com"/>
//     <version>$Revision: 2500 $</version>
// </file>

using System;
using AIMS.Libraries.Scripting.Dom.Refactoring;

namespace AIMS.Libraries.Scripting.Dom
{
	/// <summary>
	/// A class containing static actions that should be overridden by the
	/// application using AIMS.Libraries.Scripting.Dom.
	/// </summary>
	public static class HostCallback
	{
		/// <summary>
		/// Show an error message. (string message, Exception ex)
		/// </summary>
		public static Action<string, Exception> ShowError = delegate(string message, Exception ex) {
			LoggingService.Error(message, ex);
		};
		
		public static Action<string> ShowMessage = delegate(string message) {
			LoggingService.Info(message);
		};
		
		/// <summary>
		/// Get parse information by file name.
		/// </summary>
		public static Func<string, ParseInformation> GetParseInformation = delegate {
			throw new NotImplementedException("GetParseInformation was not implemented by the host.");
		};
		
		/// <summary>
		/// Get the current project content.
		/// </summary>
		public static Func<IProjectContent> GetCurrentProjectContent = delegate {
			throw new NotImplementedException("GetCurrentProjectContent was not implemented by the host.");
		};
		
		/// <summary>
		/// Rename the member (first argument) to the new name (second argument).
		/// Returns true on success, false on failure.
		/// </summary>
		public static Func<IMember, string, bool> RenameMember = delegate {
			return false;
		};
		
		public static Action<string> BeginAssemblyLoad = delegate {};
		
		public static Action FinishAssemblyLoad = delegate {};
		
		/// <summary>
		/// Show error loading code-completion information.
		/// The arguments are: string fileName, string include, string message
		/// </summary>
		public static Action<string, string, string> ShowAssemblyLoadError = delegate {};
		
		internal static void ShowAssemblyLoadErrorInternal(string fileName, string include, string message)
		{
			LoggingService.Warn("Error loading code-completion information for "
			                    + include + " from " + fileName
			                    + ":\r\n" + message + "\r\n");
			ShowAssemblyLoadError(fileName, include, message);
		}
		
		/// <summary>
		/// Initialize the code generator options of the passed CodeGenerator.
		/// Invoked exactly once for each created instance of a class derived from CodeGenerator.
		/// </summary>
		public static Action<CodeGenerator> InitializeCodeGeneratorOptions = delegate {};
	}
}
