using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Security.Principal;



namespace WindowsApplication2
{ 
    public partial class Form1 : Form
    {
        
        Regex re = new Regex(@"(.+)\\(?<filename>.+)\.(.+)");
        Regex fileline = new Regex(@"FILE ""(.+)\.([^.]+?)""", RegexOptions.IgnoreCase);
        string original_path = ""; //原始文件名       
        string readText = "";   //内容
       // int code = 0;  //编码
        string FileInFileLine;        
        INIClass config = new INIClass(Application.StartupPath+@"\config.ini");
        List<string> musicfiles = new List<string>();
        char[] JISMapBuffer = File.ReadAllText(Application.StartupPath + @"\maps\jis.map", Encoding.Unicode).ToCharArray();
        char[] GBMapBuffer = File.ReadAllText(Application.StartupPath + @"\maps\gb.map", Encoding.Unicode).ToCharArray();
        char[] Big5MapBuffer = File.ReadAllText(Application.StartupPath + @"\maps\big5.map", Encoding.Unicode).ToCharArray();
        bool loaded = false;
        bool forcecode = false;
        //bool started = false;

        public Form1()
        {
            InitializeComponent();
        }

        private string OpenFile(string original_path)
        {
            if (forcecode)
            {
                if (comboBox1.Text == "950")
                    ReadBIG5();
                else readText = File.ReadAllText(original_path, Encoding.GetEncoding(int.Parse(comboBox1.Text)));
                forcecode = false;
            }
            else
            {
                switch (getcodetype(original_path))
                {
                    case "CODETYPE_UTF8NOBOM":
                        {
                            var utf8WithoutBom = new UTF8Encoding(false);
                            readText = File.ReadAllText(original_path, utf8WithoutBom);                            
                            break;
                        }
                    case "CODETYPE_SHIFTJIS":
                        {
                            readText = File.ReadAllText(original_path, Encoding.GetEncoding(932));
                            break;
                        }
                    case "CODETYPE_DEFAULT":
                        {
                            if (String.IsNullOrEmpty(comboBox1.Text))
                            {
                                comboBox1.Text = "932";
                            }
                            if (comboBox1.Text == "950")
                                ReadBIG5();
                            else readText = File.ReadAllText(original_path, Encoding.GetEncoding(int.Parse(comboBox1.Text)));
                            break;
                        }
                    case "CODETYPE_GBK":
                        {
                            readText = File.ReadAllText(original_path, Encoding.GetEncoding(936));
                            break;
                        }
                    case "CODETYPE_BIG5":
                        {
                            ReadBIG5();
                            break;
                        }
                }
            }
            string FileInFileLine = fileline.Match(readText).Groups[1].Value.ToLower();
            return FileInFileLine;
        }        

        private void ReadBIG5()
        {
            readText = "";
            // readText = File.ReadAllText(original_path, Encoding.GetEncoding(950));
            //char[] Big5MapBuffer = File.ReadAllText(Application.StartupPath + @"\maps\big5.map", Encoding.Unicode).ToCharArray();
            Byte[] MyByte = File.ReadAllBytes(original_path);
            int high, low, chr, i;
            //   var OutPutByte = new List<Byte>();
            for (i = 0; i < MyByte.Length; )
            {
                high = MyByte[i]; //读取第一个byte
                i++;
                if (high > 0x7F) //第一个byte是高位
                {
                    low = MyByte[i]; //读取低位
                    i++;
                }
                else
                {
                    low = high;
                    high = 0;
                }
                chr = low + high * 256;
                if (chr < 0x80) // ASCII码
                {
                    var encoding = new UnicodeEncoding();
                    readText += encoding.GetString(new byte[] { (byte)chr, 0 });
                }
                else
                {
                    char a = Big5MapBuffer[chr - 0x8140];
                    readText += a;
                }
            }
        }

        private void ChangeOutputFilePath()
        {
            FilePathTextBox.Text = re.Replace(original_path, @"$1\" + RenameTextBox.Text.Replace(@"%filename%", @"${filename}") + @".$2");
        }

        private bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        private void main_func()   // 主要转码部分
        {
            loaded = true;
            if (!AutoOutputCheckBox.Checked)
                OutputButton.Enabled = true;

            if (!CoverOldFileCheckBox.Checked)
                ChangeOutputFilePath();
            else
                FilePathTextBox.Text = original_path;

            FileInFileLine = OpenFile(original_path);
            string[] exts = { "ape", "tta", "flac", "tak", "wav", "m4a","wv" };
            musicfiles.Clear();    

            if (Path.GetExtension(original_path).ToLower() == ".cue")
            {
                // 自动修正「The True Audio」→「WAVE」以便于fb2k识别
                if (ttafixCheckBox.Checked)
                {
                    readText = Regex.Replace(readText, "the true audio", "WAVE", RegexOptions.IgnoreCase);
                }

                foreach (string ext in exts)
                {
                    musicfiles.AddRange(Directory.GetFiles(Path.GetDirectoryName(original_path), "*." + ext));
                }
            
                bool MultiMusicFileType = false;

                // 自动替换FILE字段文件名
                if (fileLineCheckBox.Checked)
                {
                    if (musicfiles.Count == 0)  //如果没有搜到音乐类型文件，不作任何事情
                    { }
                    else if (musicfiles.Count == 1)  //如果只搜到一个，那么将FILE行的被引用文件替换
                    {
                        string MyFile = Path.GetFileName(musicfiles[0]);
                        readText = fileline.Replace(readText, @"FILE """ + MyFile + @"""");
                    }
                    else  //如果搜到多个，那逐个尝试是否与FileInFileLine匹配
                    {
                        string MyExt = Path.GetExtension(musicfiles[0]); 
                        for (int i = 1; i < musicfiles.Count; i++)
                        {
                            if (Path.GetExtension(musicfiles[i]) != MyExt)
                            {
                                MultiMusicFileType = true;
                                break;
                            }
                        } 
                            foreach (string MyFilePath in musicfiles)
                            {
                                if (FileInFileLine == Path.GetFileNameWithoutExtension(MyFilePath).ToLower())
                                {
                                    string MyFile = Path.GetFileName(MyFilePath);
                                    readText = fileline.Replace(readText, @"FILE """ + MyFile + @"""");
                                    break;
                                }
                                if (Path.GetFileNameWithoutExtension(original_path).ToLower() == Path.GetFileNameWithoutExtension(MyFilePath).ToLower())
                                {
                                    string MyFile = Path.GetFileName(MyFilePath);
                                    readText = fileline.Replace(readText, @"FILE """ + MyFile + @"""");
                                    break;
                                }
                            }
                        if (!MultiMusicFileType) //如果都没有匹配的但是却有只有一种音乐类型，那么好歹替换了罢（咦？
                            readText = fileline.Replace(readText, @"FILE ""$1" + MyExt + @"""");
                    }
                }
            }
            FileContentTextBox.Text = readText;

            // 自动模式            
            if (AutoOutputCheckBox.Checked) output();
        }

        private string getcodetype(string path)
        {
            string strCodeType = "CODETYPE_UTF8NOBOM";

            Byte[] MyByte = File.ReadAllBytes(path);
            int high, low, chr, i;
            int JP1 = 0, JP2 = 0;
            bool FakeJP = false;

            i = 0;
            bool isUTF8 = true;
            while (i < MyByte.Length)
            {
                if ((0x80 & MyByte[i]) == 0) // ASCII
                {
                    i++;
                    continue;
                }
                else if ((0xE0 & MyByte[i]) == 0xC0) // 110xxxxx
                {
                    if (i + 1 > MyByte.Length)
                    {
                        isUTF8 = false;
                        break;
                    }
                    if ((0xC0 & MyByte[i + 1]) == 0x80) // 10xxxxxx
                    {
                        i += 2;
                        continue;
                    }
                    else
                    {
                        isUTF8 = false;
                        break;
                    }
                }
                else if ((0xF0 & MyByte[i]) == 0xE0) // 1110xxxx
                {
                    if (i + 1 > MyByte.Length)
                    {
                        isUTF8 = false;
                        break;
                    }
                    if (i + 2 > MyByte.Length)
                    {
                        isUTF8 = false;
                        break;
                    }
                    if (((0xC0 & MyByte[i + 1]) == 0x80) && ((0xC0 & MyByte[i + 2]) == 0x80)) // 10xxxxxx 10xxxxxx
                    {
                        i += 3;
                        continue;
                    }
                    else
                    {
                        isUTF8 = false;
                        break;
                    }
                }
                else // 不是UTF-8字符串
                {
                    isUTF8 = false;
                    break;
                }
            }

            if (isUTF8 == false)
                strCodeType = "CODETYPE_SHIFTJIS";

            if (strCodeType == "CODETYPE_SHIFTJIS")
            {
                for (i = 0; i < MyByte.Length; )
                {
                    high = MyByte[i]; //读取第一个byte
                    i++;
                    if (high <= 0x7F)  //ASCII码区
                    {
                        low = high;
                        high = 0;
                    }
                    else if ((high >= 0xA1) && (high <= 0xDF))  //半角片假名区
                    {
                        low = high;
                        high = 0;
                        JP1++;
                    }
                    else  //双字节区
                    {
                        low = MyByte[i]; //读取低位
                        i++;
                        JP2++;
                    }
                    chr = low + high * 256;

                    if (chr < 0x80) // ASCII
                    { }
                    else if (chr < 0xA1) // 0x80 - 0xA0 未定义空间
                    {
                        strCodeType = "CODETYPE_DEFAULT"; // 未知编码
                        break;
                    }
                    else if (chr < (0xA1 + 63)) // 0xA1 - 0xDF 半角假名区
                    { }
                    else if (chr < 0x8140) // 0xE0 - 0x813F 未定义空间
                    {
                        strCodeType = "CODETYPE_DEFAULT";  // 未知编码
                        break;
                    }
                    else // 0x8140 - 0xFFFF
                    {
                        char a = JISMapBuffer[chr - 0x8140 + 63];
                        if (a == '\uFFFD')
                        {
                            strCodeType = "CODETYPE_GBK";
                            break;
                        }
                    }
                }
            }
            

            if ((strCodeType == "CODETYPE_SHIFTJIS")&&((float)JP1/JP2 >= 1.8))
            {
                FakeJP = true;
                strCodeType = "CODETYPE_GBK";
            }

            if (strCodeType == "CODETYPE_GBK")
            {
                for (i = 0; i < MyByte.Length; )
                {
                    high = MyByte[i]; //读取第一个byte
                    i++;
                    if (high > 0x7F) //第一个byte是高位
                    {
                        low = MyByte[i]; //读取低位
                        i++;
                    }
                    else
                    {
                        low = high;
                        high = 0;
                    }

                    chr = low + high * 256;
                    if (chr < 0x80) // ASCII码
                    { }
                    else if (chr < 0x8140) // 0x80 - 0x813F 未定义空间
                    {
                        strCodeType = "CODETYPE_DEFAULT";   // 未知编码
                        break;
                    }
                    else
                    {
                        char a = GBMapBuffer[chr - 0x8140];
                        if (a == '\uFFFD')
                        {
                            strCodeType = "CODETYPE_BIG5";
                            break;
                        }
                    }
                }
            }

            if (strCodeType == "CODETYPE_BIG5")
            {
                for (i = 0; i < MyByte.Length; )
                {
                    high = MyByte[i]; //读取第一个byte
                    i++;
                    if (high > 0x7F) //第一个byte是高位
                    {
                        low = MyByte[i]; //读取低位
                        i++;
                    }
                    else
                    {
                        low = high;
                        high = 0;
                    }
                    chr = low + high * 256;
                    if (chr < 0x80) // ASCII码
                    { }
                    else if (chr < 0x8140) // 0x80 - 0x813F 未定义空间
                    {
                        strCodeType = "CODETYPE_DEFAULT";   // 未知编码
                        break;
                    }
                    else
                    {
                        char a = Big5MapBuffer[chr - 0x8140];
                        if (a == '\uFFFD')
                        {
                            strCodeType = "CODETYPE_DEFAULT";
                            break;
                        }
                    }
                }
            }
            if ((strCodeType == "CODETYPE_DEFAULT") && FakeJP)
                strCodeType = "CODETYPE_SHIFTJIS";

            return strCodeType;
        }

        private void output() 
        {            
            StreamWriter sw = new StreamWriter(FilePathTextBox.Text, false, Encoding.UTF8);
            sw.Write(FileContentTextBox.Text);
            sw.Close();

            if (Path.GetExtension(original_path).ToLower() == ".cue")
            {
                // 是否自动发送到播放器
                if (AutoSend2PlayerCheckBox.Checked)
                {
                    System.Diagnostics.Process.Start(PlayerPathTextBox.Text, paremeterTextBox.Text.Replace("%1", FilePathTextBox.Text));
                }
                //是否自动处理其他Cue文件
                if (AutoDealOtherCueCheckBox.Checked)
                {
                    if (musicfiles.Count == 0)  //如果没有搜到音乐类型文件，不作任何事情
                    { }
                    else if (musicfiles.Count == 1)  //如果只搜到一个，那么无脑隐藏其他cue
                    {
                        DealOtherCue(false);
                    }
                    else
                    {
                        DealOtherCue(true);  //如果有多个音乐类型文件，那么仅隐藏与该cue引用了同一个音乐文件的cue
                    }                   

                }
            }

            musicfiles.Clear();
            FilePathTextBox.Text = "";
            FileContentTextBox.Text = "";
            OutputButton.Enabled = false;
            loaded = false;

            // 是否自动退出
            if ((AutoExitCheckBox.Checked) && (AutoExitCheckBox.Enabled))
            {
                Application.Exit();
            }
        }

        private void DealOtherCue(bool MultiMusicFile)
        {
            List<string> CueFile = new List<string>();
            CueFile.AddRange(Directory.GetFiles(Path.GetDirectoryName(original_path), "*.cue"));
            for (int i = 0; i < CueFile.Count; i++)
            {
                if (CueFile[i] != FilePathTextBox.Text)
                {
                    if (!MultiMusicFile)
                    {
                        DealOtherCueChild(CueFile[i]);
                    }
                    else
                    {
                        if (FileInFileLine == OpenFile(CueFile[i]))
                        {
                            DealOtherCueChild(CueFile[i]);
                        }
                    }
                }
            }
        }

        private void DealOtherCueChild(string path)
        {
            if (radioButton1.Checked)
            {
                FileAttributes fa = File.GetAttributes(path);
                File.SetAttributes(path, FileAttributes.Hidden | fa);
            }
            else if (radioButton2.Checked)
            {
                FileSystem.DeleteFile(path, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);                
            }
            else if (radioButton3.Checked)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(original_path) + @"\" + SubDirTextBox.Text + @"\");
                File.Move(path, Path.GetDirectoryName(original_path) + @"\" + SubDirTextBox.Text + @"\" + Path.GetFileName(path));
            }

        }

        private void button1_Click(object sender, EventArgs e)   //通过“打开”对话框打开文件
        {            
            
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {                
                original_path = OpenFileDialog.FileName;
                main_func();
            }       
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)  //通过拖拽打开文件
        {            
            original_path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            main_func(); 
        }


        private void Form1_DragEnter_1(object sender, DragEventArgs e)  //通过拖拽打开文件必须
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }

        //private void ReadConfig(object output, string IniDir, string IniValue)
        //{
        //    string MyValue = null;
        //    try
        //    {
        //        MyValue = config.IniReadValue(IniDir, IniValue);
        //    }
        //    catch
        //    {
        //    }
        //    if (!String.IsNullOrEmpty(MyValue))
        //    {
        //        if (Regex.IsMatch(MyValue, "^[0-9]+$"))
        //        {
        //            output = Convert.ToInt16(MyValue);
        //        }
        //        else if (Regex.IsMatch(MyValue, "true|flae", RegexOptions.IgnoreCase))
        //        {
        //            output = Convert.ToBoolean(MyValue);
        //        }
        //        else
        //        {
        //            output = MyValue;
        //        }
        //    }
        //}

        private void Form1_Load(object sender, EventArgs e)   //读取配置，初始化
        {
            string[] com = Environment.GetCommandLineArgs();
            comboBox1.Text = "932";
        
            if (File.Exists(Application.StartupPath + @"\config.ini"))
            {
                try
                {
                    //int MyLeft = -99, Mytop = -99;
                    //ReadConfig(MyLeft, "Location", "x");
                    //ReadConfig(Mytop, "Location", "y");
                    int MyLeft = Convert.ToInt16(config.IniReadValue("Location", "x"));
                    int Mytop = Convert.ToInt16(config.IniReadValue("Location", "y"));
                    if ((MyLeft != -99) && (Mytop != -99))
                        this.Location = new Point(MyLeft, Mytop);
                    RenameTextBox.Text = config.IniReadValue("Settings", "regex_name");
                    comboBox1.Text = config.IniReadValue("Settings", "code");                    
                    CoverOldFileCheckBox.Checked = Convert.ToBoolean(config.IniReadValue("Settings", "overwrite"));
                    AutoOutputCheckBox.Checked = Convert.ToBoolean(config.IniReadValue("Settings", "auto"));
                    ttafixCheckBox.Checked = Convert.ToBoolean(config.IniReadValue("Settings", "ttatowave"));
                    fileLineCheckBox.Checked = Convert.ToBoolean(config.IniReadValue("Settings", "fixeacname"));
                    AutoExitCheckBox.Checked = Convert.ToBoolean(config.IniReadValue("Settings", "autoexit"));
                    AutoDealOtherCueCheckBox.Checked = Convert.ToBoolean(config.IniReadValue("Settings", "autodealothercue"));
                    AutoSend2PlayerCheckBox.Checked = Convert.ToBoolean(config.IniReadValue("Settings", "sendtoplayer"));
                    PlayerPathTextBox.Text = config.IniReadValue("Settings", "playerpath");
                    paremeterTextBox.Text = config.IniReadValue("Settings", "playercommand");
                    SubDirTextBox.Text = config.IniReadValue("Settings", "subdir");
                    foreach (Control a in this.groupBox1.Controls)
                    {
                        if (a.Name == "radioButton" + config.IniReadValue("Settings", "cuedealmethod"))
                        {
                            ((RadioButton)a).Checked = true;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }            
            if (com.Length != 1)
            {
                original_path = com[1];
                main_func();
            }            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)   //保存配置
        {
            config.IniWriteValue("Location", "x", this.Location.X.ToString());
            config.IniWriteValue("Location", "y", this.Location.Y.ToString());
            config.IniWriteValue("Settings", "regex_name", RenameTextBox.Text);
            config.IniWriteValue("Settings", "code", comboBox1.Text);
            config.IniWriteValue("Settings", "overwrite",CoverOldFileCheckBox.Checked.ToString());
            config.IniWriteValue("Settings", "auto", AutoOutputCheckBox.Checked.ToString());
            config.IniWriteValue("Settings", "ttatowave", ttafixCheckBox.Checked.ToString());
            config.IniWriteValue("Settings", "fixeacname", fileLineCheckBox.Checked.ToString());
            config.IniWriteValue("Settings", "autoexit", AutoExitCheckBox.Checked.ToString());
            config.IniWriteValue("Settings", "sendtoplayer", AutoSend2PlayerCheckBox.Checked.ToString());
            config.IniWriteValue("Settings", "autodealothercue", AutoDealOtherCueCheckBox.Checked.ToString());
            config.IniWriteValue("Settings", "playerpath", PlayerPathTextBox.Text);
            config.IniWriteValue("Settings", "playercommand", paremeterTextBox.Text);
            config.IniWriteValue("Settings", "subdir", SubDirTextBox.Text);
            foreach (Control a in this.groupBox1.Controls)
            { 
                if (a.Name.Contains("radioButton") && ((RadioButton)a).Checked)
                {
                    config.IniWriteValue("Settings", "cuedealmethod", a.Name.Replace("radioButton",""));
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)   // 手动输出
        {
            output();    
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)  // 是否替换源文件
        {
            
            if (CoverOldFileCheckBox.Checked) 
            {
                RenameTextBox.Enabled = false;
                FilePathTextBox.Text = original_path;
              //  RenameTextBox.Text = "%filename%";
            }
            else 
            {
                RenameTextBox.Enabled = true;
                ChangeOutputFilePath();
              //  RenameTextBox.Text = temp;
            }            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)  //自动模式时禁用「输出」按钮
        {
            if (AutoOutputCheckBox.Checked)
            {
                OutputButton.Enabled = false;
                AutoExitCheckBox.Enabled = true;
            }
            else
            {
                if (loaded) OutputButton.Enabled = true;
                AutoExitCheckBox.Enabled = false;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!CoverOldFileCheckBox.Checked)
                ChangeOutputFilePath();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoSend2PlayerCheckBox.Checked)
            {
                PlayerPathTextBox.Enabled = true;
                paremeterTextBox.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                PlayerPathTextBox.Enabled = false;
                paremeterTextBox.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void AutoDealOtherCueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoDealOtherCueCheckBox.Checked)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                SubDirTextBox.Enabled = true;
            }
            else
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                SubDirTextBox.Enabled = false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (OpenPlayerDialog.ShowDialog() == DialogResult.OK)
            {
                PlayerPathTextBox.Text = OpenPlayerDialog.FileName;                
            }           
        }

        private void button4_Click(object sender, EventArgs e) // 添加到注册表
        {
            if (IsUserAdministrator())
            {
                RegistryKey Root = Registry.ClassesRoot;
                RegistryKey software = null;
                RegistryKey CurrentUser = Registry.CurrentUser;

                if ((System.Environment.OSVersion.Version.Major == 5) || ((System.Environment.OSVersion.Version.Major == 6) && (System.Environment.OSVersion.Version.Minor == 0)))
                {
                    software = CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.cue\OpenWithProgids");
                    if (software != null)
                    {
                        foreach (string b in software.GetValueNames())
                        {
                            AddToReg(b);
                        }
                    }
                    string a = "";
                    software = Root.OpenSubKey(".cue");
                    if (software != null)
                        a = software.GetValue("").ToString();
                    if (a != "")
                        AddToReg(a);
                    else
                    {
                        AddToReg(".cue");
                    }
                }

                if ((System.Environment.OSVersion.Version.Major == 6) && (System.Environment.OSVersion.Version.Minor >= 1))
                {
                    string a = "";
                    software = CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.cue\UserChoice");
                    if (software != null)
                    {
                        if (software.GetValue("Progid") != null)
                            a = software.GetValue("Progid").ToString();
                    }
                    if (a != "")
                    {
                        AddToReg(a);
                    }
                    else
                    {
                        software = Root.OpenSubKey(".cue");
                        if (software != null)
                            a = software.GetValue("").ToString();
                        if (a != "")
                            AddToReg(a);
                        else
                        {
                            AddToReg(".cue");
                        }
                    }
                }
                MessageBox.Show("已成功注册到右键菜单!");
            }
            else
            {
                MessageBox.Show("请重启程序用管理员身份运行!");
            }
        }

        void AddToReg(string a)
        {
            RegistryKey Root = Registry.ClassesRoot;
            RegistryKey cue = Root.CreateSubKey(a);
            RegistryKey shell = cue.CreateSubKey("shell");
            if (shell.OpenSubKey("用 FixCue 修复") != null)
                shell.DeleteSubKeyTree("用 FixCue 修复");
            if (shell.OpenSubKey("Fix") != null)
                shell.DeleteSubKeyTree("Fix");
            RegistryKey mystring = shell.CreateSubKey("Fix");
            mystring.SetValue("", "用 FixCue 修复");
            RegistryKey command = mystring.CreateSubKey("command");
            command.SetValue("", @"""" + Application.ExecutablePath + @""" ""%1""");
        }

        private void button5_Click(object sender, EventArgs e) //从注册表移除
        {
            if (IsUserAdministrator())
            {
                RegistryKey CurrentUser = Registry.CurrentUser;
                RegistryKey Root = Registry.ClassesRoot;
                RegistryKey software = null;

                if ((System.Environment.OSVersion.Version.Major == 5) || ((System.Environment.OSVersion.Version.Major == 6) && (System.Environment.OSVersion.Version.Minor == 0)))
                {
                    software = CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.cue\OpenWithProgids");
                    if (software != null)
                    {
                        foreach (string b in software.GetValueNames())
                        {
                            RemoveFromReg(b);
                        }
                    }
                    string a = "";
                    software = Root.OpenSubKey(".cue");
                    if (software != null)
                        a = software.GetValue("").ToString();
                    if (a != "")
                        RemoveFromReg(a);
                    else
                    {
                        RemoveFromReg(".cue");
                    }
                }

                if ((System.Environment.OSVersion.Version.Major == 6) && (System.Environment.OSVersion.Version.Minor >= 1))
                {

                    string a = "";
                    software = CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.cue\UserChoice");
                    if (software != null)
                    {
                        if (software.GetValue("Progid") != null)
                            a = software.GetValue("Progid").ToString();
                    }
                    if (a != "")
                    {
                        RemoveFromReg(a);
                    }
                    else
                    {
                        software = Root.OpenSubKey(".cue");
                        if (software != null)
                            a = software.GetValue("").ToString();
                        if (a != "")
                            RemoveFromReg(a);
                        else
                        {
                            RemoveFromReg(".cue");
                        }
                    }
                }
                MessageBox.Show("已成功从右键菜单卸载!");
            }
            else
            {
                MessageBox.Show("请重启程序用管理员身份运行!");
            }
        }

        void RemoveFromReg(string a)
        {
            RegistryKey Root = Registry.ClassesRoot;
            RegistryKey cue_shell = Root.OpenSubKey(a + @"\shell", true);
            if (cue_shell != null)
            {
                if (cue_shell.OpenSubKey("用 FixCue 修复") != null)
                    cue_shell.DeleteSubKeyTree("用 FixCue 修复");
                if (cue_shell.OpenSubKey("Fix") != null)
                    cue_shell.DeleteSubKeyTree("Fix");
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width >= 817)
            { pictureBox1.Left = this.Width - 56; }
            else
                pictureBox1.Left = 761;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                forcecode = true;
                main_func();
            }
        }
    }

    public class INIClass
    {
        public string inipath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// 
        /// 构造方法
        /// 
        /// 文件路径
        public INIClass(string INIPath)
        {
            inipath = INIPath;
        }
        /// 
        /// 写入INI文件
        /// 
        /// 项目名称(如 [TypeName] )
        /// 键
        /// 值
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.inipath);
        }
        /// 
        /// 读出INI文件
        /// 
        /// 项目名称(如 [TypeName] )
        /// 键
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            return temp.ToString();
        }
        /// 
        /// 验证文件是否存在
        /// 
        /// 布尔值
        public bool ExistINIFile()
        {
            return System.IO.File.Exists(inipath);
        }
    }   

}