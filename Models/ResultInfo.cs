using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbill.Central.Agent.Plugins.Cloudflare.Models
{
	public class ResultInfo
	{
		public int count { get; set; }
		public int page { get; set; }
		public int per_page { get; set; }
		public int total_count { get; set; }
	}
}
