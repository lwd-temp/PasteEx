﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx
{
    public partial class FormSetting : Form
    {
        public FormSetting()
        {
            InitializeComponent();
        }

        private void Get()
        {
            txtAutoExtRule.Text = Properties.Settings.Default.autoExtRule;
            chkAutoExtSwitch.Checked = Properties.Settings.Default.autoExtSwitch;
            txtAutoExtRule.Enabled = chkAutoExtSwitch.Checked;
        }
        private void Set()
        {
            Properties.Settings.Default.autoExtRule = txtAutoExtRule.Text;
            Properties.Settings.Default.autoExtSwitch = chkAutoExtSwitch.Checked;
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            Get();
        }

        [Obsolete]
        private void btnRestore_Click(object sender, EventArgs e)
        {
            // confirmneed confirm
            Properties.Settings.Default.Reset();
            Get();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Set();

            if (!CheckRules(txtAutoExtRule.Text) && chkAutoExtSwitch.Checked)
            {
                MessageBox.Show(Resources.Resource_zh_CN.TipRulesError, Resources.Resource_zh_CN.Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAutoExtRule.Focus();
                return;
            }

            Properties.Settings.Default.Save();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Set();

            if (!CheckRules(txtAutoExtRule.Text) && chkAutoExtSwitch.Checked)
            {
                MessageBox.Show(Resources.Resource_zh_CN.TipRulesError, Resources.Resource_zh_CN.Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAutoExtRule.Focus();
                return;
            }

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RightMenu.Add();
        }

        private void btnUnRegister_Click(object sender, EventArgs e)
        {
            RightMenu.Delete();
        }

        private void chkAutoExtSwitch_CheckedChanged(object sender, EventArgs e)
        {
            txtAutoExtRule.Enabled = chkAutoExtSwitch.Checked;
        }

        private bool CheckRules(string rules)
        {
            using (StringReader sr = new StringReader(rules))
            {
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    else if (line == "")
                    {
                        continue;
                    }

                    string[] kv = line.Split('=');
                    if (kv.Length != 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}