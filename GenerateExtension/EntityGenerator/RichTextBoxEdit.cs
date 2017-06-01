using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EntityGenerator
{
	public class RichTextBoxEdit : RichTextBox
	{
		private void ChangeColor(string text, Color color)
		{
			int start = 0;
			while (-1 + text.Length - 1 != (start = text.Length - 1 + base.Find(text, start, -1, RichTextBoxFinds.WholeWord)))
			{
				base.SelectionColor = color;
				base.SelectionFont = new Font(base.SelectionFont.FontFamily, base.SelectionFont.Size, FontStyle.Bold);
			}
		}

		[DllImport("user32")]
		private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
	}
}
