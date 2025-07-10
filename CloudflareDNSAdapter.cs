using Crossbill.Central.Agent.Plugins.Cloudflare.Models;
using Crossbill.Install.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Crossbill.Central.Agent.Plugins.Cloudflare
{
	[Export(typeof(Crossbill.Install.Lib.IBaseAdapter))]
	public class CloudflareDNSAdapter : Crossbill.Install.Lib.BaseAdapter
	{
		public override string GetTargetAppType()
		{
			return "CloudflareDNS";
		}

		public override List<string> Install(ContextSettings settings)
		{
			string apiToken, domain, aRecords, ip;
			if (!settings.Parameters.TryGetValue("ApiToken", out apiToken) || String.IsNullOrEmpty(apiToken))
			{
				throw new Exception("ApiToken parameter is required for CloudflareDNS adapter.");
			}
			if (!settings.Parameters.TryGetValue("Domain", out domain) || String.IsNullOrEmpty(domain))
			{
				throw new Exception("Domain parameter is required for CloudflareDNS adapter.");
			}
			if (!settings.Parameters.TryGetValue("ARecords", out aRecords) || String.IsNullOrEmpty(aRecords))
			{
				throw new Exception("ARecords parameter is required for CloudflareDNS adapter.");
			}
			if (!settings.Parameters.TryGetValue("IP", out ip) || String.IsNullOrEmpty(ip))
			{
				throw new Exception("IP parameter is required for CloudflareDNS adapter.");
			}


			CloudflareClient client = new CloudflareClient(apiToken, null);

			var zones = client.Zones.List(domain);
			if (zones == null || zones.Length == 0)
			{
				throw new Exception(String.Format("Domain {0} not found in Cloudflare", domain));
			}

			string zoneId = zones[0].id;

			var records = new List<DnsRecord>();
			string[] recs = aRecords.Split(',', ';');
			foreach (string rec in recs)
			{
				records.Add(new DnsRecord() { 
					name = rec,
					content = ip, 
					proxied = false,
					ttl = 600,
					type = "A"
				});
			}
			foreach (var record in records) 
			{
				client.Zones.AddDnsRecord(zoneId, record);
			}

			return null;
		}

		public override void Uninstall(ContextSettings settings, List<string> files)
		{
		}
	}
}
