﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArgUrTMapManager.ResourceFiles {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ArgUrTMapManager.ResourceFiles.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.IO.UnmanagedMemoryStream similar to System.IO.MemoryStream.
        /// </summary>
        internal static System.IO.UnmanagedMemoryStream EmptyWav {
            get {
                return ResourceManager.GetStream("EmptyWav", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ReadMe.txt.
        /// </summary>
        internal static string MapExtractReadMeStandardFilename {
            get {
                return ResourceManager.GetString("MapExtractReadMeStandardFilename", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This map was extracted from its original game including all required assets (textures, shaders, models, sounds, etc.)
        ///with ArgUrT Map Manager (argurtmapmanager.codeplex.com).
        ///The map is unmodified, except maybe for some entity-editing to enable it to being played in other games
        ///(e.g. inserting some game-specific spawnpoint-specifier).
        ///
        ///--------------------------------------------------------------------------------------------------------------------------
        ///The map and the assets belong to their respec [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string MapExtractReadMeStandardText {
            get {
                return ResourceManager.GetString("MapExtractReadMeStandardText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] QuakeLiveXorKey {
            get {
                object obj = ResourceManager.GetObject("QuakeLiveXorKey", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
