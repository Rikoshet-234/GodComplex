﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebServices.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebServices.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to // Code from https://stackoverflow.com/questions/5706837/get-unique-selector-of-element-in-jquery
        /////
        ///function getXPath(node, path) {
        ///	path = path || [];
        ///	if(node.parentNode) {
        ///		path = getXPath(node.parentNode, path);
        ///	}
        ///
        ///	if(node.previousSibling) {
        ///		var count = 1;
        ///		var sibling = node.previousSibling
        ///		do {
        ///		if(sibling.nodeType == 1 &amp;&amp; sibling.nodeName == node.nodeName) {count++;}
        ///		sibling = sibling.previousSibling;
        ///		} while(sibling);
        ///		if(count == 1) {count = null;}
        ///	} else if(node.nex [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Helpers {
            get {
                return ResourceManager.GetString("Helpers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // Returns the FIRST top-most element that contains ALL elements that have content (i.e. image or text)
        ///function RecurseRetrieveTopMostContentContainer( _node, _path ) {
        /////console.log( &quot;Examining node &quot; + _path + &quot; (ID = &quot; + _node.id + &quot; - Type = &quot; + _node.nodeType + &quot; - Tag = &quot; + _node.tagName + &quot; - Value = &quot; + _node.nodeValue + &quot; - XPath = &quot; + getXPath( _node ) + &quot;)&quot; );
        /////if ( _node.outerHTML !== undefined )
        /////	console.log( &quot;Outer HTML = &quot; + (_node.outerHTML.length &lt; 100 ? _node.outerHTML : _node.oute [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string IsolateMainContent {
            get {
                return ResourceManager.GetString("IsolateMainContent", resourceCulture);
            }
        }
    }
}
