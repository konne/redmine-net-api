/*
   Copyright 2011 - 2018 Adrian Popescu.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Redmine.Net.Api.Extensions;
using Redmine.Net.Api.Types;

namespace Redmine.Net.Api.Internals
{
    /// <summary>
    /// 
    /// </summary>
    internal static class WebApiAsyncHelper
    {
        /// <summary>
        /// Executes the upload.
        /// </summary>
        /// <param name="redmineManager">The redmine manager.</param>
        /// <param name="address">The address.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <param name="data">The data.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public static async Task<string> ExecuteUpload(RedmineManager redmineManager, string address, string actionType, string data,
            string methodName)
        {
            using (var wc = redmineManager.CreateWebClient(null))
            {
                try
                {
                    if (actionType == HttpVerbs.POST || actionType == HttpVerbs.DELETE || actionType == HttpVerbs.PUT ||
                        actionType == HttpVerbs.PATCH)
                    {
                        return await wc.UploadStringTaskAsync(address, actionType, data).ConfigureAwait(false);
                    }
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(methodName, redmineManager.MimeFormat);
                }
            }

            return null;
        }

        

        /// <summary>
        /// Executes the download.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redmineManager">The redmine manager.</param>
        /// <param name="address">The address.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static async Task<T> ExecuteDownload<T>(RedmineManager redmineManager, string address, string methodName,
            NameValueCollection parameters = null)
            where T : class, new()
        {
            using (var wc = redmineManager.CreateWebClient(parameters))
            {
                try
                {
                    var response = await wc.DownloadStringTaskAsync(address).ConfigureAwait(false);
                    return RedmineSerializer.Deserialize<T>(response, redmineManager.MimeFormat);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(methodName, redmineManager.MimeFormat);
                }
                return default(T);
            }
        }

        /// <summary>
        /// Executes the download list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redmineManager">The redmine manager.</param>
        /// <param name="address">The address.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static async Task<List<T>> ExecuteDownloadList<T>(RedmineManager redmineManager, string address,
           string methodName,
           NameValueCollection parameters = null) where T : class, new()
        {
            using (var wc = redmineManager.CreateWebClient(parameters))
            {
                try
                {
                    var response = await wc.DownloadStringTaskAsync(address).ConfigureAwait(false);
                    var result = RedmineSerializer.DeserializeList<T>(response, redmineManager.MimeFormat);
                    if (result != null)
                        return result.Objects;
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(methodName, redmineManager.MimeFormat);
                }
                return null;
            }
        }


        /// <summary>
        /// Executes the download paginated list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redmineManager">The redmine manager.</param>
        /// <param name="address">The address.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static async Task<PaginatedObjects<T>> ExecuteDownloadPaginatedList<T>(RedmineManager redmineManager, string address,
            string methodName,
            NameValueCollection parameters = null) where T : class, new()
        {
            using (var wc = redmineManager.CreateWebClient(parameters))
            {
                try
                {
                    var response = await wc.DownloadStringTaskAsync(address).ConfigureAwait(false);
                    return RedmineSerializer.DeserializeList<T>(response, redmineManager.MimeFormat);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(methodName, redmineManager.MimeFormat);
                }
                return null;
            }
        }

        /// <summary>
        /// Executes the download file.
        /// </summary>
        /// <param name="redmineManager">The redmine manager.</param>
        /// <param name="address">The address.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public static async Task<byte[]> ExecuteDownloadFile(RedmineManager redmineManager, string address, string methodName)
        {
            using (var wc = redmineManager.CreateWebClient(null, true))
            {
                try
                {
                    return await wc.DownloadDataTaskAsync(address);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(methodName, redmineManager.MimeFormat);
                }
                return null;
            }
        }

        /// <summary>
        /// Executes the upload file.
        /// </summary>
        /// <param name="redmineManager">The redmine manager.</param>
        /// <param name="address">The address.</param>
        /// <param name="data">The data.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        public static async Task<Upload> ExecuteUploadFile(RedmineManager redmineManager, string address, byte[] data, string methodName)
        {
            using (var wc = redmineManager.CreateWebClient(null, true))
            {
                try
                {
                    var response = await wc.UploadDataTaskAsync(address, data);
                    var responseString = Encoding.ASCII.GetString(response);
                    return RedmineSerializer.Deserialize<Upload>(responseString, redmineManager.MimeFormat);
                }
                catch (WebException webException)
                {
                    webException.HandleWebException(methodName, redmineManager.MimeFormat);
                }
                return null;
            }
        }
    }
}