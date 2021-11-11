using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace TAD
{

    /// <summary>
    /// برای تایید کپچای گوگل
    /// </summary>
    public class CaptchaManager
    {

        //Function for VerifyCaptcha
        public static ReCaptchaResponse VerifyCaptcha(string secret, string response)
        {
            //if (request != null)
            //{
            using (HttpClient hc = new System.Net.Http.HttpClient())
            {
                var values = new Dictionary<string, string>
                    {
                        {
                            "secret",
                            secret
                        },
                        {
                            "response",
                            response
                        }
                    };
                var content = new FormUrlEncodedContent(values);
                var Response = hc.PostAsync("https://www.google.com/recaptcha/api/siteverify", content).Result;
                var responseString = Response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrWhiteSpace(responseString))
                {
                    ReCaptchaResponse CaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(responseString);
                    hc.Dispose();
                    return CaptchaResponse;
                }
                else
                {
                    //Throw error as required
                    hc.Dispose();
                    return null;
                }
            }
            //else
            //{
            ////Throw error as required 
            //    return null;
            //} 
        }

    }



    public class ReCaptchaResponse
    {
        public bool success
        {
            get;
            set;
        }
        public string challenge_ts
        {
            get;
            set;
        }
        public string hostname
        {
            get;
            set;
        }
        [JsonProperty(PropertyName = "error-codes")]
        public List<string> error_codes
        {
            get;
            set;
        }
    }

}