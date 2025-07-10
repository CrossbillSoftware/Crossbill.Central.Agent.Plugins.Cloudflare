using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbill.Central.Agent.Plugins.Cloudflare.Models
{
	public class DnsRecord
	{
		public string content { get; set; }
		public string name { get; set; }
		public bool proxied { get; set; }
		public string type { get; set; }
		public string comment { get; set; }
		public string[] tags { get; set; }
		public int ttl { get; set; }
	}
}
