
using IniParser;
using IniParser.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace Dragon_Translator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] langarr = { "English", "Vietnamese", "Chinese - Simplified", "Spanish", "French", "Russian", "Japanese" };
        string[] langarrmini = { "en", "vi", "zh-CN", "es", "fr", "ru", "ja" };
        public static DateTime startdate;
        public static int Clicks = 0;
        public static int isrun = 1;
        public int x;
        public int y;
        Thread RunQuickTrans;
        public Thread thread5;
        public Thread thread6;
        public Thread thread7;
        FileIniDataParser parser = new FileIniDataParser();
        IniData data;
        string currentlang = "";
        string currentocrlang = "en";
        public static TransWord UI;
        public static TransText UI_LongText;
        public static Loading UI_Loading;
        string TK = "";
        
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
        public MainWindow()
        {
            InitializeComponent();
            data = parser.ReadFile(Directory.GetCurrentDirectory() + "\\data\\settings.ini");
            int indexarr = Array.IndexOf(langarrmini, data["option"]["lang"]);
            currentlang = langarr[indexarr];
            list_lang.SelectedValue = currentlang;
            currentocrlang = data["option"]["langocr"];
            _hotkey.Text = data["option"]["quicktranshotkey"];
            TK = Get_TK();
            if (data["option"]["runwhenwinstart"]=="0")
            {
                _runws.IsChecked = false;
            }
            else
            {
                _runws.IsChecked = true;
            }
            RunQuickTrans = new Thread(QuickTranslate);
            RunQuickTrans.SetApartmentState(ApartmentState.STA);
            RunQuickTrans.Start();
        }
        public string Get_TK()
        {
            string url = String.Format
            ("https://translate.google.com/m/translate");
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;
            return Regex.Match(result, @"tkk:'(.*?)',").Groups[1].Value;

        }
        private void Closeclick(object sender, MouseButtonEventArgs e)
        {
            //mouseInputHook.setHook(false);
            RunQuickTrans.Abort();
            System.Windows.Application.Current.Shutdown();
        }
        private void Minimizeclick(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Hide();
            var bmp = SnippingTool.Snip();
            WindowState = WindowState.Normal;
            int indexarr = Array.IndexOf(langarrmini, currentocrlang);

            if (bmp != null)
            {
                thread7 = new Thread(() => OpenLoadingBox());
                thread7.SetApartmentState(ApartmentState.STA);
                thread7.IsBackground = true;
                thread7.Start();
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg"))
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg");
                //System.Windows.Forms.MessageBox.Show("-i " + Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg" + " -l " + langarr[indexarr] + " -o data\\kq.txt");
                bmp.Save(Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg", ImageFormat.Jpeg);
                bmp.Dispose();
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "data\\Capture2Text\\Capture2Text_CLI.exe",
                        Arguments = "-i data\\Capture2Text\\kq.jpg" + " -l " + @""""+langarr[indexarr]+ @"""" +" -o data\\kq.txt",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                proc.WaitForExit();
                string kqorc= File.ReadAllText("data\\kq.txt", Encoding.UTF8);
                thread6 = new Thread(() => OpenTextBox(kqorc, "Auto", currentlang, data["option"]["lang"]));
                thread6.IsBackground = true;
                App.Current.MainWindow.Show();
                thread6.Start();
                CloseLoadingBox(UI_Loading);
            }
        }
            public void AllScreenOCR()
        {
            Bitmap captureBitmap;
            try

            {
                captureBitmap = new Bitmap(1024, 768, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                int indexarr = Array.IndexOf(langarrmini, currentocrlang);
                thread7 = new Thread(() => OpenLoadingBox());
                thread7.SetApartmentState(ApartmentState.STA);
                thread7.IsBackground = true;
                thread7.Start();
                //System.Windows.Forms.MessageBox.Show("-i " + Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg" + " -l " + langarr[indexarr] + " -o data\\kq.txt");
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg"))
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg");
                //System.Windows.Forms.MessageBox.Show("-i " + Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg" + " -l " + langarr[indexarr] + " -o data\\kq.txt");
                captureBitmap.Save(Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg", ImageFormat.Jpeg);
                captureBitmap.Dispose();
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "data\\Capture2Text\\Capture2Text_CLI.exe",
                        Arguments = "-i data\\Capture2Text\\kq.jpg" + " -l " + @"""" + langarr[indexarr] + @"""" + " -o data\\kq.txt",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                proc.WaitForExit();
                string kqorc = File.ReadAllText("data\\kq.txt", Encoding.UTF8);
                thread6 = new Thread(() => OpenTextBox(kqorc, "Auto", currentlang, data["option"]["lang"]));
                thread6.IsBackground = true;
                thread6.Start();
                CloseLoadingBox(UI_Loading);

            }

            catch (Exception ex)

            {

                System.Windows.Forms.MessageBox.Show(ex.Message);

            }
        }
        public void EndGame()
        {
            var bmp = SnippingTool.Snip();
            int indexarr = Array.IndexOf(langarrmini, currentocrlang);

            if (bmp != null)
            {
                thread7 = new Thread(() => OpenLoadingBox());
                thread7.SetApartmentState(ApartmentState.STA);
                thread7.IsBackground = true;
                thread7.Start();
                //System.Windows.Forms.MessageBox.Show("-i " + Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg" + " -l " + langarr[indexarr] + " -o data\\kq.txt");
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg"))
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg");
                //System.Windows.Forms.MessageBox.Show("-i " + Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg" + " -l " + langarr[indexarr] + " -o data\\kq.txt");
                bmp.Save(Directory.GetCurrentDirectory() + "\\data\\Capture2Text\\kq.jpg", ImageFormat.Jpeg);
                bmp.Dispose();
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "data\\Capture2Text\\Capture2Text_CLI.exe",
                        Arguments = "-i data\\Capture2Text\\kq.jpg" + " -l " + @"""" + langarr[indexarr] + @"""" + " -o data\\kq.txt",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                proc.WaitForExit();
                string kqorc = File.ReadAllText("data\\kq.txt", Encoding.UTF8);
                thread6 = new Thread(() => OpenTextBox(kqorc, "Auto", currentlang, data["option"]["lang"]));
                thread6.IsBackground = true;
                thread6.Start();
                CloseLoadingBox(UI_Loading);
            }
        }
        public bool _CheckMouseLeftClick()
        {
            if ((GetAsyncKeyState(1) & 0x8000) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool _CheckKeyPressed(int i)
        {
            if ((GetAsyncKeyState(i) & 0x8000) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void QuickTranslate()
        {
            int checkopentextbox = 0;
            while (true) {

                Dispatcher.Invoke(() => {
                    int indexarr = Array.IndexOf(langarr, list_lang.SelectedValue.ToString());
                    currentlang = list_lang.SelectedValue.ToString();
                    data["option"]["lang"] = langarrmini[indexarr];
                    data["option"]["quicktranshotkey"] = _hotkey.Text;
                });
                if (_CheckKeyPressed(115) == true)
                {
                    EndGame();
                }
                if (_CheckKeyPressed(120) == true)
                {
                    AllScreenOCR();
                }
                if (_CheckKeyPressed(113) == true)
                {
                    if (checkopentextbox != 1)
                    {
                        if (UI_LongText != null)
                        {
                            CloseTextBox(UI_LongText);
                        }
                        if (thread6 != null)
                        {
                            thread6.Abort();
                        }
                        SendKeys.SendWait("^(c)");
                        string orgtext = System.Windows.Clipboard.GetText();
                        thread6 = new Thread(() => OpenTextBox(orgtext, "Auto", currentlang, data["option"]["lang"]));
                        thread6.SetApartmentState(ApartmentState.STA);
                        thread6.IsBackground = true;
                        thread6.Start();
                       
                        checkopentextbox = 1;
                    }
                    
                }
                if (_CheckMouseLeftClick() == true)
                {
                    startdate = DateTime.Now;
                    Clicks += 1;
                    if (Clicks >= 2)
                    {
                        Clicks = 0;
                        x = System.Windows.Forms.Cursor.Position.X;
                        y = System.Windows.Forms.Cursor.Position.Y;
                        if (isrun == 0)
                        {
                            SendKeys.SendWait("^(c)");
                            string orgword = System.Windows.Clipboard.GetText();
                            try
                            {
                                System.Windows.Clipboard.SetText("");
                            }
                            catch { }
                            
                            if (UI != null)
                            {
                                CloseWordBox(UI);
                            }
                            if (thread5 != null)
                            {
                                thread5.Abort();
                            }
                            if(orgword=="" || (orgword.Length > 19))
                            {

                            }
                            else
                            {

                                thread5 = new Thread(() => OpenWordBox(orgword, "Auto", currentlang, data["option"]["lang"]));
                                thread5.IsBackground = true;
                                thread5.Start();
                            }
                            
                        }

                    }
                    while (_CheckMouseLeftClick())
                    {
                        Thread.Sleep(10);
                    }
                }
                if (Clicks != 0 && (DateTime.Now - startdate).TotalSeconds > 0.5f)
                {
                    Clicks = 0;
                    int xscr= Screen.PrimaryScreen.Bounds.Width;
                    int yscr = Screen.PrimaryScreen.Bounds.Height;
                    int x1 = (xscr/2)- (609/2);
                    int y1 = (yscr / 2) - (244 / 2);
                    if (UI_LongText != null)
                    {
                        if ((System.Windows.Forms.Cursor.Position.X - x1) < 609)
                        {
                            if ((System.Windows.Forms.Cursor.Position.Y - y1) < (244))
                            {

                            }
                            else
                            {
                                UI_LongText.Dispatcher.Invoke(() =>
                                {
                                    CloseTextBox(UI_LongText);
                                    checkopentextbox = 0;
                                });
                            }
                        }
                        else
                        {
                            UI_LongText.Dispatcher.Invoke(() =>
                            {
                                CloseTextBox(UI_LongText);
                                checkopentextbox = 0;
                            });
                        }
                    }
                    if (UI != null)
                    {
                        //this.Dispatcher.Invoke(() =>
                        //{
                        //    UI = new TransWord(langin, langout, orgtext, minilangin);
                        //    UI.Left = System.Windows.Forms.Cursor.Position.X;
                        //    UI.Top = System.Windows.Forms.Cursor.Position.Y;
                        //    UI.Show();
                        //});
                        //x = System.Windows.Forms.Cursor.Position.X;
                        //y = System.Windows.Forms.Cursor.Position.Y;
                        if ((System.Windows.Forms.Cursor.Position.X - x) < (x + 227 - x))
                        {
                            if ((System.Windows.Forms.Cursor.Position.Y - y) < (y + 135 - y))
                            {

                            }
                            else
                            {
                                CloseWordBox(UI);
                            }
                        }
                        else
                        {
                            CloseWordBox(UI);
                        }
                        //if ((System.Windows.Forms.Cursor.Position.X-x)<(x+227-x))
                        //{
                        //    if ((System.Windows.Forms.Cursor.Position.Y - y) < (y + 135 - y))
                        //    {

                        //    }
                        //    else { CloseWordBox(UI);
                        //        //UI_LongText.Dispatcher.Invoke(() =>
                        //        //{
                        //            CloseTextBox(UI_LongText);
                        //        //});
                        //        checkopentextbox = 0;
                        //    }
                        //}
                        //else { CloseWordBox(UI);

                        //        CloseTextBox(UI_LongText);
                        //        checkopentextbox = 0;

                        //}

                    }
                }


            }
        }

       
        public partial class SnippingTool : Form
        {
            public static System.Drawing.Image Snip()
            {
                var rc = Screen.PrimaryScreen.Bounds;
                using (Bitmap bmp = new Bitmap(rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
                {
                    using (Graphics gr = Graphics.FromImage(bmp))
                        gr.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                    using (var snipper = new SnippingTool(bmp))
                    {
                        if (snipper.ShowDialog() == DialogResult.OK)
                        {
                            return snipper.Image;
                        }
                    }
                    return null;
                }
            }

            public SnippingTool(System.Drawing.Image screenShot)
            {
                this.BackgroundImage = screenShot;
                this.ShowInTaskbar = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.DoubleBuffered = true;
            }
            public System.Drawing.Image Image { get; set; }

            private System.Drawing.Rectangle rcSelect = new System.Drawing.Rectangle();
            private System.Drawing.Point pntStart;

            protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
            {
                // Start the snip on mouse down
                if (e.Button != MouseButtons.Left) return;
                pntStart = e.Location;
                rcSelect = new System.Drawing.Rectangle(e.Location, new System.Drawing.Size(0, 0));
                this.Invalidate();
            }
            protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
            {
                // Modify the selection on mouse move
                if (e.Button != MouseButtons.Left) return;
               
                int x1 = Math.Min(e.X, pntStart.X);
                int y1 = Math.Min(e.Y, pntStart.Y);
                int x2 = Math.Max(e.X, pntStart.X);
                int y2 = Math.Max(e.Y, pntStart.Y);
                rcSelect = new System.Drawing.Rectangle(x1, y1, x2 - x1, y2 - y1);
                this.Invalidate();
            }
            protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
            {
                // Complete the snip on mouse-up
                if (rcSelect.Width <= 0 || rcSelect.Height <= 0) return;
                Image = new Bitmap(rcSelect.Width, rcSelect.Height);
                using (Graphics gr = Graphics.FromImage(Image))
                {
                    gr.DrawImage(this.BackgroundImage, new System.Drawing.Rectangle(0, 0, Image.Width, Image.Height),
                        rcSelect, GraphicsUnit.Pixel);
                }
                DialogResult = DialogResult.OK;
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                // Draw the current selection
                using (System.Drawing.Brush br = new SolidBrush(System.Drawing.Color.FromArgb(120, System.Drawing.Color.White)))
                {
                    int x1 = rcSelect.X; int x2 = rcSelect.X + rcSelect.Width;
                    int y1 = rcSelect.Y; int y2 = rcSelect.Y + rcSelect.Height;
                    e.Graphics.FillRectangle(br, new System.Drawing.Rectangle(0, 0, x1, this.Height));
                    e.Graphics.FillRectangle(br, new System.Drawing.Rectangle(x2, 0, this.Width - x2, this.Height));
                    e.Graphics.FillRectangle(br, new System.Drawing.Rectangle(x1, 0, x2 - x1, y1));
                    e.Graphics.FillRectangle(br, new System.Drawing.Rectangle(x1, y2, x2 - x1, this.Height - y2));
                }
                using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 3))
                {
                    e.Graphics.DrawRectangle(pen, rcSelect);
                }
            }
            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                // Allow canceling the snip with the Escape key
                if (keyData == Keys.Escape) this.DialogResult = DialogResult.Cancel;
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (btn_start.Content.ToString() == "Start")
            {
                isrun = 0;
                btn_start.Content = "Stop";
                btn_start.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE74C3C"));
            }
            else
            {
                isrun = 1;
                btn_start.Content = "Start";
                btn_start.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF673AB7"));
            }
            int indexarr = Array.IndexOf(langarr, list_lang.SelectedValue.ToString());
            currentlang = list_lang.SelectedValue.ToString();
            data["option"]["lang"] = langarrmini[indexarr];
            data["option"]["quicktranshotkey"] = _hotkey.Text;

            if (_runws.IsChecked == true)
            {
                data["option"]["runwhenwinstart"] = "1";
            }
            else
            {
                data["option"]["runwhenwinstart"] = "0";
            }
            parser.WriteFile("data\\settings.ini", data);
        }
        private void OpenTextBox(string orgtext, string langin, string langout, string minilangin)
        {
            this.Dispatcher.Invoke(() =>
            {
                UI_LongText = new TransText(langin, langout, orgtext, minilangin);
                UI_LongText.Show();
            });
        }
        private void CloseTextBox(TransText UI_LongText)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (UI_LongText != null) { }
                UI_LongText.Close();
            });
        }
        private void OpenWordBox(string orgtext, string langin, string langout,string minilangin)
        {
            this.Dispatcher.Invoke(() =>
            {
                UI = new TransWord(langin, langout, orgtext, minilangin,TK);
                UI.Left = System.Windows.Forms.Cursor.Position.X;
                UI.Top = System.Windows.Forms.Cursor.Position.Y;
                UI.Show();
            });
        }
        private void OpeninfoBox()
        {
            this.Dispatcher.Invoke(() =>
            {
                Test UI = new Test();
                UI.Show();
            });
        }
        private void CloseWordBox(TransWord UI)
        {
            this.Dispatcher.Invoke(() =>
            {
                UI.Close();
            });
        }
        private void OpenLoadingBox()
        {
            //this.Dispatcher.Invoke(() =>{
                UI_Loading = new Loading();
                UI_Loading.Show();
                UI_Loading.Closed += (sender1, e1) => UI_Loading.Dispatcher.InvokeShutdown();
                System.Windows.Threading.Dispatcher.Run();

            //});
        }
        private void CloseLoadingBox(Loading UI_Loading)
        {
            UI_Loading.Dispatcher.Invoke(() =>{
                UI_Loading.Close();
            });
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            thread6 = new Thread(() => OpenTextBox("", "Auto", currentlang, data["option"]["lang"]));
            thread6.IsBackground = true;
            App.Current.MainWindow.Show();
            thread6.Start();
        }

        private void _SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            data = parser.ReadFile("data\\settings.ini");
            if (_ocr_lang.SelectedValue.ToString().Split(new string[] { ": " }, StringSplitOptions.None)[1].Contains("Chinese"))
            {
                int indexarr = Array.IndexOf(langarr, "Chinese - Simplified");
                currentocrlang = langarrmini[indexarr];
                Console.WriteLine(currentocrlang);
                data["option"]["langocr"] = currentocrlang;
            }
            else
            {
                int indexarr = Array.IndexOf(langarr, _ocr_lang.SelectedValue.ToString().Split(new string[] { ": " }, StringSplitOptions.None)[1]);
                currentocrlang = langarrmini[indexarr];
                data["option"]["langocr"] = currentocrlang;
            }
            data["option"]["ocrhotkey"] = ocr_hotkey.Text;
            parser.WriteFile("data\\settings.ini", data);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            data = parser.ReadFile("data\\settings.ini");
            int indexarr = Array.IndexOf(langarrmini, data["option"]["langocr"]);
            Console.WriteLine(indexarr);
            //_ocr_lang.SelectedValue = "System.Windows.Controls.ComboBoxItem: " + langarr[indexarr];
            _ocr_lang.SelectedIndex = indexarr;
            ocr_hotkey.Text = data["option"]["ocrhotkey"];
            parser.WriteFile("data\\settings.ini", data);
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {


        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            OpeninfoBox();
        }
        private void OpenTransFileBox()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Title = "Support *.doc, *.docx, *.odf, *.pdf, *.ppt, *.pptx, *.ps, *.rtf, *.txt, *.xls, *.xlsx";
            dlg.Filter = "(Support Files)|*.doc;*.docx;*.odf;*.pdf;*.ppt;*.pptx;*.ps;*.rtf;*.txt;*.xls;*.xlsx"; //filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document

                string filename = dlg.FileName;
                WebClient client = new WebClient();
                byte[] response = client.UploadFile("https://translate.googleusercontent.com/translate_f?hl=" + data["option"]["lang"] + "&tl=" + data["option"]["lang"], filename);
                client.Headers.Add(HttpRequestHeader.AcceptCharset, "utf-8");
                string s = client.Encoding.GetString(response);
                var path = Directory.GetCurrentDirectory() + "\\Read.html";
                try
                {
                    File.Delete(path);
                }
                catch { }
                using (var sw = new StreamWriter(File.Open(path, FileMode.CreateNew), Encoding.UTF8))
                {
                    sw.WriteLine(s);
                }
                System.Diagnostics.Process.Start(path);

            }


        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
                thread6 = new Thread(() => OpenTransFileBox());
            thread6.SetApartmentState(ApartmentState.STA);
            thread6.IsBackground = true;
                thread6.Start();
        }
    }
}

