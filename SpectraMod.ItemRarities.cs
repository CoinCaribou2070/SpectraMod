using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpectraMod.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI.Chat;
using static Terraria.GameContent.UI.ItemRarity;
using static Terraria.ID.Colors;
using static Terraria.Main;

namespace SpectraMod
{
    public partial class SpectraMod : Mod
    {
		public bool professionalForItemTextDontUseThisOtherwise;

		#region ITEMRARITY
		private static Dictionary<int, Color> _rarities = new Dictionary<int, Color>();
        private void ItemRarity_Initialize(On.Terraria.GameContent.UI.ItemRarity.orig_Initialize orig)
        {
			_rarities.Clear();
			_rarities.Add(-13, new AnimatedColor(Color.Red, Color.Orange).GetColor());
			_rarities.Add(-11, RarityAmber);
			_rarities.Add(-1, RarityTrash);
			_rarities.Add(1, RarityBlue);
			_rarities.Add(2, RarityGreen);
			_rarities.Add(3, RarityOrange);
			_rarities.Add(4, RarityRed);
			_rarities.Add(5, RarityPink);
			_rarities.Add(6, RarityPurple);
			_rarities.Add(7, RarityLime);
			_rarities.Add(8, RarityYellow);
			_rarities.Add(9, RarityCyan);
			Logger.Info("Rarities dictionary: " + _rarities);
		}

		private Color ItemRarity_GetColor(On.Terraria.GameContent.UI.ItemRarity.orig_GetColor orig, int rarity)
		{
			if (rarity == -13)
				return new AnimatedColor(Color.Red, Color.Yellow).GetColor(); //look I just wanna be extra sure

			Color result = new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);
			if (_rarities.ContainsKey(rarity))
			{
				return _rarities[rarity];
			}
			return result;
		}

		private void Main_MouseText(On.Terraria.Main.orig_MouseText orig, Main self, string cursorText, int rare, byte diff, int hackedMouseX, int hackedMouseY, int hackedScreenWidth, int hackedScreenHeight)
		{
			orig(self, cursorText, rare, diff, hackedMouseX, hackedMouseY, hackedScreenWidth, hackedScreenHeight);

			Color baseColor = Color.White;

			int X = Main.mouseX + 10;
			int Y = Main.mouseY + 10;

			if (hackedMouseX != -1 && hackedMouseY != -1)
			{
				X = hackedMouseX + 10;
				Y = hackedMouseY + 10;
			}

			if (Main.ThickMouse)
			{
				X += 6;
				Y += 6;
			}

			Vector2 vector = Main.fontMouseText.MeasureString(cursorText);

			if (hackedScreenHeight != -1 && hackedScreenWidth != -1)
			{
				if ((float)X + vector.X + 4f > (float)hackedScreenWidth)
				{
					X = (int)((float)hackedScreenWidth - vector.X - 4f);
				}
				if ((float)Y + vector.Y + 4f > (float)hackedScreenHeight)
				{
					Y = (int)((float)hackedScreenHeight - vector.Y - 4f);
				}
			}
			else
			{
				if ((float)X + vector.X + 4f > (float)Main.screenWidth)
				{
					X = (int)((float)Main.screenWidth - vector.X - 4f);
				}
				if ((float)Y + vector.Y + 4f > (float)Main.screenHeight)
				{
					Y = (int)((float)Main.screenHeight - vector.Y - 4f);
				}
			}

			float num = (float)(int)Main.mouseTextColor / 255f;

			if (rare == -13)
				baseColor = new AnimatedColor(Color.Red, Color.Yellow).GetColor() * num;

			if (baseColor != Color.White)
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, cursorText, new Vector2(X, Y), baseColor, 0f, Vector2.Zero, Vector2.One);
		}

		private void ItemText_NewText(On.Terraria.ItemText.orig_NewText orig, Item newItem, int stack, bool noStack, bool longText)
		{
			bool flag = newItem.type >= 71 && newItem.type <= 74;
			if (!Main.showItemText || newItem.Name == null || !newItem.active || Main.netMode == 2)
			{
				return;
			}
			for (int k = 0; k < 20; k++)
			{
				if ((!Main.itemText[k].active || (!(Main.itemText[k].name == newItem.AffixName()) && (!flag || !Main.itemText[k].coinText)) || Main.itemText[k].NoStack) | noStack)
				{
					continue;
				}
				string text3 = newItem.Name + " (" + (Main.itemText[k].stack + stack).ToString() + ")";
				string text2 = newItem.Name;
				if (Main.itemText[k].stack > 1)
				{
					text2 = text2 + " (" + Main.itemText[k].stack.ToString() + ")";
				}
				Vector2 vector2 = Main.fontMouseText.MeasureString(text2);
				vector2 = Main.fontMouseText.MeasureString(text3);
				if (Main.itemText[k].lifeTime < 0)
				{
					Main.itemText[k].scale = 1f;
				}
				if (Main.itemText[k].lifeTime < 60)
				{
					Main.itemText[k].lifeTime = 60;
				}
				if (flag && Main.itemText[k].coinText)
				{
					orig(newItem, stack, noStack, longText);
				}
				Main.itemText[k].stack += stack;
				Main.itemText[k].scale = 0f;
				Main.itemText[k].rotation = 0f;
				Main.itemText[k].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector2.X * 0.5f;
				Main.itemText[k].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector2.Y * 0.5f;
				Main.itemText[k].velocity.Y = -7f;
				if (Main.itemText[k].coinText)
				{
					Main.itemText[k].stack = 1;
				}
				return;
			}
			int num4 = -1;
			for (int j = 0; j < 20; j++)
			{
				if (!Main.itemText[j].active)
				{
					num4 = j;
					break;
				}
			}
			if (num4 == -1)
			{
				double num3 = Main.bottomWorld;
				for (int i = 0; i < 20; i++)
				{
					if (num3 > (double)Main.itemText[i].position.Y)
					{
						num4 = i;
						num3 = Main.itemText[i].position.Y;
					}
				}
			}
			if (num4 < 0)
			{
				return;
			}
			string text4 = newItem.AffixName();
			if (stack > 1)
			{
				text4 = text4 + " (" + stack.ToString() + ")";
			}
			Vector2 vector3 = Main.fontMouseText.MeasureString(text4);
			Main.itemText[num4].alpha = 1f;
			Main.itemText[num4].alphaDir = -1;
			Main.itemText[num4].active = true;
			Main.itemText[num4].scale = 0f;
			Main.itemText[num4].NoStack = noStack;
			Main.itemText[num4].rotation = 0f;
			Main.itemText[num4].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector3.X * 0.5f;
			Main.itemText[num4].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector3.Y * 0.5f;
			Main.itemText[num4].color = Color.White;
			if (newItem.rare == 1)
			{
				Main.itemText[num4].color = new Color(150, 150, 255);
			}
			else if (newItem.rare == 2)
			{
				Main.itemText[num4].color = new Color(150, 255, 150);
			}
			else if (newItem.rare == 3)
			{
				Main.itemText[num4].color = new Color(255, 200, 150);
			}
			else if (newItem.rare == 4)
			{
				Main.itemText[num4].color = new Color(255, 150, 150);
			}
			else if (newItem.rare == 5)
			{
				Main.itemText[num4].color = new Color(255, 150, 255);
			}
			else if (newItem.rare == -11)
			{
				Main.itemText[num4].color = new Color(255, 175, 0);
			}
			else if (newItem.rare == -1)
			{
				Main.itemText[num4].color = new Color(130, 130, 130);
			}
			else if (newItem.rare == 6)
			{
				Main.itemText[num4].color = new Color(210, 160, 255);
			}
			else if (newItem.rare == 7)
			{
				Main.itemText[num4].color = new Color(150, 255, 10);
			}
			else if (newItem.rare == 8)
			{
				Main.itemText[num4].color = new Color(255, 255, 10);
			}
			else if (newItem.rare == 9)
			{
				Main.itemText[num4].color = new Color(5, 200, 255);
			}
			else if (newItem.rare == 10)
			{
				Main.itemText[num4].color = new Color(255, 40, 100);
			}
			else if (newItem.rare >= 11)
			{
				Main.itemText[num4].color = new Color(180, 40, 255);
			}
			professionalForItemTextDontUseThisOtherwise = newItem.rare == -13;
			Main.itemText[num4].expert = newItem.expert;
			Main.itemText[num4].name = newItem.AffixName();
			Main.itemText[num4].stack = stack;
			Main.itemText[num4].velocity.Y = -7f;
			Main.itemText[num4].lifeTime = 60;
			if (longText)
			{
				Main.itemText[num4].lifeTime *= 5;
			}
			Main.itemText[num4].coinValue = 0;
			Main.itemText[num4].coinText = (newItem.type >= 71 && newItem.type <= 74);
			if (!Main.itemText[num4].coinText)
			{
				return;
			}
			if (newItem.type >= 71 && newItem.type <= 74)
            {
				orig(newItem, stack, noStack, longText);
			}
		}

		private void ItemText_Update(On.Terraria.ItemText.orig_Update orig, ItemText self, int whoAmI)
		{
			if (!self.active)
			{
				return;
			}
			float targetScale = ItemText.TargetScale;
			self.alpha += (float)self.alphaDir * 0.01f;
			if ((double)self.alpha <= 0.7)
			{
				self.alpha = 0.7f;
				self.alphaDir = 1;
			}
			if (self.alpha >= 1f)
			{
				self.alpha = 1f;
				self.alphaDir = -1;
			}
			if (self.expert && self.expert)
			{
				self.color = new Color((byte)Main.DiscoR, (byte)Main.DiscoG, (byte)Main.DiscoB, Main.mouseTextColor);
			}
			if (professionalForItemTextDontUseThisOtherwise)
            {
				self.color = new AnimatedColor(Color.Red, Color.Orange).GetColor();
            }
			bool flag = false;
			string text3 = self.name;
			if (self.stack > 1)
			{
				text3 = text3 + " (" + self.stack.ToString() + ")";
			}
			Vector2 vector = Main.fontMouseText.MeasureString(text3);
			vector *= self.scale;
			vector.Y *= 0.8f;
			Rectangle rectangle = new Rectangle((int)(self.position.X - vector.X / 2f), (int)(self.position.Y - vector.Y / 2f), (int)vector.X, (int)vector.Y);
			for (int i = 0; i < 20; i++)
			{
				if (!Main.itemText[i].active || i == whoAmI)
				{
					continue;
				}
				string text2 = Main.itemText[i].name;
				if (Main.itemText[i].stack > 1)
				{
					text2 = text2 + " (" + Main.itemText[i].stack.ToString() + ")";
				}
				Vector2 vector2 = Main.fontMouseText.MeasureString(text2);
				vector2 *= Main.itemText[i].scale;
				vector2.Y *= 0.8f;
				Rectangle value = new Rectangle((int)(Main.itemText[i].position.X - vector2.X / 2f), (int)(Main.itemText[i].position.Y - vector2.Y / 2f), (int)vector2.X, (int)vector2.Y);
				if (rectangle.Intersects(value) && (self.position.Y < Main.itemText[i].position.Y || (self.position.Y == Main.itemText[i].position.Y && whoAmI < i)))
				{
					flag = true;
					int num = ItemText.numActive;
					if (num > 3)
					{
						num = 3;
					}
					Main.itemText[i].lifeTime = ItemText.activeTime + 15 * num;
					self.lifeTime = ItemText.activeTime + 15 * num;
				}
			}
			if (!flag)
			{
				self.velocity.Y *= 0.86f;
				if (self.scale == targetScale)
				{
					self.velocity.Y *= 0.4f;
				}
			}
			else if (self.velocity.Y > -6f)
			{
				self.velocity.Y -= 0.2f;
			}
			else
			{
				self.velocity.Y *= 0.86f;
			}
			self.velocity.X *= 0.93f;
			self.position += self.velocity;
			self.lifeTime--;
			if (self.lifeTime <= 0)
			{
				self.scale -= 0.03f * targetScale;
				if ((double)self.scale < 0.1 * (double)targetScale)
				{
					self.active = false;
				}
				self.lifeTime = 0;
				return;
			}
			if (self.scale < targetScale)
			{
				self.scale += 0.1f * targetScale;
			}
			if (self.scale > targetScale)
			{
				self.scale = targetScale;
			}
		}
		#endregion
	}
}
