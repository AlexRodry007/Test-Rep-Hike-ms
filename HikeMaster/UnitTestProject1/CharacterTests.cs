using Microsoft.VisualStudio.TestTools.UnitTesting;
using HikeMaster;
namespace HikeMaster
{
    [TestClass]
    public class CharacterTests
    {
        [TestMethod]
        public void CharacterDamageIsProper()
        {
            const int damage = 10;
            const int startHealth = 100;
            const int expectedHealth = 90;
            int[] att = new int[9] { 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            int[][] tal = new int[9][]
            {
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10}
            };
            Character testCharacter = new Character("Clark", 1, att, tal,startHealth, 100, 100, 100, new Item("testItem","test",1,1,1,new System.Collections.Generic.List<PassiveUse> { },new System.Collections.Generic.List<UseItem> { }));
            testCharacter.DamageCharacter(damage);
            int health = testCharacter.GetHealth();
            Assert.AreEqual(expectedHealth, health);
        }
        [TestMethod]
        public void CharacterExhaustIsProper()
        {
            const int exhaustion = 10;
            const int startEshaust = 100;
            const int expectedExhaust = 90;
            int[] att = new int[9] { 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            int[][] tal = new int[9][]
            {
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10}
            };
            Character testCharacter = new Character("Clark", 1, att, tal, 100, startEshaust,100, 100, new Item("testItem", "test", 1, 1, 1, new System.Collections.Generic.List<PassiveUse> { }, new System.Collections.Generic.List<UseItem> { }));
            testCharacter.DamageCharacter(exhaustion);
            int exhaust = testCharacter.GetHealth();
            Assert.AreEqual(expectedExhaust, exhaust);
        }
        [TestMethod]
        public void CharacterHungerIsProper()
        {
            const int hunger = 10;
            const int startSatiety = 100;
            const int expectedSatiety = 90;
            int[] att = new int[9] { 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            int[][] tal = new int[9][]
            {
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10},
                new int[]{10,10,10,10,10}
            };
            Character testCharacter = new Character("Clark", 1, att, tal, 100, 100, startSatiety, 100, new Item("testItem", "test", 1, 1, 1, new System.Collections.Generic.List<PassiveUse> { }, new System.Collections.Generic.List<UseItem> { }));
            testCharacter.DamageCharacter(hunger);
            int exhaust = testCharacter.GetHealth();
            Assert.AreEqual(expectedSatiety, exhaust);
        }
    }
}
