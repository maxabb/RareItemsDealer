using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RareItemsDealer.Items {
    public class rareItemsDealerGun : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Glock");
            Tooltip.SetDefault("The pistol used by the Rare Items Dealer.");
        }

        public override void SetDefaults() {
            item.width = 15;
            item.height = 10;
            item.ranged = true;
            item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 8;
            item.autoReuse = true;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item11;
            item.useAmmo = AmmoID.Bullet;

            item.rare = 2;
            item.value = 1000; // sells for 10 silver
        }
    }
}