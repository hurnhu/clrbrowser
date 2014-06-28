using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clrbrowser_installer
{
    public partial class Form1 : Form
    {   
        //Stores current working dir, programfiles(x86), and names for plugins
        string obsPluginFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\OBS\\plugins";
        string CWD = Directory.GetCurrentDirectory();
        string clrdll = @"\CLRHostPlugin.dll";
        string clrFolder = @"\CLRHostPlugin";
        string obsNewPath;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (Directory.Exists(obsPluginFolder))
            {
                try
                {
                    //moves the clr dll to the obs plugin folder while keeping the same name
                    File.Move((CWD + clrdll), (obsPluginFolder + clrdll));
                    //moves the dir CLRHostPlugin to the obs plugin folder while keeping same name
                    Directory.Move((CWD + clrFolder), (obsPluginFolder + clrFolder)); 
                }
                catch (FileNotFoundException)
                {
                    //if CLRHostPlugin or CLRHostPlugin.dll are not found in the same dir as the exe this error will be shown
                    MessageBox.Show("Exception thrown: Files not found. Please check to make sure exe is in same loaction with the clr.dll and the clr folder. Also make sure the dll is named CLRHostPlugin.dll and the folder is named CLRHostPlugin");
                   
                }
                catch (AccessViolationException)
                {
                    //if for some reason the application is not ran as admin this will be thrown (should request admin by default, check comments in app.manifest)
                    MessageBox.Show("Exception thrown: Not running as administrator.");
                    
                }
                catch (System.IO.IOException)
                {
                    //if files are already in the obs plugin folder this will be thrown.
                    MessageBox.Show("looks like CLR is already installed!");
                }
                

            }
            else
            {
                //if the obs plugin folder is not found, it will change the second tab asking user where there obs is located
                tabControl1.SelectedIndex = 1;
                //sets the size of the form to 500, 180 to give more room for user input
                this.Size = new Size(500, 180);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
       

        private void openfile(object sender, MouseEventArgs e)
        {
            try
            {
                //opens up a file explorer
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //sets selection in file explorer to the textbox
                    textBox1.Text = dlg.SelectedPath;
                    //sets the new install path from the textbox
                    obsNewPath = textBox1.Text;
                }
            }
            catch (Exception err)
            {
                //error if dlg doesnt open.
                MessageBox.Show("somethin happened openning up the file explorer, good luck.");
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //moves the clr dll to the new obs plugin folder location while keeping the same name
                File.Move((CWD + clrdll), (textBox1.Text + clrdll));
                //moves the dir CLRHostPlugin to the new obs plugin folder locatoin while keeping same name
                Directory.Move((CWD + clrFolder), (textBox1.Text + clrFolder));
            }
            catch (FileNotFoundException)
            {
                //if CLRHostPlugin or CLRHostPlugin.dll are not found in the same dir as the exe this error will be shown
                MessageBox.Show("Exception thrown: Files not found. Please check to make sure exe is in same loaction with the clr.dll and the clr folder. Also make sure the dll is named CLRHostPlugin.dll and the folder is named CLRHostPlugin");

            }
            catch (AccessViolationException)
            {
                //if for some reason the application is not ran as admin this will be thrown (should request admin by default, check comments in app.manifest)
                MessageBox.Show("Exception thrown: Not running as administrator.");

            }
            catch (System.IO.IOException)
            {
                //if files are already in the obs plugin folder this will be thrown.
                MessageBox.Show("looks like CLR is already installed!");
            }
        }
    }
}
