using Impodatos.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Impodatos.Services.Common.Security
{
    public class RequestHttp
    {
        private static Integration _integration
        {
            get
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
                    .Build();
           
                return new Integration
                {
                    Services = configuration.GetSection("integration:services").GetChildren().Select(s => new ServicesInt
                    {
                        Name = s.GetSection("name").Value,
                        URL = s.GetSection("url").Value,
                        Authentication = new Auth
                        {                         
                            User = s.GetSection("authentication:user").Value != null ?
                                s.GetSection("authentication:user").Value : null,
                            Pass = s.GetSection("authentication:pass").Value != null ?
                             s.GetSection("authentication:pass").Value : null
                        },
                        Methods = s.GetSection("methods").GetChildren().Select(m => new Methods
                        {
                            Method = m.GetSection("method").Value,
                            Value = m.GetSection("value").Value
                        }).ToList()
                    }).ToList()
                };
            }
        }

        public async static Task<string> CallMethod(string service, string method, string content)
        {
            try
            {
                var _set = _integration;
                var _service = _set.Services.Where(s => s.Name.Equals(service)).ToList().FirstOrDefault();
                var _method = _service.Methods.Where(m => m.Method.Equals(method)).FirstOrDefault().Value;
                _method = !string.IsNullOrEmpty(_method) ? string.Format($"/{_method}") : null;
                string URL = string.Format($"{_service.URL}{_method}");

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, URL))
                using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    if (_service.Authentication.Token != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Token", _service.Authentication.Token);
                    if (_service.Authentication.User != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_service.Authentication.User}:{_service.Authentication.Pass}")));
                    request.Headers.Add("Accept", "application/json");
                    request.Content = stringContent;
                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<string> CallMethod(string service, string method)
        {
            try
            {
                var _set = _integration;
                var _service = _set.Services.Where(s => s.Name.Equals(service)).ToList().FirstOrDefault();
                var _method = _service.Methods.Where(m => m.Method.Equals(method)).FirstOrDefault().Value;
                _method = !string.IsNullOrEmpty(_method) ? string.Format($"/{_method}") : null;
                string URL = string.Format($"{_service.URL}{_method}");

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, URL))
                {
                    if (_service.Authentication.Token != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Token", _service.Authentication.Token);
                    if (_service.Authentication.User != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_service.Authentication.User}:{_service.Authentication.Pass}")));
                    request.Headers.Add("Accept", "application/json"); 
                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async static Task<string> CallGetMethod(string service, string method, string content)
        {
            try
            {
                var _set = _integration;
                var _service = _set.Services.Where(s => s.Name.Equals(service)).ToList().FirstOrDefault();
                var _method = _service.Methods.Where(m => m.Method.Equals(method)).FirstOrDefault().Value;
                _method = !string.IsNullOrEmpty(_method) ? string.Format($"/{_method}") : null;
                string URL = string.Format($"{_service.URL}{_method}{content}");

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, URL))
                {
                    if (_service.Authentication.Token != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Token", _service.Authentication.Token);
                    if (_service.Authentication.User != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_service.Authentication.User}:{_service.Authentication.Pass}")));
                    request.Headers.Add("Accept", "application/json");
                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
