using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Drawing;
// using System.Drawing.Common;
using System.IO;
using System.Runtime.InteropServices;

public class Screenshot : MonoBehaviour
{
	// public UnityEngine.UI.Image DesktopImage;

	// void Start()
	// {
	// 	StartCoroutine(Desktop());
	// }

	// private IEnumerator Desktop()
	// {
	// 	IntPtr desktopHwnd = FindWindowEx(GetDesktopWindow(), IntPtr.Zero, "Progman", "Program Manager");

	// 	// get the desktop dimensions
	// 	var rect = new Rectangle();
	// 	GetWindowRect(desktopHwnd, ref rect);

	// 	// saving the screenshot to a bitmap
	// 	var bmp = new Bitmap(rect.Width, rect.Height);
	// 	System.Drawing.Graphics memoryGraphics = System.Drawing.Graphics.FromImage(bmp);
	// 	IntPtr dc = memoryGraphics.GetHdc();
	// 	PrintWindow(desktopHwnd, dc, 0);
	// 	memoryGraphics.ReleaseHdc(dc);
		
	// 	/*string path = String.Concat(Application.dataPath, "/desktop.png");
	// 	bmp.Save(path, ImageFormat.Png);*/

	// 	yield return null;

	// 	Texture2D tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
	// 	MemoryStream ms = new MemoryStream();
	// 	bmp.Save(ms, bmp.RawFormat);
	// 	tex.LoadImage(ms.ToArray());

	// 	Sprite sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(.5f,.5f));
	// 	DesktopImage.sprite = sprite;
	// }







	// [DllImport("User32.dll", SetLastError = true)]
	// [return: MarshalAs(UnmanagedType.Bool)]
	// static extern bool PrintWindow(IntPtr hwnd, IntPtr hdc, uint nFlags);

	// [DllImport("user32.dll")]
	// static extern bool GetWindowRect(IntPtr handle, ref Rectangle rect);

	// [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
	// static extern IntPtr GetDesktopWindow();

	// [DllImport("user32.dll", CharSet = CharSet.Unicode)]
	// static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);
}