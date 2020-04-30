using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dragon_Translator
{
    /// <summary>
    /// Interaction logic for TransWord.xaml
    /// </summary>
    public partial class TransWord : Window
    {
        public string minilangpb;
        public string minilangnew;
        public string[] arrkq;
        public TransWord(string langin, string langout, string orgtext,string minilangin_,string tk)
        {
            InitializeComponent();
            this.Activate();
            this.Topmost = true;
            org_text.Text = orgtext;
            new_text.Text = "Loading...";
            lang_in.Content = langin;
            minilangpb = minilangin_;
            lang_out.Content = langout;
            arrkq= TranslateText2_0(orgtext, minilangin_,"auto",tk);
            new_text.Text = arrkq[0];
            _phienam.Content= arrkq[1];
            _dangtu.Content = arrkq[2];
            _nghiadang.Text = arrkq[3];
            minilangnew = arrkq[4];
            if (arrkq[2]!="")
            {
                this.Height=175.631;
            }

        }

        public string[] TranslateText2_0(string input, string languageout, string languagein,string tk)
        {
            try
            {
                string url = String.Format
                ("https://translate.google.com/translate_a/single?client=gtx&sl={0}&tl={1}&hl={1}&dt=bd&dt=qc&dt=ld&dt=rm&dt=t&tk=" + tk+ "&q={2}&dj=1&oe=UTF-8&ie=UTF-8",
                 languagein, languageout, Uri.EscapeUriString(input));
                HttpClient httpClient = new HttpClient();
                string result = httpClient.GetStringAsync(url).Result;
                JObject kq = JObject.Parse(result);
                string nghia = (string)kq["sentences"][0]["trans"];
                string phienam, dangtu, langcuatu, nghiacuadang;
                try
                {
                    phienam = "/" + (string)kq["sentences"][1]["src_translit"] + "/";
                }
                catch
                {
                    phienam = "";
                }
                try
                {
                    dangtu = (string)kq["dict"][0]["pos"];
                }
                catch
                {
                    dangtu = "";
                }
                try
                {
                    langcuatu = (string)kq["src"];
                }
                catch
                {
                    langcuatu = "";
                }
                try
                {
                    nghiacuadang = "";
                    foreach (string item in (kq["dict"][0]["terms"]))
                    {
                        nghiacuadang += item + ", ";
                    }
                }
                catch
                {
                    nghiacuadang = "";
                }
                string[] rv = { nghia, phienam, dangtu, nghiacuadang, langcuatu };
                return rv;
            }
            catch
            {
                string[] rv = { "", "", "", "", "" };
                return rv;
            }
        }
        public static void PlayMp3FromUrl(string url)
        {
            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(url)
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                ms.Position = 0;
                using (WaveStream blockAlignedStream =
                    new BlockAlignReductionStream(
                        WaveFormatConversionStream.CreatePcmStream(
                            new Mp3FileReader(ms))))
                {
                    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        public string[] TranslateText(string input, string languageout, string languagein = "auto")
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
                string[] arr = { translation, jsonData[2] };
                return arr;
            }
            catch
            {
                string[] errarr = { "", "" };
                return errarr;
            }
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PlayMp3FromUrl("https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q=" + Uri.EscapeUriString(org_text.Text) + "&tl="+ minilangnew + "&total=1&idx=0");
        }

        private void PackIcon_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            PlayMp3FromUrl("https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q=" + Uri.EscapeUriString(new_text.Text) + "&tl=" + minilangpb + "&total=1&idx=0");
        }
    }
}
