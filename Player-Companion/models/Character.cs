using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerCompanion.models
{
    /*
    
    Characteristics of a Character:
    Name
    Class **
        // Maybe make this it's own Class?
        string classFeatures[] **
        string skillOptions[] **
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

    Stat scores:
        Str
        Dex
        Con
        Int
        Wis
        Cha
    Stat Bonuses *

    Proficiencies:
        string skillProficiencies[]
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

    /*
    Functions:
        Long Rest
        Short Rest
        Attack
        Skill Check
        Take damage
        
    */

    abstract internal class Character
    {
        private string name;
        private int level;
        private int race;
        private string alignment;
        private int exp;

        private int maxHealth;
        private int hitDie;
        private int numHitDie;

        private int proficiencyBonus;
        private int speed;
        private int armorClass;

        private int strScore;
        private int dexScore;
        private int conScore;
        private int intScore;
        private int wisScore;
        private int chaScore;

        private bool[] skillProficiencies;
        private bool[] saveProficiencies;
        private string[] weaponProficiencies;
        private string[] armorProficiencies;
        private string[] mscProficiencies;

        public Character (string name, int race, int archetype, string alignment, 
            int strScore, int dexScore, int conScore, int intScore, int wisScore, int chaScore,
            bool[] skillProficiencies, bool[] saveProficiencies, string[] weaponProficiencies, string[] armorProficiencies, string[] mscProperties)
        {

        }
    }
}
