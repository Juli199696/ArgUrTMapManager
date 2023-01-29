using System;
using System.Collections.Generic;

namespace ArgUrTMapManager
{
	struct ShaderscriptEntry
	{
		//const string SurfaceparmNoDamage = "surfaceparm nodamage";

		public string Name { get; set; }
		public string Content { get; set; }

		//public ShaderscriptEntry ApplyNoDamage(string NamePreFix, string NamePostFix)
		//{
		//	if (this.Content.Contains(ShaderscriptEntry.SurfaceparmNoDamage) == true)
		//		return this;
		//	ShaderscriptEntry shader = new ShaderscriptEntry();
		//	int i = this.Name.LastIndexOf('/')+1;
		//	if (i < 0)
		//		i = 0;
		//	shader.Name = this.Name.Substring(0, i)
		//		+ NamePreFix
		//		+ this.Name.Substring(i)
		//		+ NamePostFix;
		//	shader.Content = this.Content.Insert(
		//		0,
		//		"\n\t" + ShaderscriptEntry.SurfaceparmNoDamage);
		//	return shader;
		//}
	}
}
