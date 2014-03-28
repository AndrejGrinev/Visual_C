using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace scan_betexplorer_c
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           String adr, nm;
           Boolean cn;
           nm = "";
           cn = true;
           this.textBox1.AppendText("поехали 1...");
           nm = "c:/betexplorer_c/";
           System.IO.Directory.CreateDirectory(nm);
           nm = "c:/betexplorer_c/koren/";
           System.IO.Directory.CreateDirectory(nm);
           adr = "http://www.betexplorer.com/soccer/";
           nm = "c:/betexplorer_c/koren/koren.txt";
           if (DownloadFile(adr, nm)) cn = false;
           this.textBox1.AppendText("закончили 1.");
        }

        static bool DownloadFile(String URL, String Location) 
        {
          try
          {
            WebClient WC = new WebClient();
            WC.DownloadFile(URL, Location);
            return true;
          }
          catch 
          {
            return false;
          }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          String nm, str1, str2, str4, v, bn0 ; 
          int i9, i1, i0, i2, i3, i4; 
          bool cn;
          String[] aryTextFile;

          string nl = System.Environment.NewLine;
          nm = "c:/betexplorer_c/koren/koren.txt";
          str1 = "countries";
          str2 = "clear";
          i9 = 0;
          i0 = 0;
          cn=true;
          this.textBox1.AppendText("поехали 2...");
          StreamReader sr = new StreamReader(nm);
	      
          while ((v = sr.ReadLine()) != null)
          {     
            if (i9 == 0)  i0 = v.IndexOf(str1);
            if (i0 > 0)  i9 = 1;
            if (i9 == 1) 
            {
              aryTextFile = Regex.Split(v,"></span>");
              foreach (string word in aryTextFile)
              {   
                i1 = word.IndexOf("</a");
                i3 = word.IndexOf("</span>");
                if (i1 > 0 & i3==-1)
                {
                  str4 = word.Substring(0, word.Length - 4);
                  i4 = str4.IndexOf("Results");
                  if (i4 > 0) cn = false;
                  if (str4.Length > 0 & cn) 
                  {
                    bn0 = "c:/betexplorer_c/" + str4 + "/";
                    System.IO.Directory.CreateDirectory(bn0);
                  }
                }
              }
            }
            i2 = v.IndexOf(str2);
            if (i9 == 1 & i2 > 0)  i9 = 2;
          }
          sr.Close();
          sr.Dispose();
          this.textBox1.AppendText("закончили 2.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
          String nm, str1, str2, str4, v, bn, adr, str5, str6 ;
          int i9, i1, i0, i2, ii, kk, i11 ;
          String[] aryTextFile,Folders;
          Boolean cn;
          WebClient WC = new WebClient();
          string nl = System.Environment.NewLine;

          str1 = "league-list";
          str2 = "2011";
          this.textBox1.AppendText("поехали 3...");
          Folders = System.IO.Directory.GetDirectories("c:/betexplorer_c/");
          for (ii = 0; ii<=Folders.Length - 1;++ii)
          {
            kk = Folders[ii].LastIndexOf("/");
            str5 = Folders[ii].Substring(kk + 1);
            if (str5 != "koren") 
            {
              i9 = 0;
              i0 = 0;
              cn = true;
              adr = "http://www.betexplorer.com/soccer/" + str5 + "/";
              nm = "c:/betexplorer_c/" + str5 + "/" + str5 + ".nm";
              if (DownloadFile(adr, nm)) cn = true;
              StreamReader sr = new StreamReader(nm);
	          while ((v = sr.ReadLine()) != null)
              {
                if (i9 == 0) i0 = v.IndexOf(str1);
                if (i0 > 0)  i9 = 1;
                if (i9 == 1)
                {
                  aryTextFile = Regex.Split(v,"><");
                  foreach (string word in aryTextFile)
                  {
                    i1 = word.IndexOf("href");
                    if (i1 > 0)
                    {
                      if (word.IndexOf("2011")>0) cn = false;
                      str4 = word.Substring(9, word.Length - 10);
                      if (i1 > 0 & cn & str4.Length>0)
                      {
                          i11 = str4.IndexOf(">");   
                          if (i11>0)
                              str6 = str4.Substring(0, i11 - 2) + "/";
                          else
                              str6 = str4 + "/";
                          if (str6.Length > 6)
                          {
                            bn = "c:/betexplorer_c/" + str5 + "/" + str5 + ".name";
                            System.IO.File.AppendAllText(bn, str6 + nl);
                          }
                      }
                    }
                  }
                }
                i2 = v.IndexOf(str2);
                if (i9 == 1 & i2 > 0) i9 = 2;
              }
              sr.Close();
              sr.Dispose();   
            }
          }
          this.textBox1.AppendText("закончили 3.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
          String nm, str1, v, adr, str5, tek_fl, kz1;
          int i0, ii, kk, jj, kk2, kk3, i1;
          Boolean cn;
          WebClient WC = new WebClient();
          String[] Folders;
          WebBrowser webbrowser1 = new WebBrowser();
          string nl = System.Environment.NewLine;
          webbrowser1.ScriptErrorsSuppressed = true;
          str1 = "matchdetails.php?matchid=";
          this.textBox1.AppendText("поехали 4..." + nl);
          kk3=0;
          Folders = System.IO.Directory.GetDirectories("c:/betexplorer_c/");
          for (ii = 0; ii<=Folders.Length - 1; ++ii)
          {
            kk2 = 0;
            kk = Folders[ii].LastIndexOf("/");
            str5 = Folders[ii].Substring(kk + 1);
            tek_fl = Folders[ii] + "/matchi.ss";
            if (str5 != "koren")
            {
              String[] Farr= System.IO.Directory.GetFiles(Folders[ii], "*.name", System.IO.SearchOption.TopDirectoryOnly);
              for (jj = 0; jj<=Farr.Length - 1; ++jj)
              {
                StreamReader sr = new StreamReader(Farr[jj]);
                while ((v = sr.ReadLine()) != null)
                {
                  kk = 0;
                  this.textBox1.AppendText(v);
                  adr = "http://www.betexplorer.com/" + v + "results/";
                  nm = "c:/betexplorer_c/tek_liga.res";
                  if (DownloadFile(adr, nm)) cn = true;
                  if (File.Exists(nm)) 
                  {
                    using (var file = System.IO.File.OpenText(nm))
                    {
                      while (!file.EndOfStream)
                      {
                        String sInputLine = file.ReadLine();
                        if (sInputLine.Length > 0)
                        {
                          i0 = sInputLine.IndexOf(str1);
                          if (i0 > 0)
                          {
                            kk = kk + 1;
                            kk2 = kk2 + 1;
                            kk3 = kk3 + 1;
                            i1 = sInputLine.IndexOf("onclick");
                            kz1 = "http://www.betexplorer.com/" + v + sInputLine.Substring(i0, i1 - i0 - 2);
                            System.IO.File.AppendAllText(tek_fl, kz1 + nl);
                          }
                        }
                      }
                    }
                  this.textBox1.AppendText(" == " + kk + nl);
                  System.IO.File.Delete(nm);
                  }
                }
                this.textBox1.AppendText(" По стране== " + kk2 + " Всего== " + kk3 + nl);
                this.textBox1.AppendText(nl);
                sr.Close();
                sr.Dispose();
              }
            }
          }
          this.textBox1.AppendText(" Всего== " + kk3 + nl);
          this.textBox1.AppendText("закончили 4.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
          String nm, v, adr, str5, tek_fl, kz1, itog, rn, tsr, tsl, rs, ls, cnt, goalsr, goalsl, kz2, od1, od2, od3, kz3;
          int i0, ii, kk, jj, i1, i19, i20, i21;
          bool cn;
          WebClient WC = new WebClient();
          String[] Folders;
          WebBrowser webbrowser1 = new WebBrowser();
          webbrowser1.ScriptErrorsSuppressed = true;
          string nl = System.Environment.NewLine;
          this.textBox1.AppendText("поехали 5..." + nl);
          Folders = System.IO.Directory.GetDirectories("c:/betexplorer_c/");
          for (ii = 0; ii<=0; ++ii)
          {
            this.textBox1.AppendText(Folders[ii] + nl);
            kk = Folders[ii].LastIndexOf("/");
            tek_fl = Folders[ii] + "/matchi.all";
            str5 = Folders[ii].Substring(kk + 1);
            if (System.IO.File.Exists(tek_fl)) System.IO.File.Delete(tek_fl);
            if (str5 != "koren")
            {
              String[] Farr = System.IO.Directory.GetFiles(Folders[ii], "*.ss", System.IO.SearchOption.TopDirectoryOnly);
              for (jj = 0; jj<=Farr.Length - 1; ++jj)
              {
                StreamReader sr = new StreamReader(Farr[jj]);
                while ((v = sr.ReadLine()) != null)
                {
                  kk = 0;
                  adr = v;
                  nm = "c:/betexplorer_c/tek_liga.txt";
                  if (DownloadFile(adr, nm)) cn = true;
                  rn = "";
                  tsr = "";
                  tsl = "";
                  rs = "";
                  ls = "";
                  cnt = "";
                  goalsl = "";
                  goalsr = "";
                  od1 = "";
                  od2 = "";
                  od3 = "";
                  if (File.Exists(nm))
                  {
                    i19 = 0;
                    i20 = 0;
                    i21 = 0;
                    itog = "";
                    using (var file = System.IO.File.OpenText(nm))
                    {
                      while (!file.EndOfStream)
                      {
                        String sInputLine = file.ReadLine();
                        if (sInputLine.Length > 0)
                        {
                          i0 = sInputLine.IndexOf("right nobr");
                          if (i0 > 0)
                          {
                            kz1 = sInputLine.Substring(i0 + 10);
                            i1 = kz1.IndexOf("</");
                            rn = kz1.Substring(0, i1 - 1);
                          }
                          i0 = sInputLine.IndexOf("tscore");
                          if (i0 > 0) i19 = 1;
                          if (i19 == 1) 
                          {
                            i0 = sInputLine.IndexOf("right");
                            if (i0 > 0)
                            {
                              i19 = 0;
                              kz1 = sInputLine.Substring(i0 + 14);
                              i1 = kz1.IndexOf("</");
                              tsr = kz1.Substring(0, i1 );
                            }
                            i0 = sInputLine.IndexOf("left");
                            if (i0 > 0)
                            {
                              kz1 = sInputLine.Substring(i0 + 13);
                              i1 = kz1.IndexOf("</");
                              tsl = kz1.Substring(0, i1 );
                            }
                          }
                          i0 = sInputLine.IndexOf("right scorecell");
                          if (i0 > 0) 
                          {
                            kz1 = sInputLine.Substring(i0 + 17);
                            i1 = kz1.IndexOf("</");
                            rs = kz1.Substring(0, i1 );
                          }
                          i0 = sInputLine.IndexOf("left scorecell");
                          if (i0 > 0) 
                          {
                            kz1 = sInputLine.Substring(i0 + 16);
                            i1 = kz1.IndexOf("</");
                            ls = kz1.Substring(0, i1 );
                          }
                          i0 = sInputLine.IndexOf("center");
                          if (i0 > 0 & cnt.Length == 0)
                          {
                            kz1 = sInputLine.Substring(i0 + 8);
                            i1 = kz1.IndexOf("</");
                            cnt = kz1.Substring(0, i1 );
                          }
                          i0 = sInputLine.IndexOf("div class=\"fl");
                          if (i0 > 0) i20 = 1;
                          i0 = sInputLine.IndexOf("div class=\"fr");
                          if (i0 > 0) i21 = 1;
                          if (i20 == 1 | i21 == 1) 
                          {
                            i0 = sInputLine.IndexOf("width: 4ex");
                            if (i0 > 0)
                            {
                              kz1 = sInputLine.Substring(i0 + 12);
                              i1 = kz1.IndexOf("</");
                              if (i21 == 0)
                                goalsl = goalsl + " " + kz1.Substring(0, i1 - 1);
                              else
                                goalsr = goalsr + " " + kz1.Substring(0, i1 - 1);
                            }
                          }
                          i0 = sInputLine.IndexOf("Average odds");
                          if (i0 > 0)
                          {
                            kz1 = sInputLine.Substring(i0 + 15);
                            i1 = kz1.IndexOf("data-odd=");
                            kz2 = kz1.Substring(i1 + 9);
                            i1 = kz2.IndexOf("</");
                            od1 = kz2.Substring(0, i1 - 3);

                            kz3 = kz2.Substring(i1 + 1);
                            i1 = kz3.IndexOf("data-odd=");
                            kz2 = kz3.Substring(i1 + 9);
                            i1 = kz2.IndexOf("</");
                            od2 = kz2.Substring(0, i1 - 3);
                              
                            kz3 = kz2.Substring(i1 + 1);
                            i1 = kz3.IndexOf("data-odd=");
                            kz2 = kz3.Substring(i1 + 9);
                            i1 = kz2.IndexOf("</");
                            od3 = kz2.Substring(0, i1 - 3);
                          }
                        }
                      }
                    }
                    itog = "..dt=" + rn + "..hm=" + tsr + "..aw=" + tsl + "..sht=" + rs + "-" + ls + "..per=" + cnt + "..ghm=" + goalsl + "..gaw=" + goalsr + "..od1=" + od1 + "..od2=" + od2 + "..od3=" + od3;
                    System.IO.File.AppendAllText(tek_fl, itog + nl);
                    System.IO.File.Delete(nm);
                  }
                }
                this.textBox1.AppendText(nl);
                sr.Close();
                sr.Dispose();
              }
            }
          }
          this.textBox1.AppendText("закончили 5.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
          String str5, sInputLine, tek_fl, hm, aw, fl;
          int i0, ii, kk, jj, i1, i2;
          String[] Folders;
          Boolean flg1, flg2;
          string nl = System.Environment.NewLine;
          this.textBox1.AppendText("поехали 6..." + nl);
          Folders = System.IO.Directory.GetDirectories("c:/betexplorer_c/");
          tek_fl = "c:/betexplorer_c/strani.txt";
          if (File.Exists(tek_fl)) System.IO.File.Delete(tek_fl);
          for (ii = 0; ii<=Folders.Length - 1; ++ii)
          {
            kk = Folders[ii].LastIndexOf("/");
            str5 = Folders[ii].Substring(kk + 1);
            if (str5 != "koren") System.IO.File.AppendAllText(tek_fl, str5 + nl);
          }
          Folders = System.IO.Directory.GetDirectories("c:/betexplorer_c/");
          for (ii = 0; ii<=Folders.Length - 1; ++ii)
          {
            this.textBox1.AppendText(Folders[ii] + nl);
            kk = Folders[ii].LastIndexOf("/");
            str5 = Folders[ii].Substring(kk + 1);
            if (str5 != "koren") 
            {
              String[] Farr = System.IO.Directory.GetFiles(Folders[ii], "*.all", System.IO.SearchOption.TopDirectoryOnly);
              for (jj = 0; jj<=Farr.Length - 1; ++jj)
              {
                fl = Folders[ii] + "/komandi.txt";
                if (File.Exists(fl)) System.IO.File.Delete(fl);
                StreamReader sr = new StreamReader(Farr[jj]);
                while ((sInputLine = sr.ReadLine()) != null)
                {
                  i0 = sInputLine.IndexOf("..hm");
                  i1 = sInputLine.IndexOf("..aw");
                  i2 = sInputLine.IndexOf("..sht");
                  hm = sInputLine.Substring(i0 + 6, i1 - i0 - 6);
                  aw = sInputLine.Substring(i1 + 6, i2 - i1 - 6);
                  if (File.Exists(fl))
                  {
                    flg1 = true;
                    flg2 = true;
                    using (var file = System.IO.File.OpenText(fl))
                    {
                      while (!file.EndOfStream)
                      {
                        String sInputLine2 = file.ReadLine();
                        if (sInputLine2.Length > 0)
                        {
                          if (sInputLine2 == hm) flg1 = false;
                          if (sInputLine2 == aw) flg2 = false;
                        }
                      }
                    }
                    if (flg1) System.IO.File.AppendAllText(fl, hm + nl);
                    if (flg2) System.IO.File.AppendAllText(fl, aw + nl);
                  }
                  else
                  {
                    System.IO.File.AppendAllText(fl, hm + nl);
                    System.IO.File.AppendAllText(fl, aw + nl);
                  }
                }
                this.textBox1.AppendText(nl);
                sr.Close();
                sr.Dispose();
              }
            }
          }
          this.textBox1.AppendText("закончили 6.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
          String nm, str1, v, adr, str5, sInputLine, tek_fl, kz1, filetext, tek_fl1;
          int i0, ii, kk, jj, kk2, kk3, i1, k1;
          String itog, rn, tsr, tsl, rs, ls, cnt, goalsr, goalsl, kz2, od1, od2, od3, kz3;
          int i19, i20, i21;
          bool cn;
          String[] Folders;
                
          WebClient WC = new WebClient();
          WebBrowser webbrowser1 = new WebBrowser();
          webbrowser1.ScriptErrorsSuppressed = true;
          string nl = System.Environment.NewLine;
          str1 = "matchdetails.php?matchid=";
          this.textBox1.AppendText("поехали 7..." + nl);
          Folders = System.IO.Directory.GetDirectories("c:/betexplorer_c/");
          kk3 = 0;
          str5 = "";
          for (ii = 0; ii<=Folders.Length - 1; ++ii)
          {
            kk2 = 0;
            kk = Folders[ii].LastIndexOf("/");
            str5 = Folders[ii].Substring(kk + 1);
            tek_fl1 = Folders[ii] + "/matchi.ss";
            tek_fl = Folders[ii] + "/matchi.ss2";
            if (File.Exists(tek_fl)) System.IO.File.Delete(tek_fl);
            if (str5 != "koren") 
            {
              String[] Farr = System.IO.Directory.GetFiles(Folders[ii], "*.name", System.IO.SearchOption.TopDirectoryOnly);
              for (jj = 0; jj<=Farr.Length - 1; ++jj)
              {
                StreamReader sr = new StreamReader(Farr[jj]);
                while ((v = sr.ReadLine()) != null)
                {
                  kk = 0;
                  this.textBox1.AppendText(v);
                  adr = "http://www.betexplorer.com" + v + "results/";
                  nm = "c:/betexplorer_c/tek_liga.res";
                  if (DownloadFile(adr, nm)) cn = true;
                  if (File.Exists(nm)) 
                  {
                    using (var file = System.IO.File.OpenText(nm))
                    {
                      while (!file.EndOfStream)
                      {
                        sInputLine = file.ReadLine();
                        if (sInputLine.Length > 0)
                        {
                          i0 = sInputLine.IndexOf(str1);
                          if (i0 > 0) 
                          {
                            i1 = sInputLine.IndexOf("onclick");
                            kz1 = "http://www.betexplorer.com" + v + sInputLine.Substring(i0 - 1, i1 - i0 - 2);
                            filetext = File.ReadAllText(tek_fl1);
                            k1 = filetext.LastIndexOf(kz1);
                            if (k1 == -1) 
                            {
                              System.IO.File.AppendAllText(tek_fl, kz1 + nl);
                              kk = kk + 1;
                              kk2 = kk2 + 1;
                              kk3 = kk3 + 1;
                            }
                          }
                        }
                      }
                    }
                    this.textBox1.AppendText(" == " + kk + nl);
                    System.IO.File.Delete(nm);
                  }
                }
                this.textBox1.AppendText(" По стране== " + kk2 + " Всего== " + kk3 + nl);
                this.textBox1.AppendText(nl);
                sr.Close();
                sr.Dispose();
              }
            }
            tek_fl = Folders[ii] + "/matchi.all2";
            if (File.Exists(tek_fl)) System.IO.File.Delete(tek_fl);
            if (str5 != "koren")
            {
              String[] Farr = System.IO.Directory.GetFiles(Folders[ii], "*.ss2", System.IO.SearchOption.TopDirectoryOnly);
              for (jj = 0; jj<=Farr.Length - 1; ++jj)
              {
                StreamReader sr = new StreamReader(Farr[jj]);
                while ((v = sr.ReadLine()) != null)
                {
                  kk = 0;
                  adr = v;
                  nm = "c:/betexplorer_c/tek_liga.txt";
                  if (DownloadFile(adr, nm)) cn = true;
                  rn = "";
                  tsr = "";
                  tsl = "";
                  rs = "";
                  ls = "";
                  cnt = "";
                  goalsl = "";
                  goalsr = "";
                  od1 = "";
                  od2 = "";
                  od3 = "";
                  if (File.Exists(nm)) 
                  {
                    i19 = 0;
                    i20 = 0;
                    i21 = 0;
                    itog = "";
                    using (var file = System.IO.File.OpenText(nm))
                    {
                      while (!file.EndOfStream)
                      {
                        sInputLine = file.ReadLine();
                        if (sInputLine.Length > 0)
                        {
                          i0 = sInputLine.IndexOf("right nobr");
                          if (i0 > 0)
                          {
                            kz1 = sInputLine.Substring(i0 + 10);
                            i1 = kz1.IndexOf("</");
                            rn = kz1.Substring(0, i1 - 1 );
                          }
                          i0 = sInputLine.IndexOf("tscore");
                          if (i0 > 0) i19 = 1;
                          if (i19 == 1)
                          {
                            i0 = sInputLine.IndexOf("right");
                            if (i0 > 0)
                            {
                              i19 = 0;
                              kz1 = sInputLine.Substring(i0 + 14);
                              i1 = kz1.IndexOf("</");
                              tsr = kz1.Substring(0, i1 );
                            }
                            i0 = sInputLine.IndexOf("left");
                            if (i0 > 0)
                            {
                              kz1 = sInputLine.Substring(i0 + 13);
                              i1 = kz1.IndexOf("</");
                              tsl = kz1.Substring(0, i1 );
                            }
                          }
                          i0 = sInputLine.IndexOf("right scorecell");
                          if (i0 > 0)
                          {
                            kz1 = sInputLine.Substring(i0 + 17);
                            i1 = kz1.IndexOf("</");
                            rs = kz1.Substring(0, i1 );
                          }
                          i0 = sInputLine.IndexOf("left scorecell");
                          if (i0 > 0)
                          {
                            kz1 = sInputLine.Substring(i0 + 16);
                            i1 = kz1.IndexOf("</");
                            ls = kz1.Substring(0, i1 );
                          }
                          i0 = sInputLine.IndexOf("center");
                          if (i0 > 0 & cnt.Length == 0) 
                          {
                            kz1 = sInputLine.Substring(i0 + 8);
                            i1 = kz1.IndexOf("</");
                            cnt = kz1.Substring(0, i1 );
                          }
                          i0 = sInputLine.IndexOf("div class=\"fl");
                          if (i0 > 0) i20 = 1;
                          i0 = sInputLine.IndexOf("div class=\"fr");
                          if (i0 > 0) i21 = 1;
                          if (i20 == 1 | i21 == 1)
                          {
                            i0 = sInputLine.IndexOf("width: 4ex");
                            if (i0 > 0)
                            {
                              kz1 = sInputLine.Substring(i0 + 12);
                              i1 = kz1.IndexOf("</");
                              if (i21 == 0)
                                goalsl = goalsl + " " + kz1.Substring(0, i1 - 1);
                              else
                                goalsr = goalsr + " " + kz1.Substring(0, i1 - 1);
                            }
                          }
                          i0 = sInputLine.IndexOf("Average odds");
                          if (i0 > 0)
                          {
                              kz1 = sInputLine.Substring(i0 + 15);
                            i1 = kz1.IndexOf("data-odd=");
                            kz2 = kz1.Substring(i1 + 9);
                            i1 = kz2.IndexOf("</");
                            od1 = kz2.Substring(0, i1 - 3);

                            kz3 = kz2.Substring(i1 + 1);
                            i1 = kz3.IndexOf("data-odd=");
                            kz2 = kz3.Substring(i1 + 9);
                            i1 = kz2.IndexOf("</");
                            od2 = kz2.Substring(0, i1 - 3);

                            kz3 = kz2.Substring(i1 + 1);
                            i1 = kz3.IndexOf("data-odd=");
                            kz2 = kz3.Substring(i1 + 9);
                            i1 = kz2.IndexOf("</");
                            od3 = kz2.Substring(0, i1 - 3);
                          }
                        }
                      }
                    }
                    itog = "..dt=" + rn + "..hm=" + tsr + "..aw=" + tsl + "..sht=" + rs + "-" + ls + "..per=" + cnt + "..ghm=" + goalsl + "..gaw=" + goalsr + "..od1=" + od1 + "..od2=" + od2 + "..od3=" + od3;
                    System.IO.File.AppendAllText(tek_fl, itog + nl);
                    System.IO.File.Delete(nm);
                  }
                }
                this.textBox1.AppendText(nl);
                sr.Close();
              }
            }
            nm = Folders[ii] + "/matchi.ss2";
            tek_fl = Folders[ii] + "/matchi.ss";
            if (File.Exists(nm))
            {
              using (var file = System.IO.File.OpenText(nm))
              {
                while (!file.EndOfStream)
                {
                  sInputLine = file.ReadLine();
                  if (sInputLine.Length > 0)
                  {
                    System.IO.File.AppendAllText(tek_fl, sInputLine + nl);
                  }
                }
              }
            }
            nm = Folders[ii] + "/matchi.all2";
            tek_fl = Folders[ii] + "/matchi.all";
            if (File.Exists(nm))
            {
              using (var file = System.IO.File.OpenText(nm))
              {
                while (!file.EndOfStream)
                {
                  sInputLine = file.ReadLine();
                  if (sInputLine.Length > 0)
                  {
                    System.IO.File.AppendAllText(tek_fl, sInputLine + nl);
                  }
                }
              }
            }
          }
          this.textBox1.AppendText(" Всего== " + kk3 + nl);
          this.textBox1.AppendText("закончили 7.");
        }
    }
}
