using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RareItemsDealer.NPCS {
    [AutoloadHead] // head will show in map
    public class rareItemDealer : ModNPC {
        public override string Texture => "RareItemsDealer/NPCS/rareItemDealer"; // Texture to load

        public override bool Autoload(ref string name) {
            name = "Rare Items Dealer"; // npc name
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults() {
            Main.npcFrameCount[npc.type] = 25; // ammount of frames in npc
            NPCID.Sets.ExtraFramesCount[npc.type] = 5; // ammount of additional frames TODO get details
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
            // NPC will only be able to spawn if King Slime has been defeated
            if (NPC.downedSlimeKing) {
                return true;
            }
            return false;
        }

        public override string TownNPCName() {
            // Pick Random Name
            switch(Main.rand.Next(10)) {
                case 0:
                    return "Casey";
                case 1:
                    return "Alexis";
                case 2:
                    return "Phoenix";
                case 3:
                    return "Billie";
                case 4:
                    return "Riley";
                case 5:
                    return "Jordan";
                case 6:
                    return "Harley";
                case 7:
                    return "Ellis";
                case 8:
                    return "Elliot";
                case 9:
                    return "Taylor";
                default:
                    return "Russel";
            }
        }

        public override void NPCLoot() {
            Item.NewItem(npc.getRect(), mod.ItemType("rareItemsDealerGun")); // will drop this on death
        }

        public override string GetChat() {
            // Dialog to only use if there is a tax collector
            int taxCollector = NPC.FindFirstNPC(NPCID.TaxCollector);
            if (taxCollector >= 0 && Main.rand.NextBool(7)) {
                return "Don't tell " + Main.npc[taxCollector].GivenName + " I live here.";
            }

            switch(Main.rand.Next(7)) { // get a random value
                case 0: // if it is 0
                    return "Don't ask where I got these."; // say this
                case 1:
                    return "If you ever find yourself in the middle of a Columbian maximum security prison, don't trust Steve. You'll find yourself in the shower surrounded by Micky Mouse cosplayers, covered in blood and coughing up cement.";
                case 2:
                    return "No, I don't sell vapes. Stop asking that";
                case 3:
                    return "Thanks for letting me move in here. I'm pretty sure I'm wanted in every other country";
                case 4:
                    return "I'd be selling other stuff as well, but this game's only PG-13";
                case 5:
                    return "I also have a website, but you have to be pretty tech savvy to get there";
                default: { // if it isnt any of the above cases
                    return "Oh you're from Columbia? I stayed at a prison there once."; // say this
                }
            }
        }

        public override void SetChatButtons(ref string button, ref string button2) {
            button = Language.GetTextValue("LegacyInterface.28"); // shop button
            button2 = "Give Life Advice"; // This is how you make custom buttons you can give them funciton in OnChatButtonClicked()
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
            if(firstButton) {
                // If user clicks the shop button it will open the shop
                shop = true;
            } else {
                // Life Advice
                Main.npcChatText = "If you ever find yourself in the middle of a Columbian maximum security prison, don't trust Steve. You'll find yourself in the shower surrounded by Micky Mouse cosplayers, covered in blood and coughing up cement.";
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot) {
            // Sell Slime Staff
            shop.item[nextSlot].SetDefaults(ItemID.SlimeStaff);
            nextSlot++;

            // Sell Hermes Boots
            shop.item[nextSlot].SetDefaults(ItemID.HermesBoots);

            // Sell Pink Gel
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.PinkGel);

            // post EoC
            if (NPC.downedBoss1) {
                // Sell Binoculars
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Binoculars);
            }

            // post QueenBee
            if (NPC.downedQueenBee) {
                // Sell Honeyed Goggles
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.HoneyedGoggles);
                
                // Sell Jungle Rose
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.JungleRose);

                // Sell Natures Gift
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.NaturesGift);
            }

            // post Skeletron
            if (NPC.downedBoss3) {
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.LuckyHorseshoe);
            }

            // post WoF
            if (Main.hardMode) {
                // Sell Nimbus Rod
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.NimbusRod);
            }

            // post Mech-bosses
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) {
                // Sell Rod of Discord
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.RodofDiscord);

                // Sell Uzi
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Uzi);
            }

        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
            damage = 25; // base damage
            knockback = 4f; // knockBack
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
            cooldown = 10; // cooldown after each attack
            randExtraCooldown = 10; // a random value from 0-10 will be added to cooldown
        }

        public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness) {
            scale = 0.7f; // item is only 70% of normal size
            item = mod.ItemType("rareItemsDealerGun"); // Will hold the Glock
            closeness = 10; // how close the item is to the NPCS body
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
            projType = ProjectileID.Bullet; // shoots regular bullets
            attackDelay = 1; // slight delay per shot
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)//Allows you to determine the speed at which this town NPC throws a projectile when it attacks. Multiplier is the speed of the projectile, gravityCorrection is how much extra the projectile gets thrown upwards, and randomOffset allows you to randomize the projectile's velocity in a square centered around the original velocity
		{
			multiplier = 7f; // bullet velocity 7x greater
		}
    }
}