using Crossbill.Common.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Crossbill.Central.Agent.Plugins.Cloudflare.Processors
{
	public class CloudflareApiProcessor : ApiProcessor
	{
		protected override HttpClient GetClient(string token, HttpClientHandler handler)
		{
			var client = base.GetClient(null, handler);

			if (!String.IsNullOrEmpty(token))
			{
				client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", String.Format("Bearer {0}", token));
			}

			return client;
		}
	}
}
