using Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySpectrumCodingTest.iOS.Security
{
	public class SecurityConstants
	{
		public static string ServiceId
		{
			get
			{
				return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleDisplayName").ToString();
			}
		}

		public const string DeviceUniqueIdentifierAccountName = "DeviceUniqueIdentifier";
	}
}