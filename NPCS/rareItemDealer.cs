using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RareItemsDealer.NPCS {
    [AutoloadHead]
    public class rareItemDealer : ModNPC {
        public override string Texture => "RareItemsDealer/NPCS/rareItemDealer"; // Load Default Texture

        public override bool Autoload(ref string name) {
            name = "Rare Items Dealer"; // npc name
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults() {
            Main.npcFrameCount[npc.type] = 25; // ammount of frames in npc
            NPCID.Sets.ExtraFramesCount[npc.type] = 9; // ammount of additional frames TODO get details
            NPCID.Sets.AttackFrameCount[npc.type] = 4; // ammount of frames used for attacking
            NPCID.Sets.DangerDetectRange[npc.type] = 700; // range npc looks for enemies
            NPCID.Sets.AttackType[npc.type] = 1; // 0 = throw, 1 = fire, 2 = magic
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30; // how often they attack
            NPCID.Sets.HatOffsetY[npc.type] = 4; // when party hat is worn it will be offset by 4 pixels
        }

        public override void SetDefaults() {
            npc.townNPC = true; // REQUIRED
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7; // Town NPC AI Style
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250; // ammount of health
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) {
            if (NPC.downedSlimeKing) {
                return true;
            }
            return false;
        }

        public override string TownNPCName() {
            return "qwertyuiop";
        }

        public override void NPCLoot() {
            Item.NewItem(npc.getRect(), mod.ItemType("rareItemsDealerGun"));
        }

        public override string GetChat() {
            switch(Main.rand.Next(2)) {
                case 0:
                    return "Don't ask where I got these.";
                default: {
                    return "Oh you're from Columbia? I stayed at a prison there once.";
                }
            }
        }

        public override void SetChatButtons(ref string button, ref string button2) {
            button = Language.GetTextValue("LegacyInterface.28"); // shop button
            button2 = "Give Life Advice"; //This is how you make custom buttons you can give them funciton in OnChatButtonClicked()
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
            if(firstButton) {
                shop = true;
            } else {
                Main.npcChatText = "If you ever find yourself in the middle of a Columbian maximum security prison, don't trust Steve. You'll find yourself in the shower surrounded by Micky Mouse cosplayers, covered in blood and coughing up cement.";
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot) {
            shop.item[nextSlot].SetDefaults(ItemID.SlimeStaff);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.JungleRose);
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
            damage = 25;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
            cooldown = 10;
            randExtraCooldown = 10;
        }

        public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness) {
            scale = 0.6f;
            item = mod.ItemType("rareItemsDealerGun");
            closeness = 10;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
            projType = ProjectileID.Bullet;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)//Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity
		{
			multiplier = 7f;
		}
    }
}