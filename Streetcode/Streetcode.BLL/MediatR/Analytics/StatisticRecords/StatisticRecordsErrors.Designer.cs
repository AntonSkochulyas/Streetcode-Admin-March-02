﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class StatisticRecordsErrors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal StatisticRecordsErrors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Streetcode.BLL.MediatR.Analytics.StatisticRecords.StatisticRecordsErrors", typeof(StatisticRecordsErrors).Assembly);
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
        ///   Looks up a localized string similar to Address max length should not be longer than {0} symbols..
        /// </summary>
        internal static string CreateStatisticRecordCommandValidatorAddressMaxLengthError {
            get {
                return ResourceManager.GetString("CreateStatisticRecordCommandValidatorAddressMaxLengthError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can not map statistic record..
        /// </summary>
        internal static string CreateStatisticRecordHandlerCanNotMapError {
            get {
                return ResourceManager.GetString("CreateStatisticRecordHandlerCanNotMapError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to create a statistic record..
        /// </summary>
        internal static string CreateStatisticRecordHandlerFailedToCreateError {
            get {
                return ResourceManager.GetString("CreateStatisticRecordHandlerFailedToCreateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Qr id should be unique per one streetcode..
        /// </summary>
        internal static string CreateStatisticRecordHandlerQrIdShoulBeUniqueError {
            get {
                return ResourceManager.GetString("CreateStatisticRecordHandlerQrIdShoulBeUniqueError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can not find a statistic record with given id: {0}..
        /// </summary>
        internal static string DeleteStatisticRecordHandlerCanNotFindWithGivenIdError {
            get {
                return ResourceManager.GetString("DeleteStatisticRecordHandlerCanNotFindWithGivenIdError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to delete a statistic record..
        /// </summary>
        internal static string DeleteStatisticRecordHandlerFailedToDeleteError {
            get {
                return ResourceManager.GetString("DeleteStatisticRecordHandlerFailedToDeleteError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can not get any statistic records..
        /// </summary>
        internal static string GetAllStatisticRecordsHandlerCanNotGetAnyError {
            get {
                return ResourceManager.GetString("GetAllStatisticRecordsHandlerCanNotGetAnyError", resourceCulture);
            }
        }
    }
}
