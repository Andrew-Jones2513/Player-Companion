using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerCompanion.models
{
    internal class Item
    {
        //Properties
        private string name;
        private int weight;
        private int price;
        private string description;
        private string identifier;

        //Constructor
        public Item(string name, int weight, int price, string description, string identifier)
        {
            this.name = name;
            this.weight = weight;
            this.price = price;
            this.description = description;
            this.identifier = identifier;
        }

        //Accessors
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Weight
        {
            get { return weight; }
            set { if (value >= 0) { weight = value; } else weight = 0; }
        }
        public int Price
        {
            get { return price; }
            set { if (value >= 0) { price = value; } else price = 0; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }
    }

    internal class Weapon : Item
    {
        //Properties
        private int dmgNum;
        private int dmgDie;
        private int dmgType;
        private int quality;
        private string[] props;

        //Constructor
        public Weapon(string name, int weight, int price, string description, string identifier, int dmgNum, int dmgDie, int dmgType, string[] props) : base(name, weight, price, description, identifier)
        {
            this.dmgNum = dmgNum;
            this.dmgDie = dmgDie;
            this.dmgType = dmgType;
            this.quality = 0;
            this.props = props;
        }

        //Accessors
        public int DmgNum
        {
            get { return dmgNum; }
            set { if (value >= 0) dmgNum = value; }
        }
        public int DmgDie
        {
            get { return dmgDie; }
            set { if (value == 1 || (value % 2 == 0 && value <= 12) || value == 20) dmgDie = value; }
        }
        public int DmgType
        {
            get { return dmgType; }
            set { if (value >= 0 && value < 13) dmgType = value; }
        }
        public int Quality
        {
            get { return quality; }
            private set { quality = value; }
        }
        public string[] Props
        {
            get { return props; }
            set { props = value; }
        }

        //Methods
        public string getDmgType()
        {
            // Converts the damage type to a string
            string type = "";
            switch (this.dmgType)
            {
                case 0:
                    type = "slashing"; break;
                case 1:
                    type = "bludgeoning"; break;
                case 2:
                    type = "piercing"; break;
                case 3:
                    type = "poison"; break;
                case 4:
                    type = "acid"; break;
                case 5:
                    type = "fire"; break;
                case 6:
                    type = "cold"; break;
                case 7:
                    type = "radiant"; break;
                case 8:
                    type = "necrotic"; break;
                case 9:
                    type = "lightning"; break;
                case 10:
                    type = "thunder"; break;
                case 11:
                    type = "force"; break;
                case 12:
                    type = "psychic"; break;
            }
            return type;
        }

        public string getAttack()
        {
            // Returns a string representing the damage of the weapon
            return $"{this.dmgNum}d{this.dmgDie}";
        }
        public int attack(int bonus, int target)
        {
            // Simulates the attack with a given weapon, potentially move this to the Character class
            Random randy = new Random();
            return (randy.Next(20) + 1 + bonus + this.quality);
        }
        public int[] dealDamage()
        {
            // Simulates the dice roll of a weapon, does not return the actual total damage delt
            Random randy = new Random();
            int[] rolls = new int[dmgNum];
            for (int i = 0; i < this.dmgNum; i++) { rolls[i] = randy.Next(dmgDie) + 1; }
            return rolls;
        }
        public void upgrade()
        {
            quality++;
        }
        public bool isMartial()
        { return props.Contains("martial"); }
        public bool isRanged()
        {
            return this.props.Contains("ranged");
        }
    }

    internal class Armor : Item
    {
        //Properties
        private int armorClass;
        private int strRequirement;
        private int dexMax;
        private int armorType;

        //Constructor
        public Armor(string name, int weight, int price, string description, string identifier, int armorClass, int strRequirement, int dexMax, int armorType) : base(name, weight, price, description, identifier)
        {
            this.armorClass = armorClass;
            this.strRequirement = strRequirement;
            this.dexMax = dexMax;
            this.armorType = armorType;
        }

        //Accessors
        public int ArmorClass
        {
            get { return this.armorClass; }
            set { if (value >= 10) this.armorClass = value; }
        }
        public int StrRequirement
        {
            get { return this.strRequirement; }
            set { if (value >= 13) this.strRequirement = value; }
        }
        public int DexMax
        {
            get { return this.dexMax; }
            set { if (value >= 0 && value <= 10) this.dexMax = value; }
        }
        public int ArmorType
        {
            get { return this.armorType; }
            set { if (value >= 0 && value <= 3) this.armorType = value; }
        }

        //Methods
        public int getAC(int dex, int bonus, bool hasShield)
        {
            // Returns the adjusted Armor Class of a character based on the armor, bonus, and if they
            // have a shield, possibly move to the Character class
            return (this.armorClass + Math.Min(dex, this.dexMax) + bonus + (hasShield ? 2 : 0));
        }
        public string getArmorType()
        {
            // Returns a string representing the armor class
            string type = "";
            switch (this.ArmorType)
            {
                case 0:
                    type = "light"; break;
                case 1:
                    type = "medium"; break;
                case 2:
                    type = "heavy"; break;
            }
            return type;
        }
        public bool strMatch(int strBonus)
        {
            // Checks to see if the character has the required strength to equip the armor
            return strRequirement <= strBonus;
        }
    }
}
