using System;
using System.Collections.Generic;
using System.Reflection;

namespace ArgusLib
{
	/// <summary>
	/// Use this <see cref="Attribute"/> to tell that the object the attribute is applied to needs
	/// a specific assembly to be loaded.
	/// </summary>
    public class RequiresAssemblyAttribute : Attribute
    {
		/// <summary>
		/// Full name of the required assembly.
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// Specifies that the object to which the attribute is applied
		/// requires the assembly specified by <paramref name="FullName"/>
		/// to be loaded.
		/// </summary>
		/// <param name="FullName">
		/// The full name of the assembly that will be required.
		/// </param>
		public RequiresAssemblyAttribute(string FullName)
		{
			this.FullName = FullName;
		}

		/// <summary>
		///	Specifies that the object to which the attribute is applied
		///	requires the assembly in which the specified class is defined to be loaded.
		/// </summary>
		/// <param name="type">
		/// A <see cref="Type"/> object representing the class in the assembly
		/// that will be required.
		/// </param>
		public RequiresAssemblyAttribute(Type type)
		{
			Assembly a = Assembly.GetAssembly(type);
			this.FullName = a.FullName;
		}
    }
}
