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
                var appSettings = new AppSettings
                {
                    Secret = configuration.GetSection("appSettings:Secret").Value
                };
                return new Integration
                {
                    Services = configuration.GetSection("integration:services").GetChildren().Select(s => new ServicesInt
                    {
                        Name = s.GetSection("name").Value,
                        URL = s.GetSection("url").Value,
                        Authentication = new Auth
                        {
                            Token = s.GetSection("authentication:token").Value != null ?
                                s.GetSection("authentication:token").Value : null,
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
                //using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    if (_service.Authentication.Token != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Token", _service.Authentication.Token);
                    if (_service.Authentication.User != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_service.Authentication.User}:{_service.Authentication.Pass}")));
                    request.Headers.Add("Accept", "application/json");
                    //request.Content = stringContent;
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
                //using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    if (_service.Authentication.Token != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Token", _service.Authentication.Token);
                    if (_service.Authentication.User != null)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_service.Authentication.User}:{_service.Authentication.Pass}")));
                    request.Headers.Add("Accept", "application/json");
                    //request.Content = stringContent;
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
        //public static async Task CallMethodNotAuth(string service, string method, string content)
        //{
        //    try
        //    {
        //        var _set = _integration;
        //        var _service = _set.Services.Where(s => s.Name.Equals(service)).ToList().FirstOrDefault();
        //        var _method = _service.Methods.Where(m => m.Method.Equals(method)).FirstOrDefault().Value;
        //        _method = !string.IsNullOrEmpty(_method) ? string.Format($"/{_method}") : null;
        //        string URL = " http://localhost:53707/api/vehiculoPlaca";//string.Format($"{_service.URL}{_method}");
        //        //using (var client = new HttpClient())
        //        //{
        //        //    var vehiculo = new Vehiculo() { Placa = "AAF75A" };
        //        //    var VehiculoResponse = await client.PostAsJsonAsync(URL, vehiculo);
        //        //    if (VehiculoResponse.IsSuccessStatusCode)
        //        //    {
        //        //        var cuerpo = await VehiculoResponse.Content.ReadAsStringAsync();
        //        //    }

        //        //}

        //        using (var client = new HttpClient())
        //        using (var request = new HttpRequestMessage(HttpMethod.Post, URL))
        //        using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
        //        {
        //            //request.Headers.Add("Accept", "application/json");
        //            //request.Content = stringContent;
        //            //using (var response = await client
        //            //   .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
        //            //   .ConfigureAwait(false))


        //            client.BaseAddress = new Uri(URL);
        //            client.DefaultRequestHeaders.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            var responsed =  client.PostAsJsonAsync("api/Department", content);
        //            //if (responsed.IsSuccessStatusCode)
        //            //{
        //            //    responsed.EnsureSuccessStatusCode();
        //            //    var result = await responsed.Content.ReadAsStringAsync();
        //            //    return result;
        //            //}
        //            return "";


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}

        public async static Task<string> CallMethodImage(string Url, string pathfile)
        {
            try
            {
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
                {
                    using (
                        Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(),
                        stream = new FileStream(pathfile, FileMode.Create, FileAccess.Write, FileShare.None, 10000, true))
                    {
                        await contentStream.CopyToAsync(stream);
                    }

                    using (var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                        .ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        var stream = await response.Content.ReadAsStreamAsync();

                        using (var stream2 = new FileStream(pathfile, FileMode.Create, FileAccess.Write, FileShare.None, 10000, true))
                        {
                            await stream2.CopyToAsync(stream);
                        }
                        return "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<string> CallMethodMultipart(string service, string method, string content)
        {
            try
            {
                var _set = _integration;
                var _service = _set.Services.Where(s => s.Name.Equals(service)).ToList().FirstOrDefault();
                string URL = string.Format($"{_service.Name}/{_service.Methods.Where(m => m.Method.Equals(method)).FirstOrDefault().Value}/");

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, URL))
                using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                using (var multipart = new MultipartFormDataContent())
                {
                    multipart.Add(stringContent, "Data");
                    request.Content = multipart;
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
