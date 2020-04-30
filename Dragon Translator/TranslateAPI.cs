using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Dragon_Translator
{
    class TranslateAPI
    {
        public string Get_TK()
        {
            string url = String.Format
            ("https://translate.google.com/m/translate");
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;
            return Regex.Match(result, @"tkk:'(.*?)',").Groups[1].Value;

        }
        public string[] TranslateText2_0(string input, string languageout, string languagein = "auto")
        {
            string url = String.Format
            ("https://translate.google.com/translate_a/single?client=gtx&sl={0}&tl={1}&hl={1}&dt=bd&dt=qc&dt=ld&dt=rm&dt=t&tk=" + Get_TK() + "&q={2}&dj=1&oe=UTF-8&ie=UTF-8",
             languagein, languageout, Uri.EscapeUriString(input));
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;
            JObject kq = JObject.Parse(result);
            string nghia = (string)kq["sentences"][0]["trans"];
            string phienam = "/" + (string)kq["sentences"][1]["src_translit"] + "/";
            string langcuatu = (string)kq["src"];
            string dangtu = (string)kq["dict"][0]["pos"];
            string nghiacuadang = "";
            foreach (string item in (kq["dict"][0]["terms"]))
            {
                nghiacuadang += item + ",";
            }
            string[] rv = { nghia, phienam, dangtu, nghiacuadang , langcuatu };
            return rv;
        }
        string TranslateText(string input, string languageout, string languagein = "auto")
        {
            // Set the language from/to in the url (or pass it into this function)
            string url = String.Format
            ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
             languagein, languageout, Uri.EscapeUriString(input));
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;

            // Get all json data
            var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(result);

            // Extract just the first array element (This is the only data we are interested in)
            var translationItems = jsonData[0];

            // Translation Data
            string translation = "";

            // Loop through the collection extracting the translated objects
            try
            {
                foreach (object item in translationItems)
                {
                    // Convert the item array to IEnumerable
                    IEnumerable translationLineObject = item as IEnumerable;

                    // Convert the IEnumerable translationLineObject to a IEnumerator
                    IEnumerator translationLineString = translationLineObject.GetEnumerator();

                    // Get first object in IEnumerator
                    translationLineString.MoveNext();

                    // Save its value (translated text)
                    translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
                }

                // Remove first blank character
                if (translation.Length > 1) { translation = translation.Substring(1); };
                return translation;
            }
            catch
            {
                return "";
            }
            // Return translation

        }

    }
}
