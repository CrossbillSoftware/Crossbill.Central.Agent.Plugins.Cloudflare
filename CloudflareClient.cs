using Crossbill.Central.Agent.Plugins.Cloudflare.Models;
using Crossbill.Central.Agent.Plugins.Cloudflare.Processors;
using Crossbill.Common.Processors;
using System;
using System.Linq;
using System.Net;

namespace Crossbill.Central.Agent.Plugins.Cloudflare
{
	public class CloudflareClient
	{
		protected string _apiUrl;
		public string ApiUrl
		{
			get { return _apiUrl; }
			set { _apiUrl = value; }
		}

		protected string _apiToken;

		protected bool _isIgnoreErrors;
		public bool IsIgnoreErrors
		{
			get { return _isIgnoreErrors; }
			set { _isIgnoreErrors = value; }
		}

		public CloudflareClient()
		{
		}

		public CloudflareClient(string apiToken, IWebProxy proxy, bool isIgnoreErrors = false, bool isInitialize = true)
		{
			_apiUrl = "https://api.cloudflare.com/client/v4/";
			_apiToken = apiToken;
			_isIgnoreErrors = isIgnoreErrors;

			if (isInitialize)
			{
				Initialize(proxy);
			}
		}

		public virtual ApiProcessor Initialize(IWebProxy proxy)
		{
			ApiProcessor svc = GetApiProcessor(proxy);
			this.Zones = new ZonesFacade(_apiUrl, _apiToken, _isIgnoreErrors, svc);
			return svc;
		}

		public virtual ApiProcessor GetApiProcessor(IWebProxy proxy)
		{
			ApiProcessor processor = new CloudflareApiProcessor();
			processor.Proxy = proxy;
			return processor;
		}

		public virtual ZonesFacade Zones { get; protected set; }
	}

	public partial class ZonesFacade
	{
		protected string _apiUrl;
		protected string _apiToken;
		protected bool _isIgnoreErrors;
		protected ApiProcessor _svc;
		public ZonesFacade(string apiUrl, string apiToken, bool isIgnoreErrors, ApiProcessor svc)
		{
			_apiUrl = apiUrl;
			_apiToken = apiToken;
			_isIgnoreErrors = isIgnoreErrors;
			_svc = svc;
		}

		public virtual Zone[] List(string domain)
		{
			string url = String.Format("{0}zones?name={1}", _apiUrl, domain);
			ZonesResponse response = _svc.Call<DnsRecord, ZonesResponse>(_apiToken, url, HttpVerb.Get, _isIgnoreErrors, null);
			if (response == null || !response.success)
			{
				if (!_isIgnoreErrors)
				{
					string error = null;
					if (response != null && response.errors != null)
					{
						error = String.Join(' ', response.errors.Select(e => e.message).Where(e => !String.IsNullOrEmpty(e)).ToArray());
					}

					var ex = new Exception(error ?? "Zones list error");
					ex.Data.Add("RemoteUrl", url);
					throw ex;
				}
				return null;
			}
			return response.result;
		}

		public virtual bool AddDnsRecord(string zoneId, DnsRecord records)
		{
			string url = String.Format("{0}zones/{1}/dns_records", _apiUrl, zoneId);
			RecordsPostResponse response = _svc.Call<DnsRecord, RecordsPostResponse>(_apiToken, url, HttpVerb.Post, _isIgnoreErrors, records);
			if (response == null || !response.success)
			{
				if (!_isIgnoreErrors)
				{
					string error = null;
					if (response != null && response.errors != null)
					{
						error = String.Join(' ', response.errors.Select(e => e.message).Where(e => !String.IsNullOrEmpty(e)).ToArray());
					}

					var ex = new Exception(error ?? "AddDnsRecord error");
					ex.Data.Add("RemoteUrl", url);
					throw ex;
				}
				return false;
			}
			return true;
		}
	}
}
