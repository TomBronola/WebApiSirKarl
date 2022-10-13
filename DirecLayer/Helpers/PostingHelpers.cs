using MSXML2;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirecLayer.Helpers
{
    public static class PostingHelpers
    {
        public static ServerXMLHTTP60 ServiceLayer = new ServerXMLHTTP60();
        public static bool LoginAction()
        {
            try
            {                
                //Web.confid appsettings
                string CompanyDB = ConfigurationManager.AppSettings["SLSAPDatabase"];
                string UserName = ConfigurationManager.AppSettings["SLSAPDBUserID"];
                string Password = ConfigurationManager.AppSettings["SLSAPDBPassword"];

                bool result = true;
                var json = new StringBuilder();
                json.AppendLine("{");
                json.AppendLine($@" ""CompanyDB"" : ""{CompanyDB}"",");
                json.AppendLine($@" ""UserName"" : ""{UserName}"",");
                json.AppendLine($@" ""Password"" : ""{Password}""");
                json.AppendLine("}");

                ServiceLayer.open("POST", $@"{ServiceURL()}Login");
                ServiceLayer.setOption(SERVERXMLHTTP_OPTION.SXH_OPTION_IGNORE_SERVER_SSL_CERT_ERROR_FLAGS, 13056);
                try
                {
                    ServiceLayer.send(json.ToString());
                }
                catch (Exception ex)
                {
                    result = true;
                }

                string ret = GetJsonValue(ServiceLayer.responseText, "SessionId");
                if (string.IsNullOrEmpty(ret))
                {
                    result = false;
                }
                else
                {
                    result = ret.Contains("-");
                }
                return result;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static string ServiceURL()
        {

            //var url = $"{settings.SLURL}://{settings.SLServerIPAddress}:{settings.SLPort}/b1s/v1/";
            var url = $"https://192.168.2.32:30030/b1s/v1/";
            const string httpStr = "http://";
            const string httpsStr = "https://";
            if (!url.StartsWith(httpStr, true, null) &&
                !url.StartsWith(httpsStr, true, null))
            {
                url = httpStr + url;
            }

            if (ServiceLayer == null)
            { ServiceLayer = new ServerXMLHTTP60(); }

            return url;
        }

        public static string GetJsonValue(string json, string value)
        {
            try
            {
                if (json != null)
                {
                    JObject err = JObject.Parse(json);
                    if (err.ToString().Contains("error"))
                    {
                        return $"error : {GetJsonError(err.ToString())}";
                    }
                    else
                    {
                        return (string)err[value];
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                if (json.Contains("error"))
                {
                    string retJson = GetJsonString(json, "");
                    var sbJson = new StringBuilder();
                    sbJson.Append("{" + retJson + "}}}");
                    return GetJsonError(sbJson.ToString());
                }
                else { return "Operation completed successfully"; }
            }
        }


        public static string GetJsonError(string json)
        {
            JObject err = JObject.Parse(json);
            return (string)err["error"]["message"]["value"];
        }

        public static string GetJsonString(string ret, string tag)
        {
            var startTag = "{";
            int startIndex = ret.IndexOf(startTag) + startTag.Length;
            int endIndex = ret.IndexOf("}", startIndex);
            return ret.Substring(startIndex, endIndex - startIndex);
        }

        public static string SBOResponse(string sMethod, string sModule, string sJson, string sRetValue)
        {
            //var output = true;
            string output = "";
            string url = ServiceURL();
            ServiceLayer.open(sMethod, $"{url}{sModule}");
            ServiceLayer.setOption(SERVERXMLHTTP_OPTION.SXH_OPTION_IGNORE_SERVER_SSL_CERT_ERROR_FLAGS, 13056);
            ServiceLayer.setTimeouts(1000000, 1000000, 1000000, 1000000);
            if (sModule.Contains("Attachments2"))
            {
                ServiceLayer.setRequestHeader("Content-Type", "multipart/form-data");
            }
            if (sMethod == "PATCH")
            {
                ServiceLayer.setRequestHeader("B1S-ReplaceCollectionsOnPatch", "true");
            }
            try
            {
                ServiceLayer.send(sJson);

                var response = ServiceLayer.responseText;

                //Will Return the response text
                output = sMethod == "GET" ? response : GetJsonValue(response, sRetValue);

                //If post only the return value will be received or docentry
                //output = sMethod == "POST" ? response : GetJsonValue(response, sRetValue);

            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            return output.ToString();
        }
    }
}
