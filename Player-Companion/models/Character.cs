using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PlayerCompanion.models
{
    /* OUTLINE
    
    Characteristics of a Character:
    Name
    Class **
        // Maybe make this it's own Class?
        string classFeatures[]
        string skillOptions[]
    Level
    Race
        string racialFeatures[]
    Alignment // LG, NN, CE, etc.

    Health:
        int maxHealth *
        int currentHealth ***
        int hitDie *
        int maxHitDie *
        int numHitDie ***
    Proficiency Bonus *
    Speed *
    Armor Class *
    EXP ***

    Stat scores[]:
        Str
        Dex
        Con
        Int
        Wis
        Cha
    Stat Bonuses *

    Proficiencies:
        bool skillProficiencies[]
        bool saveProficiencies[]
        string weaponProficiencies[] *
        string armorProficiencies[] *
        string mscProficiencies[] // Launguages/Tools

    Spellcasting **
        //Maybe make this its own Class?
        bool isSpellcaster *
        bool hasRitual *
        bool canPrepare *
        int spellSave *
        int spellAttack *
        string Spell List // Chooseable options
        int maxSpellSlots *
        int currentSpellSlots[] ***
        string spellsKnown[][]
        string spellsPrepared[][] ***

    Feats **
        // BRAIN HURTY

    Inventory **
        Item inventory[] ***
        Item equipment[] ***
        int coinPurse[] ***


    Question: How do we handle conditional features (barbarian's rage, for example)
    Key:
    * = Derivable from pre-existing data, no prompt needed
    ** = Make its own class to inherit
    *** = Requires active tracking
    
    */

    /* FUNCTION OUTLINE
    Functions:
        Long Rest
        Short Rest
        Attack
        Skill Check X
        Take damage
        
    */

    internal class Character
    {
        #region PROPERTIES
        private string name;
        private int level;
        private IRace race;
        private IArchetype archetype;
        private string alignment;
        private int exp;

        private int[] statScores;

        private int maxHealth;
        private int currentHealth;
        private int temporaryHealth;
        private int hitDie;
        private int numHitDie;

        private int proficiencyBonus;
        private int speed;
        private int size;

        private bool[] skillProficiencies;
        private bool[] saveProficiencies;
        private string[] weaponProficiencies;
        private string[] armorProficiencies;
        private string[] mscProficiencies;

        private List<Object> inventory;
        private int[] coinPurse;
        private Armor armor;
        #endregion

        #region CONSTRUCTOR
        public Character (string name, int race, int archetype, string alignment, int[] statScores,
            bool[] skillProficiencies, bool[] saveProficiencies, string[] mscProficiencies)
        {
            this.name = name;
            this.level = 1;
            this.race = new IRace(race);
            this.alignment = alignment;
            this.exp = 0;

            switch (archetype)
            {
                case 0:
                    this.archetype = new Artificer();
                    break;
                case 1:
                    this.archetype = new Barbarian();
                    break;
                case 2:
                    this.archetype = new Bard();
                    break;
                case 3:
                    this.archetype = new Cleric();
                    break;
                case 4:
                    this.archetype = new Fighter();
                    break;
                case 5:
                    this.archetype = new Monk();
                    break;
                case 6:
                    this.archetype = new Paladin();
                    break;
                case 7:
                    this.archetype = new Ranger();
                    break;
                case 8:
                    this.archetype = new Rouge();
                    break;
                case 9:
                    this.archetype = new Sorcerer();
                    break;
                case 10:
                    this.archetype = new Wizard();
                    break;
                case 11:
                    this.archetype = new Warlock();
                    break;
            }

            this.statScores = statScores;

            this.maxHealth = hitDie + this.statScores[2];
            this.currentHealth = maxHealth;
            this.temporaryHealth = 0;
            this.hitDie = archetype.getHitDie(); //CHANGE WHEN DEFINED
            this.numHitDie = 1;

            this.speed = race.getSpeed(); //CHANGE WHEN DEFINED
            this.size = race.getSize(); //CHANGE WHEN DEFINED

            this.proficiencyBonus = 2;
            this.skillProficiencies = skillProficiencies;
            this.saveProficiencies = archetype.getSaveProficiencies(); //CHANGE WHEN DEFINED
            this.weaponProficiencies = archetype.getWeaponProficiencies(); //CHANGE WHEN DEFINED
            this.armorProficiencies = archetype.getArmorProficiencies(); //CHANGE WHEN DEFINED
            this.mscProficiencies = mscProficiencies;

            this.inventory = new List<Object> { };
            this.coinPurse = new int[] { 0, 0, 0, 0, 0 };
            this.armor = new Armor("natural armor", 0, 0, "Your natrual resillience against attacks. This is most commonly represented by your ability to dodge attacks.", 10, 0, 20, 0 );
        }
        #endregion

        #region UTILITIES
        public int GetStatBonus(int statScore)
        {
            return statScore / 2 - 5;
        }
        
        public int GetNaturalArmor()
        {
            // HANDLE NATURAL ARMOR FROM SELECT RACES AND CLASSES
            int natAC;
            natAC = 10 + GetStatBonus(1);
            return natAC;
        }
        #endregion

        #region METHODS
        public void TakeDamage (int damage)
        {
            this.temporaryHealth -= damage;
            int caryOver = 0;
            if (this.temporaryHealth < 0)
            {
                caryOver = temporaryHealth;
                this.temporaryHealth = 0;
            }
            this.currentHealth += caryOver;

            // +++++++ HANDLE DAMAGE REDUCTION EFFECTS LIKE RESISTANCE +++++++
        }
                // ^ UNFINISHED, SEE NOTE INSIDE ^

        public int SkillCheck (int skill, out int roll)
        {
            // Simulates a skill check
            // Takes a skill option as an integer ranging from 0-17 and returns the total result for
            // a check and outs the roll
            Random randy = new Random();
            roll = randy.Next(1, 21);
            int totalBonus = roll;
            if (skill == 3)
            {
                totalBonus += GetStatBonus(this.statScores[0]);
            } else if (skill == 0 || skill == 15 || skill == 16)
            {
                totalBonus += GetStatBonus(this.statScores[1]);
            } else if (skill == 1 || skill == 6 || skill == 9 || skill == 11 || skill == 17) 
            {
                totalBonus += GetStatBonus(this.statScores[3]);
            } else if (skill == 2 || skill == 5 || skill == 8 || skill == 10 || skill == 14)
            {
                totalBonus += GetStatBonus(this.statScores[4]);
            } else
            {
                totalBonus += GetStatBonus(this.statScores[5]);
            }

            if (skillProficiencies[skill]) totalBonus += proficiencyBonus;

            // ++++++++ IMPLEMENT CLASS BONUSES ONCE ESTABLISHED +++++++++

            return totalBonus;
        }
                // ^ UNFINISHED, SEE NOTE INSIDE ^

        public int MscCheck(int stat, string skill, out int roll)
        {
            Random randy = new Random();
            roll = randy.Next(20)+1;
            int totalBonus = roll + GetStatBonus(stat);
            if (mscProficiencies.Contains(skill)) totalBonus += proficiencyBonus;

            // ++++++++ IMPLEMENT CLASS BONUSES ONCE ESTABLISHED +++++++++

            return totalBonus;
        }
                // ^ UNFINISHED, SEE NOTE INSIDE ^

        public int SavingThrow (int save, out int roll)
        {

            Random randy = new Random();
            roll = randy.Next(20)+1;
            int totalBonus = GetStatBonus(save) + roll;

            if (this.saveProficiencies[save] == true) totalBonus += proficiencyBonus;

            // ++++++++ IMPLEMENT CLASS BONUSES ONCE ESTABLISHED +++++++++

            return totalBonus;
        }
                // ^ UNFINISHED, SEE NOTE INSIDE ^

        public void LevelUp(bool randomHealth)
        {
            //Levels up the character
            if (this.level < 20)
            {
                this.level++;
                switch (this.level)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        proficiencyBonus = 2;
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        proficiencyBonus = 3;
                        break;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        proficiencyBonus = 4;
                        break;
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        proficiencyBonus = 5;
                        break;
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                        proficiencyBonus = 6;
                        break;
                }
                this.numHitDie++;
                Random randy = new Random();
                this.maxHealth += GetStatBonus(2);
                this.maxHealth += randomHealth ? randy.Next(hitDie)+1 : this.hitDie / 2 + 1;

                this.currentHealth = this.maxHealth;
                archetype.LevelUp();
            }


            // ++++++++ NEED TO IMPLEMENT MULTICLASSING ++++++++
        }
                // ^ UNFINISHED, SEE NOTE INSIDE ^

        public void AbilityScoreImprovement(int stat)
        {
            statScores[stat] += 2;
            switch (stat)
            {
                case 2:
                    if (statScores[2] % 2 == 0) this.maxHealth += level;
                    break;
            }
        }

        public void AbilityScoreImprovement(int stat1, int stat2)
        {

        }
        #endregion
    }
}
