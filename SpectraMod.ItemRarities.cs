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

		private void LoadItemRare()
        {
			On.Terraria.GameContent.UI.ItemRarity.Initialize += ItemRarity_Initialize;
			On.Terraria.GameContent.UI.ItemRarity.GetColor += ItemRarity_GetColor;
			On.Terraria.Main.MouseText += Main_MouseText;
			On.Terraria.ItemText.NewText += ItemText_NewText;
			On.Terraria.ItemText.Update += ItemText_Update;

			ItemRarity.Initialize();
		}

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
			if (newItem.rare != -13)
            {
				orig(newItem, stack, noStack, longText);
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

			if (num4 < 0)
			{
				return;
			}

			if (newItem.rare == -13)
            {
				professionalForItemTextDontUseThisOtherwise = true;
				Main.itemText[num4].color = new AnimatedColor(Color.Red, Color.Yellow).GetColor();
            }

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
		}

		private void ItemText_Update(On.Terraria.ItemText.orig_Update orig, ItemText self, int whoAmI)
		{
			orig(self, whoAmI);

			Item item = new Item();

			if (item.rare == -13 || professionalForItemTextDontUseThisOtherwise)
				self.color = new AnimatedColor(Color.Red, Color.Yellow).GetColor();
		}
		#endregion
	}
}
