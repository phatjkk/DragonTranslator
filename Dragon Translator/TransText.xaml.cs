using IniParser;
using IniParser.Model;
using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
    /// Interaction logic for TransText.xaml
    /// </summary>
    public partial class TransText : Window
    {
        public string org_tam;
        public string minilangpb;
        public string minilangnew;
        public string[] arrkq;
        IniData data;
        FileIniDataParser parser = new FileIniDataParser();
        public TransText(string langin, string langout, string orgtext, string minilangin_)
        {
            InitializeComponent();
            this.Activate();
            this.Topmost = true;
            minilangpb = minilangin_;
            org_tam = orgtext;
            org_text.Text = orgtext;
            new_text.Text = "Loading...";
            lang_in.Content = langin;
            lang_out.Content = langout;
            arrkq = TranslateText(orgtext, minilangin_);
            minilangnew = arrkq[1];
            new_text.Text = arrkq[0];
            data = parser.ReadFile("data//settings.ini");
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Thread_SongSong);
            dispatcherTimer.Start();
        }
        private void Thread_SongSong(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (org_tam != org_text.Text)
                {
                    org_tam = org_text.Text;
                    arrkq = TranslateText(org_tam, minilangpb);
                    minilangnew = arrkq[1];
                    new_text.Text = arrkq[0];
                }
            });
            
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
        string[] TranslateText(string input, string languageout, string languagein = "auto")
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
                string[] arr ={translation, jsonData[2] };
                return arr;
            }
            catch
            {
                string[] errarr = { "","" };
                return errarr;
            }
            // Return translation

        }


        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PlayMp3FromUrl("https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q=" + Uri.EscapeUriString(org_text.Text) + "&tl="+ minilangnew + "&total=1&idx=0");
        }

        private void PackIcon_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            PlayMp3FromUrl("https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q=" + Uri.EscapeUriString(new_text.Text) + "&tl=" + minilangpb + "&total=1&idx=0");
        }

        private void PackIcon_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(new_text.Text);
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
