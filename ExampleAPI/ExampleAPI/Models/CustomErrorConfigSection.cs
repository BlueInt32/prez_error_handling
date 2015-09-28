using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ExampleAPI.Models
{
	public class ErrorCodeConfigElement : ConfigurationElement
	{
		[ConfigurationProperty("code", IsRequired = true)]
		public string Code
		{
			get
			{
				return this["code"] as string;
			}
		}
		[ConfigurationProperty("httpStatus", IsRequired = true)]
		public string HttpStatus
		{
			get
			{
				return this["httpStatus"] as string;
			}
		}
	}

	public class ErrorCodes : ConfigurationElementCollection
	{
		public ErrorCodeConfigElement this[int index]
		{
			get
			{
				return base.BaseGet(index) as ErrorCodeConfigElement;
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		public new ErrorCodeConfigElement this[string responseString]
		{
			get { return (ErrorCodeConfigElement)BaseGet(responseString); }
			set
			{
				if (BaseGet(responseString) != null)
				{
					BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
				}
				BaseAdd(value);
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new ErrorCodeConfigElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ErrorCodeConfigElement)element).Code;
		}
	}

	public class ErrorCodesConfig : ConfigurationSection
	{
		public static ErrorCodesConfig GetConfig()
		{
			return (ErrorCodesConfig)System.Configuration.ConfigurationManager.GetSection("ApiErrorCodes") ?? new ErrorCodesConfig();
		}

		[ConfigurationProperty("ErrorCodes")]
		[ConfigurationCollection(typeof(ErrorCodes), AddItemName = "ErrorCode")]
		public ErrorCodes ErrorCodes
		{
			get
			{
				object o = this["ErrorCodes"];
				return o as ErrorCodes;
			}
		}
		public static List<ErrorCodeConfigElement> CreateFlatList()
		{
			var config = ErrorCodesConfig.GetConfig();
			var result = new List<ErrorCodeConfigElement>();
			foreach (var item in config.ErrorCodes)
			{
				result.Add((ErrorCodeConfigElement)item);
			}
			return result;
		}
	}

	public class ErrorCodesList
	{
		private static List<ErrorCodeConfigElement> codesList;

		public static List<ErrorCodeConfigElement> CodesList
		{
			get { return codesList ?? (codesList = ErrorCodesConfig.CreateFlatList()); }
		}

		private ErrorCodesList()
		{

		}
	}
}