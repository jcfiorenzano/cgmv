﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cgmv {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("cgmv.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to The type {0} is not supported yet for this tool, please contact the open source engineering alias to include it.
        /// </summary>
        internal static string ComponentTypeNotSupportedMessage {
            get {
                return ResourceManager.GetString("ComponentTypeNotSupportedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Incorrect component definition. The component type is {0}, but the component definition is for: {1}..
        /// </summary>
        internal static string IncorrectComponentDefinition {
            get {
                return ResourceManager.GetString("IncorrectComponentDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The component of type {0} do not have a component definition..
        /// </summary>
        internal static string MissingComponentDefinition {
            get {
                return ResourceManager.GetString("MissingComponentDefinition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The component type is missing..
        /// </summary>
        internal static string MissingComponentType {
            get {
                return ResourceManager.GetString("MissingComponentType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The property {0} is required and was not specified. This happens if the property has a typo or was omitted.
        /// </summary>
        internal static string MissingRequiredProperty {
            get {
                return ResourceManager.GetString("MissingRequiredProperty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Type {0} is no longer supported by Governance .
        /// </summary>
        internal static string TypeIsNoLongerSupported {
            get {
                return ResourceManager.GetString("TypeIsNoLongerSupported", resourceCulture);
            }
        }
    }
}
