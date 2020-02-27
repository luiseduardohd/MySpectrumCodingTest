using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Security;

namespace MySpectrumCodingTest.iOS.Security
{
    public static class KeychainHelper
    {
        /// <summary>
        ///     Deletes a username/password record.
        /// </summary>
        /// <param name="userName">The username to query. Not case sensitive. May not be NULL.</param>
        /// <param name="serviceId">The service description to query. Not case sensitive.  May not be NULL.</param>
        /// <param name="synchronizable">
        ///     Defines if the record you want to delete is syncable via iCloud keychain or not. Note that using the same username
        ///     and service ID but different synchronization settings will result in two keychain entries.
        /// </param>
        /// <returns>Status code</returns>
        public static SecStatusCode DeletePasswordForUsername(string userName, string serviceId, bool synchronizable = false)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }

            if (serviceId == null)
            {
                throw new ArgumentNullException("serviceId");
            }

            // Querying is case sensitive - we don't want that.
            userName = userName.ToLower();
            serviceId = serviceId.ToLower();

            // Query and remove.
            var queryRecord = new SecRecord(SecKind.GenericPassword)
            {
                Service = serviceId,
                Label = serviceId,
                Account = userName,
                Synchronizable = synchronizable
            };

            var code = SecKeyChain.Remove(queryRecord);
            return code;
        }

        /// <summary>
        ///     Sets a password for a specific username.
        /// </summary>
        /// <param name="userName">The username to add the password for. Not case sensitive.  May not be NULL.</param>
        /// <param name="password">The password to associate with the record. May not be NULL.</param>
        /// <param name="serviceId">The service description to use. Not case sensitive.  May not be NULL.</param>
        /// <param name="secAccessible">Defines how the keychain record is protected.</param>
        /// <param name="synchronizable">
        ///     Defines if keychain record can by synced via iCloud keychain.
        ///     Note that using the same username and service ID but different synchronization settings will result in two keychain
        ///     entries.
        /// </param>
        /// <returns>SecStatusCode.Success if everything went fine, otherwise some other status.</returns>
        public static SecStatusCode SetPasswordForUsername(string userName, string password, string serviceId,
            SecAccessible secAccessible, bool synchronizable = false)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            if (string.IsNullOrEmpty(serviceId))
                throw new ArgumentNullException("serviceId");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            // Querying is case sensitive - we don't want that.
            userName = userName.ToLower();
            serviceId = serviceId.ToLower();

            // Don't bother updating. Delete existing record and create a new one.
            DeletePasswordForUsername(userName, serviceId, synchronizable);

            // Create a new record.
            // Store password UTF8 encoded.
            var code = SecKeyChain.Add(new SecRecord(SecKind.GenericPassword)
            {
                Service = serviceId,
                Label = serviceId,
                Account = userName,
                Generic = NSData.FromString(password, NSStringEncoding.UTF8),
                Accessible = secAccessible,
                Synchronizable = synchronizable
            });

            return code;
        }

        /// <summary>
        ///     Gets a password for a specific username.
        /// </summary>
        /// <param name="userName">The username to query. Not case sensitive.  May not be NULL.</param>
        /// <param name="serviceId">The service description to use. Not case sensitive.  May not be NULL.</param>
        /// <param name="synchronizable">
        ///     Defines if the record you want to get is syncable via iCloud keychain or not. Note that using the same username and
        ///     service ID but different synchronization settings will result in two keychain entries.
        /// </param>
        /// <returns>
        ///     The password or NULL if no matching record was found.
        /// </returns>
        public static string GetPasswordForUsername(string userName, string serviceId, bool synchronizable = false)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            if (string.IsNullOrEmpty(serviceId))
                throw new ArgumentNullException("serviceId");

            // Querying is case sensitive - we don't want that.
            userName = userName.ToLower();
            serviceId = serviceId.ToLower();

            // Query the record.
            SecStatusCode code;

            var queryRecord = new SecRecord(SecKind.GenericPassword)
            {
                Service = serviceId,
                Label = serviceId,
                Account = userName,
                Synchronizable = synchronizable
            };

            queryRecord = SecKeyChain.QueryAsRecord(queryRecord, out code);

            // If found, try to get password.
            if (code == SecStatusCode.Success && queryRecord != null && queryRecord.Generic != null)
            {
                // Decode from UTF8.
                return NSString.FromData(queryRecord.Generic, NSStringEncoding.UTF8);
            }

            // Something went wrong.
            return null;
        }


        public static SecStatusCode DeleteDeviceUniqueIdentifier(string deviceUniqueIdentifier, bool synchronizable = false)
        {
            if (string.IsNullOrEmpty(deviceUniqueIdentifier))
                throw new ArgumentNullException("deviceUniqueIdentifier");

            // Querying is case sensitive - we don't want that.
            var deviceUniqueIdentifierAccountName = SecurityConstants.DeviceUniqueIdentifierAccountName.ToLower();
            var serviceId = SecurityConstants.ServiceId.ToLower();

            // Query and remove.
            var queryRecord = new SecRecord(SecKind.GenericPassword)
            {
                Service = serviceId,
                Label = serviceId,
                Account = deviceUniqueIdentifierAccountName,
                Synchronizable = synchronizable
            };

            var code = SecKeyChain.Remove(queryRecord);
            return code;
        }
        public static SecStatusCode SetDeviceUniqueIdentifier(string deviceUniqueIdentifier, SecAccessible secAccessible,
            bool synchronizable = false)
        {
            if (string.IsNullOrEmpty(deviceUniqueIdentifier))
                throw new ArgumentNullException("deviceUniqueIdentifier");

            // Don't bother updating. Delete existing record and create a new one.
            DeleteDeviceUniqueIdentifier(deviceUniqueIdentifier, synchronizable);

            var deviceUniqueIdentifierAccountName = SecurityConstants.DeviceUniqueIdentifierAccountName.ToLower();
            var serviceId = SecurityConstants.ServiceId.ToLower();

            // Create a new record.
            // Store password UTF8 encoded.
            var code = SecKeyChain.Add(new SecRecord(SecKind.GenericPassword)
            {
                Service = serviceId,
                Label = serviceId,
                Account = deviceUniqueIdentifierAccountName,
                Generic = NSData.FromString(deviceUniqueIdentifier, NSStringEncoding.UTF8),
                Accessible = secAccessible,
                Synchronizable = synchronizable
            });

            return code;
        }
        public static string GetDeviceUniqueIdentifier(bool synchronizable = false)
        {
            // Querying is case sensitive - we don't want that.
            var deviceUniqueIdentifierAccountName = SecurityConstants.DeviceUniqueIdentifierAccountName.ToLower();
            var serviceId = SecurityConstants.ServiceId.ToLower();

            // Query the record.
            SecStatusCode code;

            var queryRecord = new SecRecord(SecKind.GenericPassword)
            {
                Service = serviceId,
                Label = serviceId,
                Account = deviceUniqueIdentifierAccountName,
                Synchronizable = synchronizable
            };

            queryRecord = SecKeyChain.QueryAsRecord(queryRecord, out code);

            // If found, try to get password.
            if (code == SecStatusCode.Success && queryRecord != null && queryRecord.Generic != null)
            {
                // Decode from UTF8.
                return NSString.FromData(queryRecord.Generic, NSStringEncoding.UTF8);
            }

            // Something went wrong.
            return null;
        }
    }
}