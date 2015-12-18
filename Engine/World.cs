using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class World
    {
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Monster> Monsters = new List<Monster>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Location> Locations = new List<Location>();

        public const int ITEM_ID_PHOTON_CANNON = 1;
        public const int ITEM_ID_METAL_SCRAP = 2;
        public const int ITEM_ID_PARTS = 3;
        public const int ITEM_ID_ENERGY_CELL = 4;
        public const int ITEM_ID_WIRES = 5;
        public const int ITEM_ID_ROCKETS = 6;
        public const int ITEM_ID_REPAIR_KIT = 7;
        public const int ITEM_ID_ADHESIVE = 8;
        public const int ITEM_ID_SCRAP = 9;
        public const int ITEM_ID_WARP_CORE = 10;

        public const int MONSTER_ID_GORGON = 1;
        public const int MONSTER_ID_YAITH = 2;
        public const int MONSTER_ID_LOZAL = 3;

        public const int QUEST_ID_CLEAR_ASTEROID_FIELD = 1;
        public const int QUEST_ID_CLEAR_UNKNOWN_SECTOR = 2;

        public const int LOCATION_ID_HOME = 1;
        public const int LOCATION_ID_DEZO = 2;
        public const int LOCATION_ID_Outpost = 3;
        public const int LOCATION_ID_ASTEROID_BELT = 4;
        public const int LOCATION_ID_ASTEROID_FIELD = 5;
        public const int LOCATION_ID_UNKNOWN_PLANET = 6;
        public const int LOCATION_ID_UNKNOWN_SECTOR = 7;
        public const int LOCATION_ID_BRIDGE = 8;
        public const int LOCATION_ID_RED_ZONE = 9;

        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateLocations();
        }

        private static void PopulateItems()
        {
            Items.Add(new Weapon(ITEM_ID_PHOTON_CANNON, "Photon Cannon", "Photon Cannons", 0, 5));
            Items.Add(new Item(ITEM_ID_METAL_SCRAP, "Metal Scrap", "Metal Scraps"));
            Items.Add(new Item(ITEM_ID_PARTS, "Part", "Parts"));
            Items.Add(new Item(ITEM_ID_ENERGY_CELL, "Energy Cell", "Energy Cells"));
            Items.Add(new Item(ITEM_ID_WIRES, "Wire", "Wires"));
            Items.Add(new Weapon(ITEM_ID_ROCKETS, "Rocket", "Rockets", 3, 10));
            Items.Add(new HealingPotion(ITEM_ID_REPAIR_KIT, "Repair Kit", "Repair Kits", 5));
            Items.Add(new Item(ITEM_ID_ADHESIVE, "Adhesive", "Adhesives"));
            Items.Add(new Item(ITEM_ID_SCRAP, "Scrap", "Scraps"));
            Items.Add(new Item(ITEM_ID_WARP_CORE, "Warp Core", "Warp Cores"));
        }

        private static void PopulateMonsters()
        {
            Monster Gorgon = new Monster(MONSTER_ID_GORGON, "GORGON", 5, 3, 10, 3, 3);
            Gorgon.LootTable.Add(new LootItem(ItemByID(ITEM_ID_METAL_SCRAP), 75, false));
            Gorgon.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PARTS), 75, true));

            Monster Yaith = new Monster(MONSTER_ID_YAITH, "YAITH", 5, 3, 10, 3, 3);
            Yaith.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ENERGY_CELL), 75, false));
            Yaith.LootTable.Add(new LootItem(ItemByID(ITEM_ID_WIRES), 75, true));

            Monster Lozal = new Monster(MONSTER_ID_LOZAL, "LOZAL", 20, 5, 40, 10, 10);
            Lozal.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ADHESIVE), 75, true));
            Lozal.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SCRAP), 25, false));

            Monsters.Add(Gorgon);
            Monsters.Add(Yaith);
            Monsters.Add(Lozal);
        }

        private static void PopulateQuests()
        {
            Quest clearAsteroidField =
                new Quest(
                    QUEST_ID_CLEAR_ASTEROID_FIELD,
                    "Clear the Asteroid field",
                    "Kill the Gorgon hiding in the asteroid field and bring back 3 metal scrap. You will receive a repair kit and 10 edo pieces.", 20, 10);

            clearAsteroidField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_METAL_SCRAP), 3));

            clearAsteroidField.RewardItem = ItemByID(ITEM_ID_REPAIR_KIT);

            Quest clearUnknownSector =
                new Quest(
                    QUEST_ID_CLEAR_UNKNOWN_SECTOR,
                    "Clear the unknown sector",
                    "Kill Yaith in the unmapped sector and bring back 3 energy cells. You will receive a warp core and 20 edo pieces.", 20, 20);

            clearUnknownSector.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_ENERGY_CELL), 3));

            clearUnknownSector.RewardItem = ItemByID(ITEM_ID_WARP_CORE);

            Quests.Add(clearUnknownSector);
            Quests.Add(clearUnknownSector);
        }

        private static void PopulateLocations()
        {

            Location home = new Location(LOCATION_ID_HOME, "Daizar", "This is your home planet where you are from");

            Location Dezo = new Location(LOCATION_ID_DEZO, "Dezo Nebula", "You see your solar system");

            Location AsteroidBelt = new Location(LOCATION_ID_ASTEROID_BELT, "Asteroid Belt", "There is a uniform belt, but some astroids are flying off course.");
            AsteroidBelt.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ASTEROID_FIELD);

            Location AsteroidField = new Location(LOCATION_ID_ASTEROID_FIELD, "Asteroid Field", "Astroids have just littered this area.");
            AsteroidField.MonsterLivingHere = MonsterByID(MONSTER_ID_GORGON);

            Location UnknownPlanet = new Location(LOCATION_ID_UNKNOWN_PLANET, "Unknown Planet", "There are life forms detected on this uncharted planet.");
            UnknownPlanet.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_UNKNOWN_SECTOR);

            Location UnknownSector = new Location(LOCATION_ID_UNKNOWN_SECTOR, "Unknown Sector", "This part of space is completely unmapped.");
            UnknownSector.MonsterLivingHere = MonsterByID(MONSTER_ID_YAITH);

            Location Outpost = new Location(LOCATION_ID_Outpost, "Outpost", "There are a lot of imperial guards here.", ItemByID(ITEM_ID_WARP_CORE));

            Location blackhole = new Location(LOCATION_ID_BRIDGE, "Blackhole", "A black hole that joins galaxies.");

            Location RedZone = new Location(LOCATION_ID_RED_ZONE, "Red Zone", "You see ship parts floating everywhere.");
            RedZone.MonsterLivingHere = MonsterByID(MONSTER_ID_LOZAL);


            home.LocationToNorth = Dezo;

            Dezo.LocationToNorth = AsteroidBelt;
            Dezo.LocationToSouth = home;
            Dezo.LocationToEast = Outpost;
            Dezo.LocationToWest = UnknownPlanet;

            UnknownPlanet.LocationToEast = Dezo;
            UnknownPlanet.LocationToWest = UnknownSector;

            UnknownSector.LocationToEast = UnknownPlanet;

            AsteroidBelt.LocationToSouth = Dezo;
            AsteroidBelt.LocationToNorth = AsteroidField;

            AsteroidField.LocationToSouth = AsteroidBelt;

            Outpost.LocationToEast = blackhole;
            Outpost.LocationToWest = Dezo;

            blackhole.LocationToWest = Outpost;
            blackhole.LocationToEast = RedZone;

            RedZone.LocationToWest = blackhole;


            Locations.Add(home);
            Locations.Add(Dezo);
            Locations.Add(Outpost);
            Locations.Add(AsteroidBelt);
            Locations.Add(AsteroidField);
            Locations.Add(UnknownPlanet);
            Locations.Add(UnknownSector);
            Locations.Add(blackhole);
            Locations.Add(RedZone);
        }

        public static Item ItemByID(int id)
        {
            foreach (Item item in Items)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }

            return null;
        }

        public static Monster MonsterByID(int id)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.ID == id)
                {
                    return monster;
                }
            }

            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach (Quest quest in Quests)
            {
                if (quest.ID == id)
                {
                    return quest;
                }
            }

            return null;
        }

        public static Location LocationByID(int id)
        {
            foreach (Location location in Locations)
            {
                if (location.ID == id)
                {
                    return location;
                }
            }

            return null;
        }
    }
}
