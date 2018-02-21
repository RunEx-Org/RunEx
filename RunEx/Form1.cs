using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace RunEx
{
	public partial class RunEx : Form
	{
		public RunEx()
		{
			InitializeComponent();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("よござんすか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
			{
				return;
			}
			var regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths", true);

			var scriptPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RunEx\Script\";

			foreach (var n in Directory.EnumerateFiles(scriptPath).Where(s => s.Contains(".js")))
			{
				var name = Path.GetFileNameWithoutExtension(n);

				var r = regKey.OpenSubKey(name + ".exe");

				if (r != null && r.Name == (name + ".exe"))
				{
					var mes = "元:" + r.GetValue(null) + "\n新:" + n + "\n更新してもよろしいですか？";
					if (MessageBox.Show(mes, "警告", MessageBoxButtons.YesNo) != DialogResult.No)
					{
						continue;
					}
				}
				var sk = regKey.CreateSubKey(name + ".exe", true);
				sk.SetValue(null, n);
			}

		}
	}
}
