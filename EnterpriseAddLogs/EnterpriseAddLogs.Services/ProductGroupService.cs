﻿namespace EnterpriseAddLogs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using EnterpriseAddLogs.Models;
    using Newtonsoft.Json;

    public class ProductGroupService : Service, IProductGroupService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<ProductGroupEntity>> GetAllProductgroupEntitiesAsync()
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, ServiceConstants.Urls.GetAllProductGroupEntities))
                {
                    AddRequestHeaders(request, ServiceConstants.SubscriptionKeys.PrimaryKey);

                    HttpResponseMessage response = null;

                    using (var cancellationToken = new CancellationTokenSource())
                    {
                        cancellationToken.CancelAfter(TimeSpan.FromSeconds(ServiceConstants.Defaults.ServiceTimeoutInSecs));

                        response = await client.SendAsync(request, cancellationToken.Token).ConfigureAwait(false);
                    }

                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ICollection<ProductGroupEntity>>(json);
                }
            }
        }
    }
}
