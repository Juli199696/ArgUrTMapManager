using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ArgusLib
{
	public static class AssemblyRuntimeCheck
	{
		public static bool IsLoaded(Assembly assembly)
		{
			Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly a in loadedAssemblies)
			{
				if (a.FullName == assembly.FullName)
					return true;
			}
			return false;
		}

		public static bool IsLoaded(Type type)
		{
			return AssemblyRuntimeCheck.IsLoaded(Assembly.GetAssembly(type));
		}
	}
}
