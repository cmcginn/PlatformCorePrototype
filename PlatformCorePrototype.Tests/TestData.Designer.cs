﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlatformCorePrototype.Tests {
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
    public class TestData {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TestData() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PlatformCorePrototype.Tests.TestData", typeof(TestData).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to { 
        ///    &quot;_id&quot; : {
        ///        &quot;ColumnName&quot; : &quot;Child 1 Level 2&quot;, 
        ///        &quot;_id&quot; : ObjectId(&quot;56154ccccd158746a441b27d&quot;)
        ///    }, 
        ///    &quot;Value&quot; : &quot;TestNodeChild&quot;, 
        ///    &quot;Ancestors&quot; : [
        ///        {
        ///            &quot;ColumnName&quot; : &quot;Root&quot;, 
        ///            &quot;_id&quot; : ObjectId(&quot;56154ccccd158746a441b27a&quot;)
        ///        }, 
        ///        {
        ///            &quot;ColumnName&quot; : &quot;Child 2 Level 1&quot;, 
        ///            &quot;_id&quot; : ObjectId(&quot;56154ccccd158746a441b27c&quot;)
        ///        }
        ///    ], 
        ///    &quot;Level&quot; : NumberInt(2), 
        ///    &quot;Path&quot; : &quot;Root.Child 2 Level 1&quot;
        ///}.
        /// </summary>
        public static string NestedTreeNodeBson {
            get {
                return ResourceManager.GetString("NestedTreeNodeBson", resourceCulture);
            }
        }
    }
}
