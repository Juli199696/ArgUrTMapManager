using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>
	/// Used to be able to use Extension-Methods with .NET 2.0.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class ExtensionAttribute : Attribute { }
}
