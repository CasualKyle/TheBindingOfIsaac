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
using System.Xml;

using System.Diagnostics;

namespace MapConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fileDirButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                fileDirTextBox.Text = openFileDialog.FileName;
        }

        private void outputDirButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                outputDirTextBox.Text = saveFileDialog.FileName;
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;

            XmlReader xmlReader = XmlReader.Create(fileDirTextBox.Text);
            FileStream mapFile = new FileStream(outputDirTextBox.Text, FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(mapFile);

            const int width = 15;
            const int height = 11;
            int[] startGids = new int[3]; //[background, foreground, tileTypes]

            xmlReader.ReadToNextSibling("map");

            xmlReader.ReadToFollowing("tileset");
            startGids[0] = Convert.ToInt32(xmlReader.GetAttribute("firstgid"));
            xmlReader.ReadToNextSibling("tileset");
            startGids[1] = Convert.ToInt32(xmlReader.GetAttribute("firstgid"));
            xmlReader.ReadToNextSibling("tileset");
            startGids[2] = Convert.ToInt32(xmlReader.GetAttribute("firstgid"));

            for (int i = 0; i < 3; i++)
            {
                xmlReader.ReadToFollowing("tile");

                for (int j = 0; j < width * height; j++)
                {
                    byte tile = (byte)Convert.ToInt32(xmlReader.GetAttribute("gid"));
                    writer.Write(tile == 0 ? (byte)0 : (byte)(tile - startGids[i] + 1));
                    xmlReader.ReadToNextSibling("tile");
                }
            }

			uint entityCount = 0;

			if (xmlReader.ReadToFollowing("objectgroup"))
			{
				xmlReader.ReadToFollowing("property");
				entityCount = (uint)Convert.ToInt32(xmlReader.GetAttribute("value"));
				writer.Write(entityCount);

				for(int i = 0; i < entityCount; i++)
				{
					xmlReader.ReadToFollowing("object");

					uint x = (uint)Convert.ToInt32(xmlReader.GetAttribute("x").Split('.')[0]);
					uint y = (uint)Convert.ToInt32(xmlReader.GetAttribute("y").Split('.')[0]);
					uint sizeX = (uint)Convert.ToInt32(xmlReader.GetAttribute("width"));
					uint sizeY = (uint)Convert.ToInt32(xmlReader.GetAttribute("height"));

					writer.Write(x + sizeX / 2);
					writer.Write(y + sizeY / 2);

					switch (xmlReader.GetAttribute("name"))
					{
						case "bouncer":
							writer.Write((byte)0);
                            break;
						case "direct_jimmy":
							writer.Write((byte)1);
							break;
						case "round_jimmy":
							writer.Write((byte)2);
							break;
						case "attack_fly":
							writer.Write((byte)3);
							break;
						case "bomb_fly":
							writer.Write((byte)4);
							break;
						case "daddy_fly":
							writer.Write((byte)5);
							break;
						case "attack_fly_spawner":
							writer.Write((byte)6);
							break;
						case "bomb_fly_spawner":
							writer.Write((byte)7);
							break;
						case "random_dashie":
							writer.Write((byte)8);
							break;
						case "direct_dashie":
							writer.Write((byte)9);
							break;
						case "spinny":
							writer.Write((byte)10);
							break;
						case "bomb_spinny":
							writer.Write((byte)11);
							break;
						case "walker":
							writer.Write((byte)12);
							break;
						case "round_shot_walker":
							writer.Write((byte)13);
							break;
						case "fast_walker":
							writer.Write((byte)14);
							break;
					}

				}
			}
			else
			{
				writer.Write(entityCount);
			}

            writer.Close();
            mapFile.Close();
            xmlReader.Close();

            panel.Enabled = true;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
